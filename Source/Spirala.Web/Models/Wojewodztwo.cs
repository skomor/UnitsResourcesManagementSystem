using System.Collections.Generic;

namespace Aut3.Models
{
    public class Wojewodztwo
    {
        public int ID { get; set; }
        public string Nazwa { get; set; }
        
        public virtual ICollection<Powiat> Powiaty { get; set; }

    }
}