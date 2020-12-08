using Microsoft.AspNet.OData.Builder;

namespace Aut3.Models
{
    public class Miasto
    {
        public int ID { get; set; }
        public string Nazwa { get; set; }
        
        public int WojewodztwoID { get; set; }
        [AutoExpand]
        public Wojewodztwo Wojewodztwo { get; set; }
        
        public int PowiatID { get; set; }
        [AutoExpand]
        public Powiat Powiat { get; set; }
        

    }
}