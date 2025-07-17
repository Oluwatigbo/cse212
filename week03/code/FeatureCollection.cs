using System.Collections.Generic;
using System.Text.Json.Serialization;

public class FeatureCollection
{
    [JsonPropertyName("features")]
    public List<Feature> Features { get; set; } = new List<Feature>();
}

public class Feature
{
    [JsonPropertyName("properties")]
    public Properties Properties { get; set; } = new Properties();
}

public class Properties
{
    [JsonPropertyName("place")]
    public string Place { get; set; } = string.Empty;

    [JsonPropertyName("mag")]
    public double Mag { get; set; }
}
