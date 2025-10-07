using Shared;
using System;
using System.Collections.Generic;
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
        public int Number { get; set; }
        public string Model { get; set; } = null!;
        public string Name { get; set; } = null!;
        public Status Status { get; set; }
        public Area Area { get; set; }
        public Location CurrentLocation { get; set; } = null!;
    }
}
