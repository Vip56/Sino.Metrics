using Sino.Metrics.Formatters.Prometheus.Internal;
using System;

namespace Sino.Metrics.Formatters.Prometheus
{
    /// <summary>
    /// Prometheus格式化配置项
    /// </summary>
    public class MetricsPrometheusOptions
    {
        public MetricsPrometheusOptions()
        {
            MetricNameFormatter = PrometheusFormatterConstants.MetricNameFormatter;
        }

        public Func<string, string, string> MetricNameFormatter { get; set; }

        public NewLineFormat NewLineFormat { get; set; } = NewLineFormat.Default;
    }
}
