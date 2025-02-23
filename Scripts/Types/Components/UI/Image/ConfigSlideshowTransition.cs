using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NnUtils.Scripts;
using NnUtils.Scripts.UI.Slideshow;
using UnityEngine;

namespace NnUtils.Modules.JSONUtils.Scripts.Types.Components.UI.Image
{
    // TODO: Possibly implement bezier curves, not sure about this yet
    /// This class is used as a bridge between <see cref="SlideshowTransition"/> and JSON
    [Serializable]
    public class ConfigSlideshowTransition
    {
        // Can't simply name it Type due to C# stuff 0)
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("Type")] public SlideshowTransitionType TransitionType;
        [JsonProperty] public float Duration;
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty] public Easings.Type Easing;
        
        [Tooltip("Whether data type defaults will be used if partially defined object is found in JSON")] [JsonIgnore]
        public bool UseDataDefaults = true;
        
        /// Resets values to data defaults overwriting custom defined defaults if data is found in the config
        [OnDeserializing]
        private void OnDeserializing(StreamingContext context)
        {
            if (!UseDataDefaults) return;
            TransitionType = SlideshowTransitionType.None;
            Duration = 0;
            Easing = Easings.Type.None;
        }

        public ConfigSlideshowTransition() : this(SlideshowTransitionType.None, 0, Easings.Type.None) { }

        public ConfigSlideshowTransition(SlideshowTransition transition)
        {
            TransitionType = transition.TransitionType;
            Duration = transition.Duration;
            Easing = transition.Easing;
        }
        
        public ConfigSlideshowTransition(SlideshowTransitionType type = SlideshowTransitionType.None, float duration = 0,
            Easings.Type easing = Easings.Type.None)
        {
            TransitionType = type;
            Duration = duration;
            Easing = easing;
        }

        public static implicit operator SlideshowTransition(ConfigSlideshowTransition transition) =>
            new(transition.TransitionType, transition.Duration, transition.Easing);
        
        public static implicit operator ConfigSlideshowTransition(SlideshowTransition transition) =>
            new(transition);
    }
}