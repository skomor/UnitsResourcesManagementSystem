using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.OData.Edm;

namespace Aut3.Models
{
    public class FamilyMember
    {
        /*
        public long SId { get; set; }
        */

        public Guid FamilyMemberId { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public Boolean Sex { get; set; }
        public string PlaceOfResidence { get; set; }
        [Column(TypeName="Date")]
        public DateTime DateOfBirth { get; set; }
        
        public virtual ICollection<FamilyRelationToSoldier> FamilyRelationToSoldiers { get; set; }
        
        
        /*public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string WhoEdited { get; set; }*/
    }
}