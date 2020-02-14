namespace Sino.Metrics.Formatters.Prometheus.Internal
{
    public class Bucket
    {
        public Bucket() { }

        public ulong Cumulative_Count { get; set; }

        public double Upper_Bound { get; set; }
    }
}
