using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aut3.Models.VehiclesEnums;
using Microsoft.AspNet.OData.Builder;
using Microsoft.OData.Edm;

namespace Aut3.Models
{
    public class Vehicle
    {
        /*
        public long SId { get; set; }
        */
        
        public Guid VehicleId { get; set; }
        public string Vin { get; set; }
        [Required]
        public string Brand  { get; set; }
        public string Model  { get; set; }
        public string LicensePlate  { get; set; }
        [Range(0, 9)]
        public int CarType { get; set; }
        [Range(0, 1)]

        public int  TransmissionConfig { get; set; }
        [Range(0, 6)]

        public int FuelConfig { get; set; }
        [Column(TypeName="Date")]
        public DateTime DateOfProduction { get; set; }
        
        public int EngineCapacityCC { get; set; }
        public int WeightKg { get; set; }
        public int PowerOutputHP { get; set; }
        
        [AutoExpand]
        public MilitaryUnit CurrUnit { get; set; }
        public Guid CurrUnitID { get; set; }

        
        public Guid SoldierId { get; set;  }
        public Soldier Owner { get; set; }

        /*public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string WhoEdited { get; set; }*/

    }
}