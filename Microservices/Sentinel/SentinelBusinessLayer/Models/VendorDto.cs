using DAL_Core.Enums;

namespace SentinelBusinessLayer.Models;

public class VendorDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public bool IsExpired { get; }

    public DateTime? SignedAt { get; set; }

    public DateTime? ExpirationDate { get; set; }

    public ContractType ContractType { get; set; }
}