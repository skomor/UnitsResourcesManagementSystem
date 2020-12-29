using Microsoft.AspNet.OData.Builder;

namespace Aut3.Models
{
    public class City
    {
        public int ID { get; set; }
        public string Name { get; set; }
        
        public int VoivodeshipID { get; set; }
        [AutoExpand]
        public Voivodeship Voivodeship { get; set; }
        
        public int CountyID { get; set; }
        [AutoExpand]
        public County County { get; set; }
        

    }
}