﻿using System.Reflection.Metadata;

namespace Estate.Domain.Entities
{
    public class ProductComment : BaseEntity
    {
        public string Comment { get; set; } = null!;
        public string AppUserId { get; set; } = null!;
        public AppUser? AppUser { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public ICollection<ProductReply>? ProductReplies { get; set; }
    }
}
