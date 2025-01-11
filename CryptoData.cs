using System;
using Newtonsoft.Json;

public class CryptoData {
    
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("current_price")]
    public double CurrentPrice { get; set; }
}