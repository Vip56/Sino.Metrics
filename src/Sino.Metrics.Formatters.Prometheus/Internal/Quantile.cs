namespace Sino.Metrics.Formatters.Prometheus.Internal
{
    /// <summary>
    /// 分位数
    /// </summary>
    public class Quantile
    {
        public Quantile() { }

        /// <summary>
        /// 分位数
        /// </summary>
        public double QuantileValue { get; set; }

        /// <summary>
        /// 该分位数值
        /// </summary>
        public double Value { get; set; }
    }
}
