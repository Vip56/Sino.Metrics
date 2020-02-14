namespace Sino.Metrics.Formatters.Prometheus.Internal
{
    /// <summary>
    /// 指标类型
    /// </summary>
    public enum MetricType
    {
        COUNTER = 0,
        GAUGE = 1,
        SUMMARY = 2,
        UNTYPED = 3,
        HISTOGRAM = 4
    }
}
