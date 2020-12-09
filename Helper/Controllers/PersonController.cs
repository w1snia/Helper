using Helper.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
            return View(_personRepository.GetPersons());
        }


    }
}
