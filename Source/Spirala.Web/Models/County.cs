using System.Collections.Generic;
using Microsoft.AspNet.OData.Builder;

namespace Aut3.Models
{
    public class County
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public int VoivodeshipID { get; set; }
        [AutoExpand]
        public Voivodeship Voivodeship { get; set; }
        
        public virtual ICollection<City> Cities { get; set; }

        
    }
}