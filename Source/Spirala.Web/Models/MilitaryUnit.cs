using System;
using System.Collections.Generic;

namespace Aut3.Models
{
    public class MilitaryUnit
    {
        /*
        public long SId { get; set; }
        */
        public Guid MilitaryUnitId { get; set; }
        public string Name{ get; set; }
        public string UnitNumber{ get; set; }
        public string City { get; set; }
        
        public List<Soldier> Soldiers { get; set; }
        public List<Vehicle> Vehicles { get; set; }
        public List<RegistrationOfSoldier> RegistrationOfSoldiers { get; set; }

        /*
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string WhoEdited { get; set; }*/
    }
}