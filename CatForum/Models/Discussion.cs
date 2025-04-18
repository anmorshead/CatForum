﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CatForum.Data;

namespace CatForum.Models
{
    public class Discussion
    {
        //primary key
        public int DiscussionId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string? ImageFileName { get; set; } // Nullable, so it's optional

        public DateTime CreateDate { get; set; } = DateTime.Now;

        //property for dile upload, not mapped in EF
        [NotMapped]
        [Display(Name = "Photograph")]
        public IFormFile? ImageFile { get; set; } //nullable

        //Navigation Property - a discussion can have many comments
        public List<Comment>? Comments { get; set; } //nullable

        // Foreign key (AspNetUsers table)
        public string ApplicationUserId { get; set; } = string.Empty;

        // Navigation property
        public ApplicationUser? ApplicationUser { get; set; } // nullable!!!

    }

}
