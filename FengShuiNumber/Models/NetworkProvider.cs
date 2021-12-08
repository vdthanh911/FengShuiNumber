using System;
using System.Collections.Generic;

#nullable disable

namespace FengShuiNumber.Models
{
    public partial class NetworkProvider
    {
        public NetworkProvider()
        {
            MobileNumbers = new HashSet<MobileNumber>();
            NetworkProviderPrefixes = new HashSet<NetworkProviderPrefix>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<MobileNumber> MobileNumbers { get; set; }
        public virtual ICollection<NetworkProviderPrefix> NetworkProviderPrefixes { get; set; }
    }
}
