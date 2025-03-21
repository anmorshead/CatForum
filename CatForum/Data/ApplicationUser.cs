﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using CatForum.Models;

namespace CatForum.Data
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData] // property is included in download of personal data.
        public string Name { get; set; } = string.Empty;

        [PersonalData]
        public string Location { get; set; } = string.Empty;

        [PersonalData]
        public string ImageFileName { get; set; } = string.Empty;

        //property for file upload, not mapped
        [NotMapped]
        [Display(Name = "Profile Picture")]
        public IFormFile? ImageFile { get; set; } //nullable

        // Navigation property for discussions
        public virtual ICollection<Discussion> Discussions { get; set; } = new List<Discussion>();

    }
}
