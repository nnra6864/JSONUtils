using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using NnUtils.Scripts;
using UnityEngine;
using Color = UnityEngine.Color;

namespace NnUtils.Modules.JSONUtils.Scripts.Types.Components.UI.Image
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
            img.sprite         = sp;
            img.color          = Color;
            img.raycastTarget  = Raycast;
            img.raycastPadding = RaycastPadding;
            img.maskable       = Maskable;
            return img;
        }

        public override void AddComponent(GameObject go)
        {
            var interactiveImage = go.AddComponent<InteractiveImageScript>();
            interactiveImage.LoadImage(this);
        }
    }
}