using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using NnUtils.Scripts;
using NnUtils.Scripts.UI;
using Scripts.InteractiveComponents;
using UnityEngine;
using Color = UnityEngine.Color;

namespace NnUtils.Modules.JSONUtils.Scripts.Types.Components.UI
{
    /// This class is used as a bridge between <see cref="UnityEngine.UI.Image"/> and JSON <br/>
    [Serializable]
    public class ConfigImage : ConfigComponent
    {
        [JsonProperty] public string Image;
        [JsonProperty] public ConfigColor Color;
        [JsonProperty] public bool Raycast;
        [JsonProperty] public ConfigVector4 RaycastPadding;
        [JsonProperty] public bool Maskable;
        [JsonProperty] public bool Envelope;

        [Tooltip("Whether data type defaults will be used if partially defined object is found in JSON")] [JsonIgnore]
        public bool UseDataDefaults = true;

        /// Resets values to data defaults overwriting custom defined defaults if data is found in the config
        [OnDeserializing]
        private void OnDeserializing(StreamingContext context)
        {
            if (!UseDataDefaults) return;
            Image    = "";
            Color    = UnityEngine.Color.white;
            Raycast  = true;
            Maskable = true;
        }

        public ConfigImage() :
            this("", UnityEngine.Color.white, true, new(), true, false) { }

        public ConfigImage(UnityEngine.UI.Image image)
        {
            Image          = image.sprite.name;
            Color          = image.color;
            Raycast        = image.raycastTarget;
            RaycastPadding = image.raycastPadding;
            Maskable       = image.maskable;
            Envelope       = false;
        }

        public ConfigImage(string image = "", Color color = default, bool raycast = true,
           Vector4 raycastPadding = default, bool maskable = true, bool envelope = false)
        {
            Image          = image;
            Color          = color;
            Raycast        = raycast;
            RaycastPadding = raycastPadding;
            Maskable       = maskable;
            Envelope       = envelope;
        }

        public UnityEngine.UI.Image UpdateImage(UnityEngine.UI.Image img)
        {
            var sp = Misc.SpriteFromFile(Image);
            img.sprite         = sp ? sp : Sprite.Create(new(1, 1), new(0, 0, 1, 1), new(0.5f, 0.5f));
            img.color          = Color;
            img.raycastTarget  = Raycast;
            img.raycastPadding = RaycastPadding;
            img.maskable       = Maskable;
            return img;
        }

        public override void AddComponent(GameObject go)
        {
            var ii = go.AddComponent<InteractiveImageScript>();
            ii.LoadData(this);
        }

        public bool Equals(ConfigImage other)
        {
            return Image == other.Image &&
                   Equals(Color, other.Color) &&
                   Raycast == other.Raycast &&
                   Equals(RaycastPadding, other.RaycastPadding) &&
                   Maskable == other.Maskable &&
                   Envelope == other.Envelope &&
                   UseDataDefaults == other.UseDataDefaults;
        }
    }
}