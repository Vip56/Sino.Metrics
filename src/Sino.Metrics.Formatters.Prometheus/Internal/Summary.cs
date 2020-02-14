using System.Collections.Generic;

namespace Sino.Metrics.Formatters.Prometheus.Internal
{
    public class Summary
    {
        public Summary()
        {
            Quantile = new List<Quantile>();
        }

        public ulong Sample_Count { get; set; }

        public double Sample_Sum { get; set; }

        public List<Quantile> Quantile { get; }
    }
}
