using Helper.Models.AppDbContext;
using Helper.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Helper.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {

        private readonly AppDbContext _appDbContext;
        private readonly IConfiguration _configuration;

        public DocumentRepository(AppDbContext appDbContext, IConfiguration configuration)
        {
            _appDbContext = appDbContext;
            _configuration = configuration;
        }

        public bool MakeDocx()
        {
            bool result = false;

            try
            {
                Spire.Doc.Document document = new Spire.Doc.Document();
                document.LoadFromFile(_configuration.GetSection("Documents").GetSection("TemplateDocx").Value);

                FillDocx(document);

                var savePath = _configuration.GetSection("Documents").GetSection("WhereSaveDocx").Value;
                var fullFilePath = $"{savePath}{DateTime.Now.ToShortDateString()}.docx";

                byte version = 1;
                while (File.Exists(fullFilePath))
                {
                    version++;
                    fullFilePath = $"{savePath}{DateTime.Now.ToShortDateString()}({version}).docx";
                }
                document.SaveToFile(fullFilePath);
                return result = true;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public void FillDocx(Spire.Doc.Document document)
        {
            document.Replace("_DATA_", DateTime.Now.ToString(), false, true);
        }

    }
}
