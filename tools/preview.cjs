const { chromium } = require(process.env.PLAYWRIGHT_MODULE || 'playwright');
const { spawn } = require('node:child_process');
const fs = require('node:fs');
const path = require('node:path');
const root = path.resolve(__dirname, '..');
(async () => {
  const log = fs.openSync(path.join(root,'preview-server.log'),'w');
  const server = spawn('dotnet',['run','--no-build','--project','firstASPnet/firstASPnet.csproj','--no-launch-profile','--urls','https://localhost:17443;http://localhost:17080'],{cwd:root,windowsHide:true,env:{...process.env,ASPNETCORE_ENVIRONMENT:'Production',HttpsPort:'17443'},stdio:['ignore',log,log]});
  let browser;
  try {
    browser = await chromium.launch({headless:true,channel:process.env.BROWSER_CHANNEL || 'msedge'});
    const context = await browser.newContext({ignoreHTTPSErrors:true,viewport:{width:1440,height:1000}});
    const page = await context.newPage();
    for(let attempt=0;attempt<60;attempt++) {
      try { await page.goto('https://localhost:17443/'); break; }
      catch(error) { if(attempt===59) throw error; await new Promise(r=>setTimeout(r,200)); }
    }
    const errors = [];
    page.on('pageerror', error => errors.push(error.message));
    fs.mkdirSync(path.join(root,'artifacts'),{recursive:true});
    for (const [name,width,height] of [['desktop',1440,1000],['mobile',390,844]]) {
      await page.setViewportSize({width,height});
      for (const [slug,url] of [['home','/'],['about','/company'],['form','/demo'],['not-found','/missing']]) {
        await page.goto('https://localhost:17443'+url);
        if (await page.evaluate(()=>document.documentElement.scrollWidth > window.innerWidth)) throw new Error('Horizontal overflow: '+name+' '+url);
        if (name==='mobile' && !(await page.locator('.mobile-contact').isVisible())) throw new Error('Mobile contact button hidden');
        await page.screenshot({path:path.join(root,'artifacts',name+'-'+slug+'.png'),fullPage:true});
      }
    }
    if(errors.length) throw new Error(errors.join('\n'));
    console.log('PASS: desktop/mobile pages render without horizontal overflow or page errors; screenshots saved in artifacts/.');
  } finally { if(browser) await browser.close(); server.kill(); fs.closeSync(log); }
})().catch(error=>{console.error(error.message);process.exitCode=1;});
