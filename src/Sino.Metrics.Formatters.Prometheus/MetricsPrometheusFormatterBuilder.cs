using Sino.Metrics.Formatters.Prometheus;
using System;

namespace App.Metrics
{
    public static class MetricsPrometheusFormatterBuilder
    {
        /// <summary>
        /// 使用Prometheus格式化输出
        /// </summary>
        /// <param name="options">配置</param>
        /// <returns>返回IMetricsBuilder接口</returns>
        public static IMetricsBuilder AsPrometheusPlainText(
            this IMetricsOutputFormattingBuilder metricFormattingBuilder,
            MetricsPrometheusOptions options)
        {
            if (metricFormattingBuilder == null)
            {
                throw new ArgumentNullException(nameof(metricFormattingBuilder));
            }

            var formatter = new MetricsPrometheusTextOutputFormatter(options);

            return metricFormattingBuilder.Using(formatter, false);
        }

        public static IMetricsBuilder AsPrometheusPlainText(
            this IMetricsOutputFormattingBuilder metricFormattingBuilder,
            Action<MetricsPrometheusOptions> setupAction)
        {
            if (metricFormattingBuilder == null)
            {
                throw new ArgumentNullException(nameof(metricFormattingBuilder));
            }

            if (setupAction == null)
            {
                throw new ArgumentNullException(nameof(setupAction));
            }

            var options = new MetricsPrometheusOptions();

            setupAction.Invoke(options);

            var formatter = new MetricsPrometheusTextOutputFormatter(options);

            return metricFormattingBuilder.Using(formatter, false);
        }

        public static IMetricsBuilder AsPrometheusPlainText(
           this IMetricsOutputFormattingBuilder metricFormattingBuilder)
        {
            if (metricFormattingBuilder == null)
            {
                throw new ArgumentNullException(nameof(metricFormattingBuilder));
            }

            var formatter = new MetricsPrometheusTextOutputFormatter();

            return metricFormattingBuilder.Using(formatter, false);
        }
    }
}
