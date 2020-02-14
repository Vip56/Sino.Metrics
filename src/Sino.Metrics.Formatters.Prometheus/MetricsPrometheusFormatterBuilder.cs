using Sino.Metrics.Formatters.Prometheus;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
