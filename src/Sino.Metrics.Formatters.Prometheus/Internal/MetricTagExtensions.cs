using App.Metrics;
using System.Collections.Generic;

namespace Sino.Metrics.Formatters.Prometheus.Internal
{
    public static class MetricTagExtensions
    {
        public static List<LabelPair> ToLabelPairs(this MetricTags tags)
        {
            var result = new List<LabelPair>(tags.Count);
            for (var i = 0; i < tags.Count; i++)
            {
                result.Add(new LabelPair { Name = tags.Keys[i], Value = tags.Values[i] });
            }

            return result;
        }
    }
}
