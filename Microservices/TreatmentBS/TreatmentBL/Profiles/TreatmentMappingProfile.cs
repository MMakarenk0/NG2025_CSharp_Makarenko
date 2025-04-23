using AutoMapper;
using DAL_Core.Entities;
using TreatmentBL.Models;

namespace TreatmentBL.Profiles;
public class TreatmentMappingProfile : Profile
{
    public TreatmentMappingProfile()
    {
        CreateMap<HealthCare, TreatmentDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.TreatmentName))
            .ForMember(dest => dest.InjectedAt, opt => opt.MapFrom(src => src.InjectedAt))
            .ForMember(dest => dest.ExpirationDate, opt => opt.MapFrom(src => src.ExpirationDate))
            .ForMember(dest => dest.IsExpired, opt => opt.MapFrom(src => src.ExpirationDate <= DateTime.Now))
            .ForMember(dest => dest.PetId, opt => opt.MapFrom(src => src.PetId))
            .ForMember(dest => dest.VendorId, opt => opt.MapFrom(src => src.VendorId))
            .ReverseMap();

        CreateMap<HealthCare, CreateTreatmentDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.TreatmentName))
            .ForMember(dest => dest.InjectedAt, opt => opt.MapFrom(src => src.InjectedAt))
            .ForMember(dest => dest.ExpirationDate, opt => opt.MapFrom(src => src.ExpirationDate))
            .ForMember(dest => dest.PetId, opt => opt.Ignore())
            .ForMember(dest => dest.VendorId, opt => opt.Ignore())
            .ReverseMap();

        CreateMap<Vendor, VendorDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.IsExpired, opt => opt.MapFrom(src => src.ExpirationDate <= DateTime.Now))
            .ForMember(dest => dest.SignedAt, opt => opt.MapFrom(src => src.SignedAt))
            .ForMember(dest => dest.ExpirationDate, opt => opt.MapFrom(src => src.ExpirationDate))
            .ForMember(dest => dest.ContractType, opt => opt.MapFrom(src => src.ContractType))
            .ReverseMap();

        CreateMap<Pet, PetDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.Breed, opt => opt.MapFrom(src => src.Breed))
            .ForMember(dest => dest.StoreId, opt => opt.MapFrom(src => src.StoreId))
            .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer))
            .ForMember(dest => dest.IsAdopted, opt => opt.MapFrom(src => src.CustomerId != null));

        CreateMap<Customer, CustomerDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
            .ForMember(dest => dest.Pets, opt => opt.Ignore())
            .ReverseMap();
    }
}
