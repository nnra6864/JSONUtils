using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using UnityEngine;

namespace NnUtils.Modules.JSONUtils.Scripts.Types
{
    /// This class is used as a bridge between <see cref="Vector2"/> and JSON <br/>
    [Serializable]
    public class ConfigVector2
    {
        [JsonIgnore]
        [Tooltip("Whether data type defaults will be used if partially defined object is found in JSON")]
        public bool UseDataDefaults = true;
        
        public float X;
        public float Y;

        /// Resets values to data defaults overwriting custom defined defaults if data is found in the config
        [OnDeserializing]
        private void OnDeserializing(StreamingContext context)
        {
            if (!UseDataDefaults) return;
            X = 0; Y = 0;
        }
        
        public ConfigVector2() : this(0, 0) { }

        public ConfigVector2(Vector2 v) : this(v.x, v.y) { }
        
        public ConfigVector2(float x = 0, float y = 0) { X = x; Y = y; }
        
        /// Implicit conversion from <see cref="Vector2"/>
        public static implicit operator ConfigVector2(Vector2 v) => new(v.x, v.y);
        
        /// Implicit conversion to <see cref="Vector2"/>
        public static implicit operator Vector2(ConfigVector2 v) => new(v.X, v.Y);
    }
}