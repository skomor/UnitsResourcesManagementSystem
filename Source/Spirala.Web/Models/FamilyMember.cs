using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.OData.Builder;
using Microsoft.OData.Edm;

namespace Aut3.Models
{
    public class FamilyMember
    {
        /*
        public long SId { get; set; }
        */

        public Guid FamilyMemberId { get; set; }
        [Required]
        public string FName { get; set; }
        [Required]
        public string LName { get; set; }
        public Boolean Sex { get; set; }
        public string PlaceOfResidence { get; set; }
        [Column(TypeName="Date")]
        public DateTime DateOfBirth { get; set; }
        [AutoExpand]
        public virtual ICollection<FamilyRelationToSoldier> FamilyRelationToSoldiers { get; set; }
        
        
        /*public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string WhoEdited { get; set; }*/
    }
}