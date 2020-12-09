using Helper.Models;
using System.Collections.Generic;

namespace Helper.Repositories.Interfaces
{
    public interface IPersonRepository
    {
        public List<Person> GetPersons();
    }
}
