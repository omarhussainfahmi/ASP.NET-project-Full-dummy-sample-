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
        /* Fictional contact records removed. */

        //_countriesRepository.sp_InsertPerson(person);
    }
}
