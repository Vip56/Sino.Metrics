using System;
using System.Text.RegularExpressions;

namespace Sino.Metrics.Formatters.Prometheus.Internal
{
    public static class PrometheusFormatterConstants
    {
        public static readonly Func<string, string, string> MetricNameFormatter =
            (metricContext, metricName) => string.IsNullOrWhiteSpace(metricContext)
                ? MetricNameRegex.Replace(metricName, "_").ToLowerInvariant()
                : MetricNameRegex.Replace($"{metricContext}_{metricName}", "_").ToLowerInvariant();

        private static readonly Regex MetricNameRegex = new Regex("[^a-z0-9A-Z:_]");
    }
}
