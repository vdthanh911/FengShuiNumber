using System;
using System.Collections.Generic;

#nullable disable

namespace FengShuiNumber.Models
{
    public partial class MobileNumber
    {
        public int Id { get; set; }
        public string MobileNumber1 { get; set; }
        public int? NetworkProviderId { get; set; }

        public virtual NetworkProvider NetworkProvider { get; set; }
    }
}
