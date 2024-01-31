﻿namespace Estate.Domain.Entities
{
    public class ProductReply : BaseEntity
    {
        public string ReplyComment { get; set; } = null!;
        public DateTime ReplyCommentTime { get; set; }
        public string AppUserId { get; set; } = null!;
        public AppUser AppUser { get; set; } = null!;
        public int ProductCommentId { get; set; }
        public ProductComment ProductComment { get; set; } = null!;
    }
}
