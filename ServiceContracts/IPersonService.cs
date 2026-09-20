using System;
using ServiceContracts.Enums;


namespace ServiceContracts
{
    public interface IPersonService
    {
        PersonResponse AddPerson (PersonAddRequest personAddRequest);

        List<PersonResponse> GetAllPersons();
    }

}
