using Microsoft.AspNetCore.Identity;

namespace Estate.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;

        public bool IsActivate { get; set; }
        public string Img { get; set; } = "default-profile.png";
        public string? About { get; set; }


        public ICollection<ProductComment>? ProductComments { get; set; }
        public ICollection<ProductReply>? ProductReplies { get; set; }
        public ICollection<BlogComment>? BlogComments { get; set; }
        public ICollection<BlogReply>? BlogReplies { get; set; }

    }
}
