using Kish_AndreiCezarStudent_Lab2.Migrations;
using Kish_AndreiCezarStudent_Lab2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kish_AndreiCezarStudent_Lab2.Data
{
    public class Kish_AndreiCezarStudent_Lab2Context : DbContext
    {
        public Kish_AndreiCezarStudent_Lab2Context (DbContextOptions<Kish_AndreiCezarStudent_Lab2Context> options)
            : base(options)
        {
        }

        public DbSet<Kish_AndreiCezarStudent_Lab2.Models.Author> Author { get; set; } = default!;
        public DbSet<Kish_AndreiCezarStudent_Lab2.Models.Book> Book { get; set; } = default!;
        public DbSet<Kish_AndreiCezarStudent_Lab2.Models.Customer> Customer { get; set; } = default!;
        public DbSet<Kish_AndreiCezarStudent_Lab2.Models.Genre> Genre { get; set; } = default!;
        
    }
}
