using System;
using System.Collections.Generic;

namespace TabulatorDynamicQueryBuilder.Entities
{
    public partial class Registered
    {
        public long RegisteredId { get; set; }
        public byte[] Date { get; set; } = null!;
        public long Age { get; set; }

        public virtual BasicDatum? BasicDatum { get; set; }
    }
}
