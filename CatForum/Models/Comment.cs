using CatForum.Data;

namespace CatForum.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; }

        //foreign key
        public int DiscussionId { get; set; }

        //Navigation property - a comment belongs to a discussion
        public Discussion? Discussion { get; set; } //should always be nullable (?)

        // Foreign key (AspNetUsers table)
        public string ApplicationUserId { get; set; } = string.Empty;

        // Navigation property
        public ApplicationUser? ApplicationUser { get; set; } // nullable!!!
    }

}
