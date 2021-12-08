using System;
using System.Collections.Generic;
using System.Text;

namespace FengShuiNumber.ModelResponses.FengShuiConfigurationModel
{
    public class CriteriaModel
    {
        public TotalModel Total { get; set; }
        public List<int> LastNicePairOfNumbers { get; set; }
    }
}
