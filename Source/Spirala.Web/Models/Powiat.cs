using System.Collections.Generic;
using Microsoft.AspNet.OData.Builder;

namespace Aut3.Models
{
    public class Powiat
    {
        public int ID { get; set; }
        public string Nazwa { get; set; }

        public int WojewodztwoID { get; set; }
        [AutoExpand]
        public Wojewodztwo Wojewodztwo { get; set; }
        
        public virtual ICollection<Miasto> Miasta { get; set; }

        
    }
}