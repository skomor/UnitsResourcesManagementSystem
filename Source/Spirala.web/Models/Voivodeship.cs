using System.Collections.Generic;

namespace Aut3.Models
{
    public class Voivodeship
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<County> Counties { get; set; }

    }
}