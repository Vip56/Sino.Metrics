using App.Metrics;
using App.Metrics.Formatters;
using Sino.Metrics.Formatters.Prometheus.Internal;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Sino.Metrics.Formatters.Prometheus
{
    public class MetricsPrometheusTextOutputFormatter : IMetricsOutputFormatter
    {
        private readonly MetricsPrometheusOptions _options;

        public MetricsPrometheusTextOutputFormatter()
            :this(new MetricsPrometheusOptions())
        {
        }

        public MetricsPrometheusTextOutputFormatter(MetricsPrometheusOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public MetricsMediaTypeValue MediaType => new MetricsMediaTypeValue("text", "vnd.appmetrics.metrics.prometheus", "v1", "plain");

        public async Task WriteAsync(
            Stream output,
            MetricsDataValueSource metricsData,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (output == null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            var prometheusMetricsSnapshot = metricsData.GetPrometheusMetricsSnapshot(_options.MetricNameFormatter);

            await AsciiFormatter.Write(output, prometheusMetricsSnapshot, _options.NewLineFormat);
        }
    }
}
