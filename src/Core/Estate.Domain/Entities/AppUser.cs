using Microsoft.AspNetCore.Identity;

namespace Estate.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;

        public bool IsActivate { get; set; }
        public string Img { get; set; } = "default-profile.png";

        public string? PhoneSecond { get; set; } 
        public string? Address { get; set; }
        public string? About { get; set; } 
        public string? FaceLink { get; set; } 
        public string? TwitLink { get; set; } 
        public string? GoogleLink { get; set; } 
        public string? LinkedLink { get; set; } 
        public string? InstaLink { get; set; }

        public bool IsFounder { get; set; }
        public bool SoulOfAgency { get; set; }

        public ICollection<Product>? Products { get; set; }
        public ICollection<AgencyAppUser>? AgencyAppUsers { get; set; }
        public ICollection<AppUserImage>? AppUserImages { get; set; }

        public ICollection<Favorite>? Favorites { get; set; }
        public ICollection<ProductComment>? ProductComments { get; set; }
        public ICollection<ProductReply>? ProductReplies { get; set; }
        public ICollection<BlogComment>? BlogComments { get; set; }
        public ICollection<BlogReply>? BlogReplies { get; set; }


    }
}
