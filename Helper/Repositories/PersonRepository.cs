using Helper.Models;
using Helper.Models.AppDbContext;
using Helper.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Helper.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly AppDbContext _appDbContext;

        public PersonRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public List<Person> GetPersons()
        {
            return _appDbContext.Persons.ToList();
        }
    }
}
