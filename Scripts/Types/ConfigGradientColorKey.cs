using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using UnityEngine;

namespace NnUtils.Modules.JSONUtils.Scripts.Types
{
    /// This class is used as a bridge between <see cref="GradientColorKey"/> and JSON <br/>
    [Serializable]
    public class ConfigGradientColorKey
    {
        public ConfigColor Color;
        public float Time;

        [JsonIgnore]
        [Tooltip("Whether data type defaults will be used if partially defined object is found in JSON")]
        public bool UseDataDefaults = true;
        
        /// Resets values to data defaults overwriting custom defined defaults if data is found in the config
        [OnDeserializing]
        private void OnDeserializing(StreamingContext context)
        {
            if (!UseDataDefaults) return;
            Color = new(); Time  = 0;
        }
        
        /// Default ctor
        public ConfigGradientColorKey() : this(new(), 0) { }

        /// Ctor from <see cref="GradientAlphaKey"/>
        public ConfigGradientColorKey(GradientColorKey key) : this(key.color, key.time) { }

        /// Ctor from values
        public ConfigGradientColorKey(ConfigColor color, float time)
        {
            Color = color;
            Time  = time;
        }

        /// Implicit operator from <see cref="GradientAlphaKey"/>
        public static implicit operator ConfigGradientColorKey(GradientColorKey key) => new(key);

        /// Implicit operator to <see cref="GradientAlphaKey"/>
        public static implicit operator GradientColorKey(ConfigGradientColorKey key) => new(key.Color, key.Time);
    }
}