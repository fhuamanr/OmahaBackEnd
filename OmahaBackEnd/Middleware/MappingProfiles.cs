using AutoMapper;
using Omaha.Infra.DTOs;
using Omaha.Infra.Models;
using System.Net;

namespace OmahaBackEnd.Middleware
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<TblPerfile, PerfilesDTO>();
            CreateMap<TblPdfFile, TblPdfFileDTO>();
            CreateMap<TblPdfFileDTO, TblPdfFile>().ReverseMap();
            CreateMap<TblPdfFilesReporte, TblPdfFilesReporteDTO>();
            CreateMap<TblPdfFilesReporteDTO, TblPdfFilesReporte>().ReverseMap();

        }
    }
}
