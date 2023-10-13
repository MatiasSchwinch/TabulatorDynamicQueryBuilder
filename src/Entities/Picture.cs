using System;
using System.Collections.Generic;

namespace TabulatorDynamicQueryBuilder.Entities
{
    public partial class Picture
    {
        public long PictureId { get; set; }
        public string? Large { get; set; }
        public string? Medium { get; set; }
        public string? Thumbnail { get; set; }

        public virtual BasicDatum? BasicDatum { get; set; }
    }
}
