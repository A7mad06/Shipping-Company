using Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public enum Status
    {
        Busy, Available
    };

    public enum Area
    {
        Cairo,
        Giza,
        Alexandria,
        Dakahlia,
        RedSea,
        Beheira,
        Fayoum,
        Gharbia,
        Ismailia,
        Menofia,
        Minya,
        Qaliubiya,
        NewValley,
        Suez,
        Aswan,
        Assiut,
        BeniSuef,
        PortSaid,
        Damietta,
        Sharkia,
        SouthSinai,
        KafrElSheikh,
        Matruh,
        Luxor,
        Qena,
        NorthSinai,
        Sohag
    }
    public class Vehicle
    {
        [Key]
        public int Number { get; set; }
        [Required]
        public string Model { get; set; } = null!;
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public Status Status { get; set; }
        [Required]
        public Area Area { get; set; }
        public Location? CurrentLocation { get; set; }
    }
}
