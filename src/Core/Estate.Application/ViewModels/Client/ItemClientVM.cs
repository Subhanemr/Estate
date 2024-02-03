using Estate.Domain.Entities;

namespace Estate.Application.ViewModels
{
    public record ItemClientVM(int Id, string Comment, string Country, string? Specialty, 
        int CorporateId, string AppUserId, AppUser? AppUser, IncludeCorporateVM? Corporate);
}
