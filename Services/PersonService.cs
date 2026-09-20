using System;
using Entities;
using ServiceContracts1.DTO;
using ServiceContracts1;
using ServiceContracts;
using ServiceContracts.Enums;
using CsvHelper;
using System.Globalization;
using System.IO;
using OfficeOpenXml;
using RepositoryContracts;
using Microsoft.Extensions.Logging;
 

namespace Services
{
    public class PersonService : IPersonService
    {
      private readonly IPersonsRepository _personsRepository;
        private readonly ILogger<PersonService> _logger;
        public PersonService(IPersonsRepository personsRepository, ILogger<PersonService> logger)
        {
            _personsRepository = personsRepository;
          
        }

        public List<ServiceContracts.PersonResponse> GetFilteredPersons(string searchby, string? searchString)
        {
            _logger.LogInformation("GetFilteredPersons method called in PersonService");
            throw new NotImplementedException();
        }
      

        public bool DeletPerson(Guid? PersonID)
        {
            throw new NotImplementedException();
        }

        public List<ServiceContracts.PersonResponse> getAllPersons(string searchBy)
        {
            _logger.LogInformation("getAllPersons method called in PersonService");
            throw new NotImplementedException();
        }

        public List<ServiceContracts.PersonResponse> GetFilteredPersons(object searchby, string? searchString)
        { 
            throw new NotImplementedException();
        }

        public List<ServiceContracts.PersonResponse> getAllPersons()
        {
            throw new NotImplementedException();
        }

        public void GetSortedPersons(List<ServiceContracts.PersonResponse> persons, object sortBy, object sortOrder)
        {
            _logger.LogInformation("GetSortedPersons method called in PersonService");
            throw new NotImplementedException();
        }

        void IPersonService.AddPerson(ServiceContracts.PersonAddRequest personAddRequest)
        {
            throw new NotImplementedException();
        }

        public void GetPersonByPersonID(Guid personID)
        {
            throw new NotImplementedException();
        }

        public async Task<MemoryStream> GetPersonsCSV()
        {
              MemoryStream memoryStream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(memoryStream);
            CsvWriter csvWriter = new CsvWriter(streamWriter, System.Globalization.CultureInfo.InvariantCulture,
                leaveOpen: true);

            csvWriter.WriteHeader<ServiceContracts.PersonResponse>();
            csvWriter.NextRecord();
            List<ServiceContracts.PersonResponse> Person = getAllPersons();
            csvWriter.WriteRecordsAsync(Person);
            memoryStream.Position = 0;
            return memoryStream;
        }

        public async Task<MemoryStream> GetPersonExcel()
        {
            MemoryStream memoryStream = new MemoryStream();
            using(ExcelPackage excelPackage = new ExcelPackage(memoryStream))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Persons");
                worksheet.Cells["A1"].LoadFromCollection(getAllPersons(), true);
                excelPackage.Save();
                memoryStream.Position = 0;
                return memoryStream;
            }


        }
        /* Faustina,fphilpault0@fastcompany.com,1997-12-03,Female,2780 Ridgeview Plaza,true
Vanya,vcox1@uiuc.edu,1994-03-30,Genderqueer,44470 Kipling Center,true
Neron,nbenge2@live.com,2010-11-11,Male,00002 Melby Plaza,false
Bidget,bmayte3@zimbio.com,1996-02-02,Bigender,08910 Prairieview Parkway,true
Corbet,cbaudino4@chron.com,2023-02-18,Male,84496 Bunting Crossing,true
Joel,jbeaument5@diigo.com,2012-07-08,Male,341 Mesta Alley,false
Emelina,egorger6@squarespace.com,1995-05-23,Genderqueer,72227 Melody Center,true
Gabi,glesly7@gizmodo.com,2000-09-08,Female,41 Derek Place,false
Walker,wstubley8@techcrunch.com,1999-06-02,Male,5094 Express Street,false
Abraham,alafrentz9@mediafire.com,2021-12-17,Male,082 Redwing Junction,false
        */

        //_countriesRepository.sp_InsertPerson(person);
    }
}
