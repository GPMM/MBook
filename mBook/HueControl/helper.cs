using System;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json;
using ControlHuePhilips;

namespace HueTestForms
{
    public partial class ColorLightHelper
    {
        [JsonProperty("transitiontime")]
        public int Transitiontime { get; set; }
        [JsonProperty("on")]
        public bool On { get; set; }
        [JsonProperty("sat")]
        public long Sat { get; set; }
        [JsonProperty("bri")]
        public long Bri { get; set; }
        [JsonProperty("hue")]
        public long Hue { get; set; }
        public ColorLightHelper(int transitiontime, bool on, long sat, long bri, long hue)
        {
            Transitiontime = transitiontime;
            On = on;
            Sat = sat;
            Bri = bri;
            Hue = hue;
        }
    }
    public partial class ColorLightHelper
    {
        public static ColorLightHelper FromJson(string json) => JsonConvert.DeserializeObject<ColorLightHelper>(json, Converter.Settings);
    }
    public static class SerializeColorLightHelper
    {
        public static string ToJson(this ColorLightHelper self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }
    public class ConverterColorLightHelper
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
        };
    }
}
