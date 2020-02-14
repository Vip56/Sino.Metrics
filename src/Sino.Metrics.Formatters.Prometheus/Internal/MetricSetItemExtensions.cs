using App.Metrics.Counter;
using App.Metrics.Meter;

namespace Sino.Metrics.Formatters.Prometheus.Internal
{
    public static class MetricSetItemExtensions
    {
        public static MetricsValue ToPrometheusMetric(this CounterValue.SetItem item)
        {
            var result = new MetricsValue
            {
                Gauge = new Gauge
                {
                    Value = item.Count
                },
                Label = item.Tags.ToLabelPairs()
            };

            return result;
        }

        public static MetricsValue ToPrometheusMetric(this MeterValue.SetItem item)
        {
            var result = new MetricsValue
            {
                Counter = new Counter
                {
                    Value = item.Value.Count
                },
                Label = item.Tags.ToLabelPairs()
            };

            return result;
        }
    }
}
