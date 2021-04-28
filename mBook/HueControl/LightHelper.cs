using System;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ControlHuePhilips
{
    public partial class LightHelper
    {
        public string ID { get; set; }
        [JsonProperty("state")]
        public State State { get; set; }
        [JsonProperty("swupdate")]
        public Swupdate Swupdate { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("modelid")]
        public string Modelid { get; set; }
        [JsonProperty("manufacturername")]
        public string Manufacturername { get; set; }
        [JsonProperty("capabilities")]
        public Capabilities Capabilities { get; set; }
        [JsonProperty("uniqueid")]
        public string Uniqueid { get; set; }
        [JsonProperty("swversion")]
        public string Swversion { get; set; }
        [JsonProperty("swconfigid")]
        public string Swconfigid { get; set; }
        [JsonProperty("productid")]
        public string Productid { get; set; }
    }
    public partial class Swupdate
    {
        [JsonProperty("state")]
        public string State { get; set; }
        [JsonProperty("lastinstall")]
        public string Lastinstall { get; set; }
    }
    public partial class State
    {
        [JsonProperty("on")]
        public bool On { get; set; }
        [JsonProperty("bri")]
        public long Bri { get; set; }
        [JsonProperty("hue")]
        public long Hue { get; set; }
        [JsonProperty("sat")]
        public long Sat { get; set; }
        [JsonProperty("effect")]
        public string Effect { get; set; }
        [JsonProperty("xy")]
        public List<double> Xy { get; set; }
        [JsonProperty("ct")]
        public long Ct { get; set; }
        [JsonProperty("alert")]
        public string Alert { get; set; }
        [JsonProperty("colormode")]
        public string Colormode { get; set; }
        [JsonProperty("mode")]
        public string Mode { get; set; }
        [JsonProperty("reachable")]
        public bool Reachable { get; set; }
    }
    public partial class Capabilities
    {
        [JsonProperty("streaming")]
        public Streaming Streaming { get; set; }
    }
    public partial class Streaming
    {
        [JsonProperty("renderer")]
        public bool Renderer { get; set; }
        [JsonProperty("proxy")]
        public bool Proxy { get; set; }
    }
    public partial class LightHelper
    {
        public static List<LightHelper> FromJson(string json)
        {
            var dict = JsonConvert.DeserializeObject<Dictionary<string, LightHelper>>(json, ConverterLight.Settings);
            var result = new List<LightHelper>();
            foreach (var item in dict)
            {
                item.Value.ID = item.Key;
                result.Add(item.Value);
            }
            return result;
        }
    }
    public static class SerializeLight
    {
        public static string ToJson(this LightHelper self) => JsonConvert.SerializeObject(self, ConverterLight.Settings);
    }
    public class ConverterLight
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
        };
    }
}
