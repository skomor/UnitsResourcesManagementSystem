using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aut3.Models
{
    public class FamilyRelationToSoldier
    {
        public int FamilyRelationToSoldierId { get; set; }
        public Guid FamilyMemberId { get; set; }
        public Guid SoldierId { get; set; }

        public virtual FamilyMember FamilyMember { get; set; }
        public virtual Soldier Soldier { get; set; }
        
        public virtual FamilyRelationsEnum RelationToSoldier { get; set; }


    }
}