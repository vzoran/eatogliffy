using EaToGliffy.Gliffy.Model.Graphics;
using Newtonsoft.Json;

namespace EaToGliffy.Gliffy.Model
{
    public class GliffyObject
    {
        [JsonProperty(PropertyName = "x")]
        public int XPos { get; set; }

        [JsonProperty(PropertyName = "y")]
        public int YPos { get; set; }

        [JsonProperty(PropertyName = "rotation")]
        public int Rotation { get; set; }

        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "width")]
        public int Width { get; set; }

        [JsonProperty(PropertyName = "height")]
        public int Height { get; set; }

        [JsonProperty(PropertyName = "order")]
        public string Order { get; set; }

        [JsonProperty(PropertyName = "lockAspectRatio")]
        public bool LockAspectRatio { get; set; }

        [JsonProperty(PropertyName = "lockShape")]
        public bool LockShape { get; set; }

        [JsonProperty(PropertyName = "uid")]
        public string Uid { get; set; }

        [JsonProperty(PropertyName = "hidden")]
        public bool Hidden { get; set; }

        [JsonProperty(PropertyName = "layerId")]
        public string LayerId { get; set; }

        [JsonProperty(PropertyName = "graphic")]
        public GliffyGraphic Graphic { get; set; }
    }
}
