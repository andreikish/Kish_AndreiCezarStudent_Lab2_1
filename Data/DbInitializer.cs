using Microsoft.EntityFrameworkCore;
using Kish_AndreiCezarStudent_Lab2.Models;
namespace Kish_AndreiCezarStudent_Lab2.Data
{
    public static class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new Kish_AndreiCezarStudent_Lab2Context
           (serviceProvider.GetRequiredService
            <DbContextOptions<Kish_AndreiCezarStudent_Lab2Context>>()))
            {
                if (context.Book.Any())
                {
                   return; // BD a fost creata anterior
                }
                context.Author.AddRange(
               new Author { FirstName = "Mihail", LastName = "Sadoveanu" },
               new Author { FirstName = "George", LastName = "Calinescu" },
               new Author { FirstName = "Mircea", LastName = "Eliade" }
                );
                context.SaveChanges();
                var sadoveanu = context.Author.First(a => a.LastName == "Sadoveanu");
                var calinescu = context.Author.First(a => a.LastName == "Calinescu");
                var eliade = context.Author.First(a => a.LastName == "Eliade");

                context.Book.AddRange(
                    new Book
                    {
                        Title = "Baltagul",
                        Price = decimal.Parse("22"),
                        Author = sadoveanu
                    },
                    new Book
                    {
                        Title = "Enigma Otiliei",
                        Price = decimal.Parse("18"),
                        Author = calinescu
                    },
                    new Book
                    {
                        Title = "Maytrei",
                        Price = decimal.Parse("27"),
                        Author = eliade
                    }
                );

                context.Genre.AddRange(
               new Genre { Name = "Roman" },
               new Genre { Name = "Nuvela" },
               new Genre { Name = "Poezie" }
                );

                context.Customer.AddRange(
                new Customer
                {
                    Name = "Popescu Marcela",
                    Adress = "Str. Plopilor, nr. 24",
                    BirthDate = DateTime.Parse("1979-09-01")
                },
                new Customer
                {
                    Name = "Mihailescu Cornel",
                    Adress = "Str. Bucuresti, nr.45, ap. 2",BirthDate=DateTime.Parse("1969 - 07 - 08")}
               
                );

                context.SaveChanges();
            }
        }
    }
}