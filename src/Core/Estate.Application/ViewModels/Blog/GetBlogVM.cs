﻿namespace Estate.Application.ViewModels
{
    public record GetBlogVM(int Id, string Name, string Description, DateTime CreateAt, string CreatedBy,
        string FaceLink, string TwitLink, string? GoogleLink, string LinkedLink, string InstaLink, 
        ICollection<IncludeBlogImageVM> Images, ICollection<IncludeBlogCommnetVM> Commnets);
}
