using SentinelBusinessLayer.Clients;
using SentinelBusinessLayer.Models;
using SentinelBusinessLayer.Service.Interface;

namespace SentinelBusinessLayer.Service;

public class VendorService : IVendorService
{
    private readonly IVendorClient _vendorClient;
    public VendorService(IVendorClient vendorClient)
    {
        _vendorClient = vendorClient;
    }
    public async Task<List<VendorDto>> GetAllVendors()
    {
        return await _vendorClient.GetAllVendorsAsync();
    }
    public async Task<VendorDto> GetVendor(Guid id)
    {
        return await _vendorClient.GetVendorByIdAsync(id);
    }
    public async Task CreateVendor(CreateVendorDto vendorDto)
    {
        await _vendorClient.CreateVendorAsync(vendorDto);
    }
    public async Task UpdateVendor(CreateVendorDto vendorDto)
    {
        await _vendorClient.UpdateVendorAsync(vendorDto);
    }
    public async Task DeleteVendor(Guid id)
    {
        await _vendorClient.DeleteVendorAsync(id);
    }
    public async Task<List<VendorDto>> GetVendorsByIds(IEnumerable<Guid> ids)
    {
        return await _vendorClient.GetVendorsByIdsAsync(ids);
    }
    public async Task<List<VendorDto>> GetVendorsByContractType(string type)
    {
        return await _vendorClient.GetVendorsByContractTypeAsync(type);
    }
}

