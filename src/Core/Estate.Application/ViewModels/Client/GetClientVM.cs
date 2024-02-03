using Estate.Domain.Entities;

namespace Estate.Application.ViewModels
{
    public record GetClientVM(int Id, string Comment, string Country, string? Specialty,
        int CorporateId, string AppUserId, AppUser AppUser, IncludeCorporateVM Corporate);
}
