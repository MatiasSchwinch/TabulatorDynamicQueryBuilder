using System;
using System.Collections.Generic;

namespace TabulatorDynamicQueryBuilder.Entities
{
    public partial class Login
    {
        public long LoginId { get; set; }
        public string? Uuid { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Salt { get; set; }
        public string? Md5 { get; set; }
        public string? Sha1 { get; set; }
        public string? Sha256 { get; set; }

        public virtual BasicDatum? BasicDatum { get; set; }
    }
}
