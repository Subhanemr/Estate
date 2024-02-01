﻿using Estate.Application.ViewModels.Corporate;

namespace Estate.Application.ViewModels.Client
{
    public record CreateClientVM(string Comment, string Country, string? Specialty, int CorporateId)
    {
        public ICollection<IncludeCorporateVM>? Corporates { get; set; }
    }
}
