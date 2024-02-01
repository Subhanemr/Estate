﻿using Estate.Application.ViewModels.BlogImage;
using Microsoft.AspNetCore.Http;

namespace Estate.Application.ViewModels.Blog
{
    public record UpdateBlogVM(string Name, string Description,
        string FaceLink, string TwitLink, string? GoogleLink, string LinkedLink, string InstaLink, IFormFile MainPhoto,
        ICollection<IFormFile>? Photos)
    {
        public ICollection<IncludeBlogImageVM>? BlogImages { get; set; }
        public List<int>? BlogImageIds { get; set; }
    }
}