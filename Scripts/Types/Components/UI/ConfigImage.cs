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
        [JsonProperty] public List<string> Images;
        [JsonProperty] public ConfigColor Color;
        [JsonProperty] public bool Raycast;
        [JsonProperty] public ConfigVector4 RaycastPadding;
        [JsonProperty] public bool Maskable;
        [JsonProperty] public bool Envelope;
        [JsonProperty] public bool Interactive; 

        [Tooltip("Whether data type defaults will be used if partially defined object is found in JSON")] [JsonIgnore]
        public bool UseDataDefaults = true;

        /// Resets values to data defaults overwriting custom defined defaults if data is found in the config
        [OnDeserializing]
        private void OnDeserializing(StreamingContext context)
        {
            if (!UseDataDefaults) return;
            Image    = "";
            Images   = new();
            Color    = UnityEngine.Color.white;
            Raycast  = true;
            Maskable = true;
            Interactive = true;
        }

        public ConfigImage() :
            this("", new(), UnityEngine.Color.white, true, new(), true, false) { }

        public ConfigImage(UnityEngine.UI.Image image)
        {
            Image          = image.sprite.name;
            Images         = new();
            Color          = image.color;
            Raycast        = image.raycastTarget;
            RaycastPadding = image.raycastPadding;
            Maskable       = image.maskable;
            Envelope       = false;
            Interactive    = false;
        }

        public ConfigImage(string image = "", List<string> images = null, Color color = default, bool raycast = true,
            Vector4 raycastPadding = default, bool maskable = true, bool envelope = false, bool interactive = true)
        {
            Image          = image;
            Images         = images;
            Color          = color;
            Raycast        = raycast;
            RaycastPadding = raycastPadding;
            Maskable       = maskable;
            Envelope       = envelope;
            Interactive    = interactive;
        }

        public UnityEngine.UI.Image UpdateImage(UnityEngine.UI.Image img)
        {
            var sp = Misc.SpriteFromFile(Images.Count == 0 ? Image : Images[0]);
            img.sprite         = sp == null ? Sprite.Create(new(1, 1), new(0, 0, 1, 1), new(0.5f, 0.5f)) : sp;
            img.color          = Color;
            img.raycastTarget  = Raycast;
            img.raycastPadding = RaycastPadding;
            img.maskable       = Maskable;
            return img;
        }

        public override void AddComponent(GameObject go)
        {
            UpdateImage(Envelope ? go.AddComponent<EnvelopedImage>() : go.AddComponent<UnityEngine.UI.Image>());
            
            if (!Interactive) return;
            var ii = go.AddComponent<InteractiveImageScript>();
            ii.LoadData(this);
        }

        public bool Equals(ConfigImage other)
        {
            return Image == other.Image &&
                   Equals(Images, other.Images) &&
                   Equals(Color, other.Color) &&
                   Raycast == other.Raycast &&
                   Equals(RaycastPadding, other.RaycastPadding) &&
                   Maskable == other.Maskable &&
                   Envelope == other.Envelope &&
                   Interactive == other.Interactive &&
                   UseDataDefaults == other.UseDataDefaults;
        }
    }
}