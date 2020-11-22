using System;
using System.ComponentModel.DataAnnotations.Schema;
using Aut3.Models.VehiclesEnums;
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
        public string Brand  { get; set; }
        public string Model  { get; set; }
        public string LicensePlate  { get; set; }
        public VehicleTypeEnum CarType { get; set; }
        public TransmissionGearTypeEnum  TransmissionConfig { get; set; }
        public FuelTypeEnum FuelConfig { get; set; }
        [Column(TypeName="Date")]
        public DateTime DateOfProduction { get; set; }
        
        public int EngineCapacityCC { get; set; }
        public int WeightKg { get; set; }
        public int PowerOutputHP { get; set; }
        
        public MilitaryUnit CurrUnit { get; set; }
        
        public Guid SoldierId { get; set;  }
        public Soldier Owner { get; set; }

        /*public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string WhoEdited { get; set; }*/

    }
}