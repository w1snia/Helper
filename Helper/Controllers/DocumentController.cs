using Helper.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Helper.Controllers
{
    public class DocumentController : Controller
    {
        private readonly IDocumentRepository _documentRepository;

        public DocumentController(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public IActionResult Index()
        {
            _documentRepository.MakeDocx();
            return RedirectToAction("Index", "Home");
        }
    }
}
