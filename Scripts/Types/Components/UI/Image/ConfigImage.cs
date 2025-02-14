using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NnUtils.Scripts;
using NnUtils.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;
using Color = UnityEngine.Color;

namespace NnUtils.Modules.JSONUtils.Scripts.Types.Components.UI.Image
{
    /// This class is used as a bridge between <see cref="InteractiveImageScript"/> and JSON
    [Serializable]
    public class ConfigImage : ConfigComponent
    {
        [JsonProperty] public string Image;
        [JsonProperty] public ConfigColor Color;
        [JsonProperty] public bool Raycast;
        [JsonProperty] public ConfigVector4 RaycastPadding;
        [JsonProperty] public bool Maskable;
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty] public AspectRatioFitter.AspectMode ScalingMode;

        [Tooltip("Whether data type defaults will be used if partially defined object is found in JSON")] [JsonIgnore]
        public bool UseDataDefaults = true;

        /// Resets values to data defaults overwriting custom defined defaults if data is found in the config
        [OnDeserializing]
        private void OnDeserializing(StreamingContext context)
        {
            if (!UseDataDefaults) return;
            Image       = "";
            Color       = UnityEngine.Color.white;
            Raycast     = true;
            Maskable    = true;
            ScalingMode = AspectRatioFitter.AspectMode.None;
        }

        public ConfigImage() :
            this("", UnityEngine.Color.white, true, new(), true, AspectRatioFitter.AspectMode.None) { }

        public ConfigImage(UnityEngine.UI.Image image)
        {
            Image          = image.sprite.name;
            Color          = image.color;
            Raycast        = image.raycastTarget;
            RaycastPadding = image.raycastPadding;
            Maskable       = image.maskable;
            ScalingMode    = AspectRatioFitter.AspectMode.None;
        }

        public ConfigImage(string image = "", Color color = default, bool raycast = true,
            Vector4 raycastPadding = default, bool maskable = true, AspectRatioFitter.AspectMode scalingMode = AspectRatioFitter.AspectMode.None)
        {
            Image          = image;
            Color          = color;
            Raycast        = raycast;
            RaycastPadding = raycastPadding;
            Maskable       = maskable;
            ScalingMode    = scalingMode;
        }

        public override void AddComponent(GameObject go)
        {
            var interactiveImage = go.AddComponent<InteractiveImageScript>();
            interactiveImage.LoadData(Image, Color, Raycast, RaycastPadding, Maskable, ScalingMode);
        }
    }
}