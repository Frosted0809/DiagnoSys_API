using AutoMapper;
using DiagnoSys_API.DTOs; // 
using DiagnoSys_API.Models;

namespace DiagnoSys_API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // lahat ng mga Dtos file sa DTOS folder nandito dapat
            CreateMap<User, UserDto>();
            CreateMap<CreateUserDto, User>();
            CreateMap<UpdateUserDto, User>();

            CreateMap<ClinicPatient, ClinicPatientDto>();
            CreateMap<CreateClinicPatientDto, ClinicPatient>();
            CreateMap<UpdateClinicPatientDto, ClinicPatient>();

            CreateMap<LabTestCatalog, LabTestCatalogDto>();
            CreateMap<CreateLabTestCatalogDto, LabTestCatalog>();
            CreateMap<UpdateLabTestCatalogDto, LabTestCatalog>();

            CreateMap<LabTest, LabTestDto>();
            CreateMap<CreateLabTestDto, LabTest>();
            CreateMap<UpdateLabTestDto, LabTest>();

            CreateMap<Cbc, CbcDto>();
            CreateMap<CreateCbcDto, Cbc>();
            CreateMap<UpdateCbcDto, Cbc>();

            CreateMap<Urinalysi, UrinalysiDto>();
            CreateMap<CreateUrinalysiDto, Urinalysi>();
            CreateMap<UpdateUrinalysiDto, Urinalysi>();

            CreateMap<Fecalysi, FecalysiDto>();
            CreateMap<CreateFecalysiDto, Fecalysi>();
            CreateMap<UpdateFecalysiDto, Fecalysi>();

            CreateMap<Payment, PaymentDto>();
            CreateMap<CreatePaymentDto, Payment>();
            CreateMap<UpdatePaymentDto, Payment>();
        }
    }
}