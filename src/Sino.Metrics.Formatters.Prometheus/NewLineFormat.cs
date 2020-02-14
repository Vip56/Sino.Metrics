namespace Sino.Metrics.Formatters.Prometheus
{
    /// <summary>
    /// 换行格式
    /// </summary>
    public enum NewLineFormat
    {
        /// <summary>
        /// 使用Unix格式的换行作为默认
        /// </summary>
        Default,

        /// <summary>
        /// 使用Environement.NewLine环境变量
        /// </summary>
        Auto,

        /// <summary>
        /// 使用'\r\n'作为换行
        /// </summary>
        Windows,

        /// <summary>
        /// 使用'\r'作为换行
        /// </summary>
        Unix
    }
}
