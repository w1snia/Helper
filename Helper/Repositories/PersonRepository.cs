﻿using Hangfire;
using Helper.Models;
using Helper.Models.AppDbContext;
using Helper.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace Helper.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IConfiguration _configuration;

        public PersonRepository(AppDbContext appDbContext, IConfiguration configuration)
        {
            _appDbContext = appDbContext;
            _configuration = configuration;

        }

        public List<Person> GetPersons()
        {
            CheckJobs();
            return _appDbContext.Persons.ToList();
        }

        private void CheckJobs()
        {
            JobRepository jobRepository = new JobRepository(_appDbContext, _configuration);
            RecurringJob.AddOrUpdate(
                () => jobRepository.AddNewPersonEveryMinute(),
                "*/2 * * * *"
                );
        }
    }
}
