using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using UnityEngine;

namespace NnUtils.Modules.JSONUtils.Scripts.Types
{
    /// This class is used as a bridge between <see cref="Vector4"/> and JSON <br/>
    [Serializable]
    public class ConfigVector4
    {
        public float X;
        public float Y;
        public float Z;
        public float W;

        [Tooltip("Whether data type defaults will be used if partially defined object is found in JSON")]
        [JsonIgnore] public bool UseDataDefaults;
        
        /// Resets values to data defaults overwriting custom defined defaults if data is found in the config
        [OnDeserializing]
        private void OnDeserializing(StreamingContext context)
        {
            if (!UseDataDefaults) return;
            X = 0; Y = 0; Z = 0; W = 0;
        }

        public ConfigVector4() : this(0, 0, 0, 0) { }

        public ConfigVector4(Vector4 v) : this(v.x, v.y, v.z, v.w) { }
        
        public ConfigVector4(float x = 0, float y = 0, float z = 0, float w = 0) { X = x; Y = y; Z = z; W= w; }
        
        /// Implicit conversion from <see cref="Vector4"/>
        public static implicit operator ConfigVector4(Vector4 v) => new(v.x, v.y, v.z, v.w);
        
        /// Implicit conversion to <see cref="Vector4"/>
        public static implicit operator Vector4(ConfigVector4 v) => new(v.X, v.Y, v.Z, v.W);
    }
}