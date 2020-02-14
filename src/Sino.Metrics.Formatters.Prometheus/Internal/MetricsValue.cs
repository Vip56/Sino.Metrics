using System.Collections.Generic;

namespace Sino.Metrics.Formatters.Prometheus.Internal
{
    /// <summary>
    /// 指标数据
    /// </summary>
    public class MetricsValue
    {
        public MetricsValue() { }

        /// <summary>
        /// 标签
        /// </summary>
        public List<LabelPair> Label { get; set; }

        public Gauge Gauge { get; set; }

        public Counter Counter { get; set; }

        public Summary Summary { get; set; }

        public Untyped Untyped { get; set; }

        public Histogram Histogram { get; set; }

        public long TimeStamp { get; set; }
    }
}
