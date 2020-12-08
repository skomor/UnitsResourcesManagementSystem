using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.OData.Builder;

namespace Aut3.Models
{
    public class MilitaryUnit
    {
        /*
        public long SId { get; set; }
        */
        public Guid MilitaryUnitId { get; set; }
        [Required]
        public string Name{ get; set; }
        [Required]
        public string UnitNumber{ get; set; }
      

        public string Miasto { get; set; }
        [AutoExpand]
        public Powiat Powiat { get; set; }
        public int PowiatID { get; set; }
        
        public List<Soldier> Soldiers { get; set; }
        public List<Vehicle> Vehicles { get; set; }
        public List<RegistrationOfSoldier> RegistrationOfSoldiers { get; set; }

        /*
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string WhoEdited { get; set; }*/
    }
}