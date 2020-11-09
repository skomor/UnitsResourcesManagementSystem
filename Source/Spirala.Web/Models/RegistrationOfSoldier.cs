using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aut3.Models
{
    public class RegistrationOfSoldier
    {

        public Guid RegistrationOfSoldierId { get; set; }
        public string Place { get; set; }
        public string Notes { get; set; }
        [Column(TypeName="Date")]
        public DateTime DateOfRegistration { get; set; }
        public MilitaryUnit Unit { get; set; }

        public Guid SoldierId { get; set;  }
        public Soldier Soldier { get; set; }

    }
}