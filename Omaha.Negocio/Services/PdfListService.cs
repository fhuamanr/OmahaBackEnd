using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Omaha.Infra.Common;
using Omaha.Infra.Context;
using Omaha.Infra.DTOs;
using Omaha.Infra.Models;
using Omaha.Negocio.Interfaces;
using System.Buffers.Text;

namespace Omaha.Negocio.Services
{
    public class PdfListService : IPdfList
    {
        private IConfiguration _config;
        private readonly string _connectionString;
        private readonly OmahaContext _ContextDBSQL;
        private readonly IMapper _mapper;
        public PdfListService(IConfiguration config, OmahaContext omahaContext, IMapper mapper) 
        {
            _config = config;
            _connectionString = config.GetConnectionString("ConexionDB-EF");
            _ContextDBSQL = omahaContext;
            _mapper = mapper;
        }
        public async Task<List<TblPdfFileDTO>> GetListPdf(int IdUser)
        {
            try
            {
                var data = await _ContextDBSQL.TblPdfFiles
               .Where(x => x.IdUsuario == IdUser)
               .ToListAsync();
                var mappedPDF = _mapper.Map<List<TblPdfFileDTO>>(data);
               return mappedPDF;
            }
            catch (Exception)
            {

                throw;
            }           

        }

        public async Task<string> GetArchivoPDf(int IdFile)
        {
            try
            {
                var data = await _ContextDBSQL.TblPdfFiles
                    .Where(x=> x.Id == IdFile)
                    .Select(x => x.File)
                    .FirstOrDefaultAsync();
                if (data is not null)
                {
                    var database = Convert.ToBase64String(data);
                    return database;
                }
                else return "No hay data";
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ApiResponse<string>> InsertaPdf(PdfInsert aPdf)
        {
            var rspta = new ApiResponse<string>();
            try
            {
                byte[] bytes = System.Convert.FromBase64String(aPdf.File);

                var nuevoPdf = new TblPdfFile()
                {
                    IdUsuario = aPdf.IdUsuario,
                    Periodo = aPdf.Periodo,
                    Part = aPdf.Part,
                    NombreArchivo = aPdf.NombreArchivo,
                    File = bytes,
                    FechaCarga = DateTime.Now,
                    Vigente = true
                };
                await _ContextDBSQL.TblPdfFiles.AddAsync(nuevoPdf);
                await _ContextDBSQL.SaveChangesAsync();
                rspta.Message = "Registro insertado con éxito!";
                rspta.Succeeded = true;
            }
            catch (Exception)
            {

                rspta.Message = "Problemas al insertar la información!";
                rspta.Succeeded = false;
            }
            return rspta;


        }

        public async Task<List<string>> GetTpoFondo()
        {
            return await _ContextDBSQL
                .TblPdfFilesReportes
                .Select(x => x.TpoFondo).Distinct().ToListAsync();            
        }

        public async Task<List<string>> GetPeriodoReportes()
        {
            return  await _ContextDBSQL
                .TblPdfFilesReportes
                .Select(x => x.Periodo).Distinct().ToListAsync();
        }

        public async Task<List<TblPdfFilesReporteDTO>> GetListPdfReportes(string TpoFondo, string Periodo)
        {
            var data = await _ContextDBSQL
                .TblPdfFilesReportes
                .Where(x => x.TpoFondo == TpoFondo && x.Periodo == Periodo)
                .ToListAsync();
            var mappedPDF = _mapper.Map<List<TblPdfFilesReporteDTO>>(data);
            return mappedPDF;

        }

        public async Task<string> GetArchivoPDfReporte(int IdFile)
        {
            try
            {
                var data = await _ContextDBSQL.TblPdfFilesReportes
                    .Where(x => x.Id == IdFile)
                    .Select(x => x.File)
                    .FirstOrDefaultAsync();
                if (data is not null)
                {
                    var database = Convert.ToBase64String(data);
                    return database;
                }
                else return "No hay data";
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ApiResponse<string>> InsertaPdfReporte(PdfInsertReport aPdf)
        {
            var rspta = new ApiResponse<string>();
            try
            {
                byte[] bytes = System.Convert.FromBase64String(aPdf.File);

                var nuevoPdf = new TblPdfFilesReporte()
                {
                    TpoFondo = aPdf.TpoFondo,
                    Periodo = aPdf.Periodo,                   
                    NombreArchivo = aPdf.NombreArchivo,
                    File = bytes,
                    FechaCarga = DateTime.Now,
                    Vigente = true
                };
                await _ContextDBSQL.TblPdfFilesReportes.AddAsync(nuevoPdf);
                await _ContextDBSQL.SaveChangesAsync();
                rspta.Message = "Registro insertado con éxito!";
                rspta.Succeeded = true;
            }
            catch (Exception)
            {

                rspta.Message = "Problemas al insertar la información!";
                rspta.Succeeded = false;
            }
            return rspta;

        }
    }
}
