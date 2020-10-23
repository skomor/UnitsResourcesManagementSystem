using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aut3.Models
{
    public class Soldier
    {
        public Guid Id { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Pesel { get; set; }
        public bool Sex { get; set; }

    }
}
