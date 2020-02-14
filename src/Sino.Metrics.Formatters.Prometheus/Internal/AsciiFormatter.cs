using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sino.Metrics.Formatters.Prometheus.Internal
{
    internal static class AsciiFormatter
    {
        private static readonly UTF8Encoding Encoding = new UTF8Encoding(false);

        public static async Task<string> Format(IEnumerable<MetricFamily> metrics, NewLineFormat newLine)
        {
            using (var memoryStream = new MemoryStream())
            {
                await Write(memoryStream, metrics, newLine);

                return Encoding.GetString(memoryStream.ToArray());
            }
        }

        public static async Task Write(Stream destination, IEnumerable<MetricFamily> metrics, NewLineFormat newLine)
        {
            var metricFamilies = metrics.ToArray();
            using (var streamWriter = new StreamWriter(destination, Encoding) { NewLine = GetNewLineChar(newLine) })
            {
                foreach (var metricFamily in metricFamilies)
                {
                    WriteFamily(streamWriter, metricFamily);
                }

                await streamWriter.FlushAsync();
            }
        }

        private static void WriteFamily(StreamWriter streamWriter, MetricFamily metricFamily)
        {
            streamWriter.Write("# HELP ");
            streamWriter.Write(metricFamily.Name);
            streamWriter.Write(' ');
            streamWriter.WriteLine(metricFamily.Help);

            streamWriter.Write("# TYPE ");
            streamWriter.Write(metricFamily.Name);
            streamWriter.Write(' ');
            streamWriter.WriteLine(ToString(metricFamily.Type));

            foreach (var metric in metricFamily.MetricsValues)
            {
                WriteMetric(streamWriter, metricFamily, metric);
                streamWriter.WriteLine();
            }
        }

        private static void WriteMetric(StreamWriter streamWriter, MetricFamily family, MetricsValue metric)
        {
            var familyName = family.Name;

            if (metric.Gauge != null)
            {
                WriteSimpleValue(streamWriter, familyName, metric.Gauge.Value, metric.Label);
            }
            else if (metric.Counter != null)
            {
                WriteSimpleValue(streamWriter, familyName, metric.Counter.Value, metric.Label);
            }
            else if (metric.Summary != null)
            {
                WriteSimpleValue(streamWriter, familyName, metric.Summary.Sample_Sum, metric.Label, "_sum");
                WriteSimpleValue(streamWriter, familyName, metric.Summary.Sample_Count, metric.Label, "_count");

                foreach (var quantileValuePair in metric.Summary.Quantile)
                {
                    var quantile = double.IsPositiveInfinity(quantileValuePair.QuantileValue) ? "+Inf" : quantileValuePair.QuantileValue.ToString(CultureInfo.InvariantCulture);

                    WriteSimpleValue(
                        streamWriter,
                        familyName,
                        quantileValuePair.Value,
                        metric.Label.Concat(new[] { new LabelPair { Name = "quantile", Value = quantile } }));
                }
            }
            else if (metric.Histogram != null)
            {
                WriteSimpleValue(streamWriter, familyName, metric.Histogram.Sample_Sum, metric.Label, "_sum");
                WriteSimpleValue(streamWriter, familyName, metric.Histogram.Sample_Count, metric.Label, "_count");
                foreach (var bucket in metric.Histogram.Buckets)
                {
                    var value = double.IsPositiveInfinity(bucket.Upper_Bound) ? "+Inf" : bucket.Upper_Bound.ToString(CultureInfo.InvariantCulture);

                    WriteSimpleValue(
                        streamWriter,
                        familyName,
                        bucket.Cumulative_Count,
                        metric.Label.Concat(new[] { new LabelPair { Name = "le", Value = value } }),
                        "_bucket");
                }
            }
            else
            {
                // not supported
            }
        }

        private static void WriteSimpleValue(StreamWriter writer, string family, double value, IEnumerable<LabelPair> labels, string namePostfix = null)
        {
            writer.Write(family);
            if (namePostfix != null)
            {
                writer.Write(namePostfix);
            }

            bool any = false;
            foreach (var l in labels)
            {
                writer.Write(any ? ',' : '{');

                writer.Write(l.Name);
                writer.Write("=\"");
                writer.Write(l.Value);
                writer.Write('"');

                any = true;
            }

            if (any)
            {
                writer.Write('}');
            }

            writer.Write(' ');
            writer.WriteLine(value.ToString(CultureInfo.InvariantCulture));
        }

        private static string GetNewLineChar(NewLineFormat newLine)
        {
            switch (newLine)
            {
                case NewLineFormat.Auto:
                    return Environment.NewLine;
                case NewLineFormat.Windows:
                    return "\r\n";
                case NewLineFormat.Unix:
                case NewLineFormat.Default:
                    return "\n";
                default:
                    throw new ArgumentOutOfRangeException(nameof(newLine), newLine, null);
            }
        }

        private static string ToString(MetricType type)
        {
            switch (type)
            {
                case MetricType.COUNTER:
                    return "counter";
                case MetricType.GAUGE:
                    return "gauge";
                case MetricType.SUMMARY:
                    return "summary";
                case MetricType.UNTYPED:
                    return "untyped";
                case MetricType.HISTOGRAM:
                    return "histogram";
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}
