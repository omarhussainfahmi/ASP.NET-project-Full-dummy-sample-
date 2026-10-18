"""Fail CI if NuGet reports known vulnerable packages or audit errors."""
import json
import sys
from pathlib import Path

data = json.loads(Path(sys.argv[1]).read_text(encoding='utf-8-sig'))
findings = []
for project in data.get('projects', []):
    for framework in project.get('frameworks', []):
        for kind in ('topLevelPackages', 'transitivePackages'):
            for package in framework.get(kind, []):
                if package.get('vulnerabilities'):
                    findings.append(f"{Path(project['path']).stem}: {package['id']} {package['resolvedVersion']}")
if findings or data.get('errors') or data.get('problems'):
    print('Dependency audit failed:', *findings, sep='\n')
    raise SystemExit(1)
print('PASS: NuGet reports no known vulnerable packages in the solution.')
