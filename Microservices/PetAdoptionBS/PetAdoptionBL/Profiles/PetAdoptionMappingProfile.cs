using AutoMapper;
using DAL_Core.Entities;
using PetAdoptionBL.Models;

namespace PetAdoptionBL.Profiles;

public class PetAdoptionMappingProfile : Profile
{
    public PetAdoptionMappingProfile()
    {
        CreateMap<Pet, PetDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.Breed, opt => opt.MapFrom(src => src.Breed))
            .ForMember(dest => dest.StoreId, opt => opt.MapFrom(src => src.StoreId))
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
            .ForMember(dest => dest.IsAdopted, opt => opt.MapFrom(src => src.CustomerId != null))
            .ForMember(dest => dest.Treatments, opt => opt.MapFrom(src => src.HealthCares))
            .ReverseMap();

        CreateMap<Pet, CreatePetDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.Breed, opt => opt.MapFrom(src => src.Breed))
            .ForMember(dest => dest.StoreId, opt => opt.Ignore())
            .ForMember(dest => dest.TreatmentIds, opt => opt.Ignore())
            .ForMember(dest => dest.CustomerId, opt => opt.Ignore())
            .ReverseMap();

        CreateMap<TreatmentDto, HealthCare>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.TreatmentName, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.InjectedAt, opt => opt.MapFrom(src => src.InjectedAt))
            .ForMember(dest => dest.ExpirationDate, opt => opt.MapFrom(src => src.ExpirationDate))
            .ForMember(dest => dest.PetId, opt => opt.MapFrom(src => src.PetId))
            .ForMember(dest => dest.VendorId, opt => opt.MapFrom(src => src.VendorId))
            .ReverseMap();

        CreateMap<Store, StoreDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
            .ForMember(dest => dest.Pets, opt => opt.MapFrom(src => src.Pets))
            .ReverseMap();

        CreateMap<Store, CreateStoreDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
            .ForMember(dest => dest.PetIds, opt => opt.Ignore())
            .ReverseMap();

        CreateMap<Customer, CustomerDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
            .ForMember(dest => dest.Pets, opt => opt.Ignore())
            .ReverseMap();

        CreateMap<Customer, CreateCustomerDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
            .ForMember(dest => dest.PetIds, opt => opt.Ignore())
            .ReverseMap();
    }
}

