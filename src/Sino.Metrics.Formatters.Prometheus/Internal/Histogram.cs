using System.Collections.Generic;

namespace Sino.Metrics.Formatters.Prometheus.Internal
{
    public class Histogram
    {
        public Histogram()
        {
            Buckets = new List<Bucket>();
        }

        public ulong Sample_Count { get; set; }

        public double Sample_Sum { get; set; }

        public List<Bucket> Buckets { get; }
    }
}
