using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using UnityEngine;

namespace NnUtils.Modules.JSONUtils.Scripts.Types
{
    /// This class is used as a bridge between <see cref="GradientAlphaKey"/> and JSON <br/>
    [Serializable]
    public class ConfigGradientAlphaKey
    {
        [JsonIgnore]
        [Tooltip("Whether data type defaults will be used if partially defined object is found in JSON")]
        public bool UseDataDefaults = true;
        
        public float Alpha;
        public float Time;

        /// Resets values to data defaults overwriting custom defined defaults if data is found in the config
        [OnDeserializing]
        private void OnDeserializing(StreamingContext context)
        {
            if (!UseDataDefaults) return;
            Alpha = 1; Time  = 0;
        }
        
        /// Default ctor
        public ConfigGradientAlphaKey() : this(1, 0) { }
        
        /// Ctor from <see cref="GradientAlphaKey"/>
        public ConfigGradientAlphaKey(GradientAlphaKey key) : this(key.alpha, key.time) { }

        /// Ctor from values
        public ConfigGradientAlphaKey(float alpha, float time)
        {
            Alpha = alpha;
            Time = time;
        }

        /// Implicit operator from <see cref="GradientAlphaKey"/>
        public static implicit operator ConfigGradientAlphaKey(GradientAlphaKey key) => new(key);
        
        /// Implicit operator to <see cref="GradientAlphaKey"/>
        public static implicit operator GradientAlphaKey(ConfigGradientAlphaKey key) => new(key.Alpha, key.Time);
    }
}