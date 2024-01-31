﻿namespace Estate.Domain.Entities
{
    public class BlogReply : BaseEntity
    {
        public string ReplyComment { get; set; } = null!;
        public DateTime ReplyCommentTime { get; set; }
        public string AppUserId { get; set; } = null!;
        public AppUser AppUser { get; set; } = null!;
        public int BlogCommentId { get; set; }
        public BlogComment BlogComment { get; set; } = null!;
    }
}
