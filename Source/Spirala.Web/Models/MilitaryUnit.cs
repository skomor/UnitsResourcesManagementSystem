using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.OData.Builder;

namespace Aut3.Models
{
    public class MilitaryUnit
    {
        public Guid MilitaryUnitId { get; set; }
        [Required] public string Name { get; set; }

        [Required] public string UnitNumber { get; set; }
        public string City { get; set; }

        [AutoExpand] public County County { get; set; }
        public int CountyID { get; set; }
        public List<Soldier> Soldiers { get; set; }
        public List<Vehicle> Vehicles { get; set; }
        public List<RegistrationOfSoldier> RegistrationOfSoldiers { get; set; }
    }
}