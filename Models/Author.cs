using System.Collections.Generic;

namespace Kish_AndreiCezarStudent_Lab2.Models
{
    public class Author
    {
        public int ID { get; set; }

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        // Proprietate calculată pentru numele complet
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        // Relație unul-la-mulți cu Book
        public ICollection<Book>? Books { get; set; }
    }
}
