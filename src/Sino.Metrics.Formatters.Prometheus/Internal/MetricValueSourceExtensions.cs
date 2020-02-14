using App.Metrics;
using App.Metrics.Apdex;
using App.Metrics.Counter;
using App.Metrics.Gauge;
using App.Metrics.Histogram;
using App.Metrics.Meter;
using App.Metrics.Timer;
using System.Collections.Generic;
using System.Linq;

namespace Sino.Metrics.Formatters.Prometheus.Internal
{
    public static class MetricValueSourceExtensions
    {
        public static IEnumerable<MetricsValue> ToPrometheusMetrics(this ApdexValueSource metric)
        {
            var result = new List<MetricsValue>
                         {
                             new MetricsValue
                             {
                                 Gauge = new Gauge
                                         {
                                             Value = metric.Value.Score
                                         },
                                 Label = metric.Tags.ToLabelPairs()
                             }
                         };

            return result;
        }

        public static IEnumerable<MetricsValue> ToPrometheusMetrics(this GaugeValueSource metric)
        {
            var result = new List<MetricsValue>
                         {
                             new MetricsValue
                             {
                                 Gauge = new Gauge
                                         {
                                             Value = metric.Value
                                         },
                                 Label = metric.Tags.ToLabelPairs()
                             }
                         };

            return result;
        }

        public static IEnumerable<MetricsValue> ToPrometheusMetrics(this CounterValueSource metric)
        {
            IEnumerable<MetricsValue> items = null;

            // when We Resting counter, we lost Items "Count"
            if (metric.Value.Items?.Length > 0)
            {
                items = metric.Value.Items.Select(i => i.ToPrometheusMetric());
            }

            var result = new List<MetricsValue>
                         {
                             new MetricsValue
                             {
                                 Gauge = new Gauge
                                         {
                                            Value = metric.ValueProvider.GetValue(metric.ResetOnReporting).Count
                                         },
                                 Label = metric.Tags.ToLabelPairs()
                             }
                         };

            if (metric.Value.Items?.Length > 0 && items != null)
            {
                result.AddRange(items);
            }

            return result;
        }

        public static IEnumerable<MetricsValue> ToPrometheusMetrics(this MeterValueSource metric)
        {
            var result = new List<MetricsValue>
                         {
                             new MetricsValue
                             {
                                 Counter = new Counter
                                           {
                                               Value = metric.Value.Count
                                           },
                                 Label = metric.Tags.ToLabelPairs()
                             }
                         };

            if (metric.Value.Items?.Length > 0)
            {
                result.AddRange(metric.Value.Items.Select(x => x.ToPrometheusMetric()));
            }

            return result;
        }

        public static IEnumerable<MetricsValue> ToPrometheusMetrics(this HistogramValueSource metric)
        {
            var result = new List<MetricsValue>
                         {
                             new MetricsValue
                             {
                                 Summary = new Summary
                                           {
                                               Sample_Count = (ulong)metric.Value.Count,
                                               Sample_Sum = metric.Value.Sum,
                                               Quantile =
                                               {
                                                   new Quantile { QuantileValue = 0.5, Value = metric.Value.Median },
                                                   new Quantile { QuantileValue = 0.75, Value = metric.Value.Percentile75 },
                                                   new Quantile { QuantileValue = 0.95, Value = metric.Value.Percentile95 },
                                                   new Quantile { QuantileValue = 0.99, Value = metric.Value.Percentile99 }
                                               }
                                           },
                                 Label = metric.Tags.ToLabelPairs()
                             }
                         };

            return result;
        }

        public static IEnumerable<MetricsValue> ToPrometheusMetrics(this TimerValueSource metric)
        {
            // Prometheus advocates always using seconds as a base unit for time
            var rescaledVal = metric.Value.Scale(TimeUnit.Seconds, TimeUnit.Seconds);
            var result = new List<MetricsValue>
                         {
                             new MetricsValue
                             {
                                 Summary = new Summary
                                           {
                                               Sample_Count = (ulong)rescaledVal.Rate.Count,
                                               Sample_Sum = rescaledVal.Histogram.Sum,
                                               Quantile =
                                               {
                                                   new Quantile { QuantileValue = 0.5, Value = rescaledVal.Histogram.Median },
                                                   new Quantile { QuantileValue = 0.75, Value = rescaledVal.Histogram.Percentile75 },
                                                   new Quantile { QuantileValue = 0.95, Value = rescaledVal.Histogram.Percentile95 },
                                                   new Quantile { QuantileValue = 0.99, Value = rescaledVal.Histogram.Percentile99 }
                                               }
                                           },
                                 Label = metric.Tags.ToLabelPairs()
                             }
                         };

            return result;
        }
    }
}
