using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ML;

namespace TextExtractProject.Controllers
{
    [EnableCors("CORSPolicy")]
    [Route("api/DocumentController/")]
    [ApiController]
    public class DocumentParsingController : ControllerBase
    {
        private readonly UserAdharInformationContext _context;
        private OCRModel? oCRModel;
        

        public DocumentParsingController(UserAdharInformationContext context)
        {
            _context = context;
        }

        [HttpGet("OCR/{command}")]
        public async Task<List<string>> TestOCR(string command) {
            oCRModel = new OCRModel();
            return await oCRModel.TextExtractionMethod(command); 
        }

        [HttpPost("adharImage")]
        public async Task<IActionResult> TestDocumentIntelligeneceModel(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                DocumentIntelligenceModel DIModel = new DocumentIntelligenceModel();
                var jsonString = await DIModel.ModelAnalyzer(file);
                return Ok(jsonString);
            }
            return BadRequest("No valid image provided.");
        }

        //[HttpGet("adharImage")]
        //public async Task<string> TestDocumentIntelligeneceModel(string command) {

        //    DocumentIntelligenceModel DIModel = new DocumentIntelligenceModel();
        //    string jsonResult = await DIModel.ModelAnalyzer(int.Parse(command));
        //    return jsonResult;
        //}


        private byte[] ConvertIFormFileToBytes(IFormFile file)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}