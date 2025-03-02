using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CatForum.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CatForum.Data
{
    public class CatForumContext : IdentityDbContext<ApplicationUser>
    {
        public CatForumContext (DbContextOptions<CatForumContext> options)
            : base(options)
        {
        }

        public DbSet<CatForum.Models.Comment> Comment { get; set; } = default!;
        public DbSet<CatForum.Models.Discussion> Discussion { get; set; } = default!;
    }
}
