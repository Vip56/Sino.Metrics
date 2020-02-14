namespace Sino.Metrics.Formatters.Prometheus.Internal
{
    /// <summary>
    /// 标签
    /// </summary>
    public class LabelPair
    {
        public LabelPair() { }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public string Value { get; set; }
    }
}
