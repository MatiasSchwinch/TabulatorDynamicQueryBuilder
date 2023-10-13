using System;
using System.Collections.Generic;

namespace TabulatorDynamicQueryBuilder.Entities
{
    public partial class Timezone
    {
        public long TimezoneId { get; set; }
        public string? Offset { get; set; }
        public string? Description { get; set; }

        public virtual Location? Location { get; set; }
    }
}
