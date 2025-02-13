using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using NnUtils.Scripts.UI.Slideshow;
using UnityEngine;

namespace NnUtils.Modules.JSONUtils.Scripts.Types.Components.UI.Image
{
    /// This class is used as a bridge between <see cref="Slideshow"/> and JSON
    [Serializable]
    public class ConfigSlideshow : ConfigComponent
    {
        [JsonProperty] public ConfigImage[] Images;
        [JsonProperty] public ConfigSlideshowTransition[] Transitions;
        
        [Tooltip("Whether data type defaults will be used if partially defined object is found in JSON")] [JsonIgnore]
        public bool UseDataDefaults = true;

        /// Resets values to data defaults overwriting custom defined defaults if data is found in the config
        [OnDeserializing]
        private void OnDeserializing(StreamingContext context)
        {
            if (!UseDataDefaults) return;
            Images = Array.Empty<ConfigImage>();
            Transitions = Array.Empty<ConfigSlideshowTransition>();
        }
        
        public ConfigSlideshow() : this(Array.Empty<ConfigImage>(), Array.Empty<ConfigSlideshowTransition>()) { }

        public ConfigSlideshow(ConfigImage[] images, ConfigSlideshowTransition[] transitions)
        {
            Images = images;
            Transitions = transitions;
        }
        
    }
}