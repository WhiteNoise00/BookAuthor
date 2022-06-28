using BookAuthor.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookAuthor.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Character> Characters { get; set; }
        public DbSet<Chapter> Chapters{ get; set; }
        public DbSet<Scene> Scenes { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Note> Notes { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }

}


