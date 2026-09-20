using System;
using ServiceContracts;
using ServiceContracts.Enums;



namespace Services
{
    public interface IPersonService
    {
        List<PersonResponse> GetFilteredPersons(string searchby, string? searchString);
        bool DeletPerson(Guid? PersonID);
        List<PersonResponse> getAllPersons(string searchBy);
        List<PersonResponse> GetFilteredPersons(object searchby, string? searchString);
        List<PersonResponse> getAllPersons();
        void GetSortedPersons(List<PersonResponse> persons, object sortBy, object sortOrder);
        void AddPerson(PersonAddRequest personAddRequest);
        void GetPersonByPersonID(Guid personID);

        Task<MemoryStream> GetPersonsCSV();

        Task<MemoryStream> GetPersonExcel();
    }
}