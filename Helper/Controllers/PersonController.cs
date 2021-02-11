using Helper.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Threading.Tasks;

namespace Helper.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonRepository _personRepository;

        public PersonController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            Log.Information("Wywoało liste");
            Task<string> text = _personRepository.GetTextAsync();
            return View(_personRepository.GetPersons());
        }


    }
}
