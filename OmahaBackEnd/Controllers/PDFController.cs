using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Omaha.Infra.Common;
using Omaha.Negocio.Interfaces;

namespace OmahaBackEnd.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PDFController : ControllerBase
    {
        private IPdfList _pdfList;

        public PDFController(IPdfList pdfList)
        {
            _pdfList = pdfList;
        }


        [HttpGet]
        public async Task<IActionResult> GetPdfbyUser(int IdUser)
        {
            var result = await _pdfList.GetListPdf(IdUser);
            return Ok(result);

        }
        [HttpGet]
        public async Task<IActionResult> GetArchivoPDf(int IdFile)
        {
            var result = await _pdfList.GetArchivoPDf(IdFile);
            return Ok(result);

        }

        [HttpPost]
        public async Task<IActionResult> InsertaPdf(PdfInsert aPdf)
        {
            var result = await _pdfList.InsertaPdf(aPdf);
            return Ok(result);

        }

        [HttpGet]
        public async Task<IActionResult> GetTpoFondo()
        {
            var result = await _pdfList.GetTpoFondo();
            return Ok(result);

        }

        [HttpGet]
        public async Task<IActionResult> GetPeriodoReportes()
        {
            var result = await _pdfList.GetPeriodoReportes();
            return Ok(result);

        }

        [HttpGet]
        public async Task<IActionResult> GetListPdfReportes(string TpoFondo, string Periodo)
        {
            var result = await _pdfList.GetListPdfReportes( TpoFondo, Periodo);
            return Ok(result);

        }

        [HttpGet]
        public async Task<IActionResult> GetArchivoPDfReporte(int IdFile)
        {
            var result = await _pdfList.GetArchivoPDfReporte(IdFile);
            return Ok(result);

        }


        [HttpPost]
        public async Task<IActionResult> InsertaPdfReporte(PdfInsertReport aPdf)
        {
            var result = await _pdfList.InsertaPdfReporte(aPdf);
            return Ok(result);

        }


    }
}
