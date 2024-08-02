using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Projekt_atrakcje.Models;

namespace Projekt_atrakcje.Data
{
    public class Projekt_atrakcjeContext : DbContext
    {
        public Projekt_atrakcjeContext (DbContextOptions<Projekt_atrakcjeContext> options)
            : base(options)
        {
        }

        public DbSet<Projekt_atrakcje.Models.Country> Country { get; set; } = default!;
        public DbSet<Projekt_atrakcje.Models.City> City { get; set; } = default!;
        public DbSet<Projekt_atrakcje.Models.Attraction> Attraction { get; set; } = default!;
        public DbSet<Projekt_atrakcje.Models.User> User { get; set; } = default!;
        public DbSet<Projekt_atrakcje.Models.Grade> Grade { get; set; } = default!;
    }
}
