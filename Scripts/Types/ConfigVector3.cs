using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using UnityEngine;

namespace NnUtils.Modules.JSONUtils.Scripts.Types
{
    /// This class is used as a bridge between <see cref="Vector3"/> and JSON <br/>
    [Serializable]
    public class ConfigVector3
    {
        [JsonIgnore]
        [Tooltip("Whether data type defaults will be used if partially defined object is found in JSON")]
        public bool UseDataDefaults = true;
        
        public float X;
        public float Y;
        public float Z;

        /// Resets values to data defaults overwriting custom defined defaults if data is found in the config
        [OnDeserializing]
        private void OnDeserializing(StreamingContext context)
        {
            if (!UseDataDefaults) return;
            X = 0; Y = 0; Z = 0;
        }

        public ConfigVector3() : this(0, 0, 0) { }

        public ConfigVector3(Vector3 v) : this(v.x, v.y, v.z) { }
        
        public ConfigVector3(float x = 0, float y = 0, float z = 0) { X = x; Y = y; Z = z; }
        
        /// Implicit conversion from <see cref="Vector3"/>
        public static implicit operator ConfigVector3(Vector3 v) => new(v.x, v.y, v.z);
        
        /// Implicit conversion to <see cref="Vector3"/>
        public static implicit operator Vector3(ConfigVector3 v) => new(v.X, v.Y, v.Z);
    }
}