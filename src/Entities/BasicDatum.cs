using System;
using System.Collections.Generic;

namespace TabulatorDynamicQueryBuilder.Entities
{
    public partial class BasicDatum
    {
        public long PersonId { get; set; }
        public long? LocationId { get; set; }
        public long? LoginId { get; set; }
        public long? RegisteredId { get; set; }
        public long? PictureId { get; set; }
        public string? Title { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public long Gender { get; set; }
        public DateTime? Date { get; set; } = null!;
        public long Age { get; set; }
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Cell { get; set; }
        public string? Nationality { get; set; }

        public virtual Location? Location { get; set; }
        public virtual Login? Login { get; set; }
        public virtual Picture? Picture { get; set; }
        public virtual Registered? Registered { get; set; }
    }
}
