using Helper.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Helper.Repositories.Interfaces
{
    public interface IPersonRepository
    {
        public List<Person> GetPersons();
        public Task<string> GetTextAsync();
    }
}
