using System.Collections.Generic;

namespace Sino.Metrics.Formatters.Prometheus.Internal
{
    /// <summary>
    /// 指标模型
    /// </summary>
    public class MetricFamily
    {
        public MetricFamily()
        {
            Type = MetricType.COUNTER;
            MetricsValues = new List<MetricsValue>();
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 辅助信息
        /// </summary>
        public string Help { get; set; }

        /// <summary>
        /// 指标类型
        /// </summary>
        public MetricType Type { get; set; }

        /// <summary>
        /// 指标值
        /// </summary>
        public List<MetricsValue> MetricsValues { get; }
    }
}
