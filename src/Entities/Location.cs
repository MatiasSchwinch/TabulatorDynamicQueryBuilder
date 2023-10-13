using System;
using System.Collections.Generic;

namespace TabulatorDynamicQueryBuilder.Entities
{
    public partial class Location
    {
        public long LocationId { get; set; }
        public long? CoordinatesId { get; set; }
        public long? TimezoneId { get; set; }
        public long StreetNumber { get; set; }
        public string? StreetName { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? Postcode { get; set; }

        public virtual Coordinate? Coordinates { get; set; }
        public virtual Timezone? Timezone { get; set; }
        public virtual BasicDatum? BasicDatum { get; set; }
    }
}
