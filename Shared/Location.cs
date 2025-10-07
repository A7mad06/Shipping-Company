using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    [Owned]
    public class Location
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
