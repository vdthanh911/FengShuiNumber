using System;
using System.Collections.Generic;

#nullable disable

namespace FengShuiNumber.Models
{
    public partial class NetworkProviderPrefix
    {
        public int Id { get; set; }
        public string Prefix { get; set; }
        public int? NetworkProviderId { get; set; }

        public virtual NetworkProvider NetworkProvider { get; set; }
    }
}
