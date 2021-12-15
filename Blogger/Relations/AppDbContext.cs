﻿using Blogger.Relations;
using Microsoft.EntityFrameworkCore;

namespace UniShareAPI.Models.Relations
{
    public class AppDbContext : DbContext
    {
        public DbSet<Blog>? Blog { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
