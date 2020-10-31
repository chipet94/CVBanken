using System;
using System.Collections.Generic;
using System.Linq;
using CVBanken.Data.Models.Auth;

namespace CVBanken.Data.Models
{
    public class Programme
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public virtual ICollection<User> Students { get; set; }

        public int TotalStudents()
        {
            return Students.Count;
        }

        public int PublicStudents()
        {
            return Students.Count(s => s.Private == false);
        }
    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Programme> Programmes { get; set; }
    }
}