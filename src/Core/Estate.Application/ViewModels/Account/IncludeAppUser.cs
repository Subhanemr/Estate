﻿namespace Estate.Application.ViewModels
{
    public record IncludeAppUser(string Id, string Name, string Surname, string UserName, string Img, string PhoneFirst, string? PhoneSecond,
        string Address, string About, string FaceLink, string TwitLink, string? GoogleLink, string LinkedLink, string InstaLink, 
        ICollection<IncludeAppUserImage> Images);
}
