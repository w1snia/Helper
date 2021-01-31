using Helper.Models;
using Helper.Models.AppDbContext;
using Microsoft.Extensions.Configuration;
using System;

namespace Helper.Repositories
{
    public class JobRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IConfiguration _configuration;

        public JobRepository(AppDbContext appDbContext, IConfiguration configuration)
        {
            _appDbContext = appDbContext;
            _configuration = configuration;
        }

        public void AddNewPersonEveryMinute()
        {
            Person person = new Person()
            {
                FirstName = DateTime.Now.ToString(),
                LastName = DateTime.Now.ToString()
            };
            _appDbContext.Persons.Add(person);
            _appDbContext.SaveChanges();
        }
    }
}
