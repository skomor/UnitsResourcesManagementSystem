using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Aut3.Models.Enums;
using Microsoft.AspNet.OData.Builder;


namespace Aut3.Models
{
    public class Soldier
    {
  
        public Guid SoldierId { get; set; }
        [Required]
        public string FName { get; set; }
        [Required]
        public string LName { get; set; }
        /*
        [Required]
        */
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Zła ilość znaków")]
        public string Pesel { get; set; }
        [Range(0, 42)]
        public int Rank { get; set; }
        public bool Sex { get; set; }
        public string PlaceOfBirth { get; set; }
        
       /*
       [AutoExpand]
        */
        public virtual RegistrationOfSoldier RegistrationOfSoldier { get; set; }
        /*
        [AutoExpand]
        */
        public Guid? MilitaryUnitId { get; set; }

        public virtual MilitaryUnit? CurrUnit { get; set; }
        
        
        
        public virtual ICollection<FamilyRelationToSoldier> FamilyRelationToSoldiers { get; set; }
        public virtual ICollection<Vehicle> OwnedVehicles { get; set; }


        
    }
}
