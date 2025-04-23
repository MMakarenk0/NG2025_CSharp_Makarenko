using AutoMapper;
using DAL_Core.Entities;
using DAL_Core.Enums;
using TreatmentBL.Models;
using TreatmentBL.Services.Interfaces;
using TreatmentDal.UoF;


namespace TreatmentBL.Services;

public class VendorService : IVendorService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public VendorService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<List<VendorDto>> GetAllVendors()
    {
        var _vendorRepository = _unitOfWork.VendorRepository;
        var vendors = await _vendorRepository.GetAllAsync();
        return _mapper.Map<List<VendorDto>>(vendors);
    }
    public async Task<VendorDto> GetVendorById(Guid id)
    {
        var _vendorRepository = _unitOfWork.VendorRepository;

        var vendor = await _vendorRepository.GetAsync(id);
        return _mapper.Map<VendorDto>(vendor);
    }

    public async Task<List<VendorDto>> GetManyVendorsByIds(IEnumerable<Guid> ids)
    {
        var _vendorRepository = _unitOfWork.VendorRepository;

        var vendors = await _vendorRepository.GetVendorsByIds(ids);
        return _mapper.Map<List<VendorDto>>(vendors);
    }

    public async Task<List<VendorDto>> GetVendorsByContractType(ContractType type)
    {
        var _vendorRepository = _unitOfWork.VendorRepository;

        var vendors = await _vendorRepository.GetVendorsByContractType(type);
        return _mapper.Map<List<VendorDto>>(vendors);
    }

    public async Task<Guid> CreateVendor(CreateVendorDto vendorDto)
    {
        var _vendorRepository = _unitOfWork.VendorRepository;

        var vendor = _mapper.Map<Vendor>(vendorDto);
        await _vendorRepository.CreateAsync(vendor);
        await _unitOfWork.SaveChangesAsync();
        return vendor.Id;
    }

    public async Task<Guid> UpdateVendor(CreateVendorDto vendorDto)
    {
        var _vendorRepository = _unitOfWork.VendorRepository;

        var vendor = await _vendorRepository.GetAsync(vendorDto.Id);

        if (vendor == null)
        {
            throw new Exception("Vendor not found");
        }

        _mapper.Map(vendorDto, vendor);

        await _vendorRepository.UpdateAsync(vendor);
        await _unitOfWork.SaveChangesAsync();
        return vendor.Id;
    }

    public async Task DeleteVendor(Guid id)
    {
        var _vendorRepository = _unitOfWork.VendorRepository;

        await _vendorRepository.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<List<TreatmentDto>> GetVendorHealthCares(Guid vendorId)
    {
        var _vendorRepository = _unitOfWork.VendorRepository;
        var healthCares = await _vendorRepository.GetVendorHealthCares(vendorId);
        return _mapper.Map<List<TreatmentDto>>(healthCares);
    }
}

