using System;
using System.Collections.Generic;


namespace Aut3.Models
{
    public class Soldier
    {
  
        public Guid SoldierId { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Pesel { get; set; }
        public bool Sex { get; set; }
        public string PlaceOfBirth { get; set; }
        
        public RegistrationOfSoldier RegistrationOfSoldier { get; set; }
        public MilitaryUnit CurrUnit { get; set; }
        
        
        
        public virtual ICollection<FamilyRelationToSoldier> FamilyRelationToSoldiers { get; set; }
        public virtual ICollection<Vehicle> OwnedVehicles { get; set; }


        
    }
}
