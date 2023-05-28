using Omaha.Infra.Common;
using Omaha.Infra.DTOs;

namespace Omaha.Negocio.Interfaces
{
    public interface IPdfList
    {
        Task<List<TblPdfFileDTO>> GetListPdf(int IdUser);

        Task<string> GetArchivoPDf(int IdFile);
        Task<ApiResponse<string>> InsertaPdf(PdfInsert aPdf);

        Task<List<string>> GetTpoFondo();

        Task<List<string>> GetPeriodoReportes();

        Task<List<TblPdfFilesReporteDTO>> GetListPdfReportes(string TpoFondo, string Periodo);

        Task<string> GetArchivoPDfReporte(int IdFile);

        Task<ApiResponse<string>> InsertaPdfReporte(PdfInsertReport aPdf);


    }
}
