using App.Metrics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sino.Metrics.Formatters.Prometheus.Internal
{
    public static class MetricDataValueSourceExtensions
    {
        public static IEnumerable<MetricFamily> GetPrometheusMetricsSnapshot(
            this MetricsDataValueSource snapshot,
            Func<string, string, string> metricNameFormatter)
        {
            var result = new List<MetricFamily>();
            foreach (var group in snapshot.Contexts)
            {
                foreach (var metricGroup in group.ApdexScores.GroupBy(
                    source => source.IsMultidimensional ? source.MultidimensionalName : source.Name))
                {
                    var promMetricFamily = new MetricFamily
                    {
                        Name = metricNameFormatter(group.Context, metricGroup.Key),
                        Type = MetricType.GAUGE
                    };
                    foreach (var metric in metricGroup)
                    {
                        promMetricFamily.MetricsValues.AddRange(metric.ToPrometheusMetrics());
                    }

                    result.Add(promMetricFamily);
                }

                foreach (var metricGroup in group.Gauges.GroupBy(
                    source => source.IsMultidimensional ? source.MultidimensionalName : source.Name))
                {
                    var promMetricFamily = new MetricFamily
                    {
                        Name = metricNameFormatter(group.Context, metricGroup.Key),
                        Type = MetricType.GAUGE
                    };
                    foreach (var metric in metricGroup)
                    {
                        promMetricFamily.MetricsValues.AddRange(metric.ToPrometheusMetrics());
                    }

                    result.Add(promMetricFamily);
                }

                foreach (var metricGroup in group.Counters.GroupBy(
                    source => source.IsMultidimensional ? source.MultidimensionalName : source.Name))
                {
                    var promMetricFamily = new MetricFamily
                    {
                        Name = metricNameFormatter(group.Context, metricGroup.Key),
                        Type = MetricType.GAUGE
                    };

                    foreach (var metric in metricGroup)
                    {
                        promMetricFamily.MetricsValues.AddRange(metric.ToPrometheusMetrics());
                    }

                    result.Add(promMetricFamily);
                }

                foreach (var metricGroup in group.Meters.GroupBy(
                    source => source.IsMultidimensional ? source.MultidimensionalName : source.Name))
                {
                    var promMetricFamily = new MetricFamily
                    {
                        Name = metricNameFormatter(group.Context, $"{metricGroup.Key}_total"),
                        Type = MetricType.COUNTER
                    };

                    foreach (var metric in metricGroup)
                    {
                        promMetricFamily.MetricsValues.AddRange(metric.ToPrometheusMetrics());
                    }

                    result.Add(promMetricFamily);
                }

                foreach (var metricGroup in group.Histograms.GroupBy(
                    source => source.IsMultidimensional ? source.MultidimensionalName : source.Name))
                {
                    var promMetricFamily = new MetricFamily
                    {
                        Name = metricNameFormatter(group.Context, metricGroup.Key),
                        Type = MetricType.SUMMARY
                    };

                    foreach (var timer in metricGroup)
                    {
                        promMetricFamily.MetricsValues.AddRange(timer.ToPrometheusMetrics());
                    }

                    result.Add(promMetricFamily);
                }

                foreach (var metricGroup in group.Timers.GroupBy(
                    source => source.IsMultidimensional ? source.MultidimensionalName : source.Name))
                {
                    var promMetricFamily = new MetricFamily
                    {
                        Name = metricNameFormatter(group.Context, metricGroup.Key),
                        Type = MetricType.SUMMARY
                    };

                    foreach (var timer in metricGroup)
                    {
                        promMetricFamily.MetricsValues.AddRange(timer.ToPrometheusMetrics());
                    }

                    result.Add(promMetricFamily);
                }
            }

            return result;
        }
    }
}
