using System;
using System.Collections.Generic;

namespace TabulatorDynamicQueryBuilder.Entities
{
    public partial class Coordinate
    {
        public long CoordinatesId { get; set; }
        public string Latitude { get; set; } = null!;
        public string Longitude { get; set; } = null!;

        public virtual Location? Location { get; set; }
    }
}
