using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using UnityEngine;

namespace NnUtils.Modules.JSONUtils.Scripts.Types
{
    /// This class is used as a bridge between <see cref="Color"/> and JSON <br/>
    [Serializable]
    public class ConfigColor
    {
        /// Red
        public float R;
        /// Green
        public float G;
        /// Blue
        public float B;
        /// Alpha
        public float A;
        /// Intensity
        public float I;

        [Tooltip("Whether data type defaults will be used if partially defined object is found in JSON")]
        [JsonIgnore] public bool UseDataDefaults = true;
        
        /// Resets values to data defaults overwriting custom defined defaults if data is found in the config
        [OnDeserializing]
        private void Reset(StreamingContext context)
        {
            if (!UseDataDefaults) return;
            R = 0; G = 0; B = 0; A = 1; I = 1;
        }

        public ConfigColor() : this(0, 0, 0, 1, 1) { }
        
        public ConfigColor(ConfigColor c) : this(c.R, c.G, c.B, c.A, c.I) { }

        public ConfigColor(Color c) : this(c.r, c.g, c.b, c.a, 1) { }

        public ConfigColor(float r, float g, float b, float a, float i) { R = r; G = g; B = b; A = a; I = i; }

        /// Implicit conversion from <see cref="UnityEngine.Color"/>
        public static implicit operator ConfigColor(Color c) => new(c);
        
        /// Implicit conversion to <see cref="UnityEngine.Color"/>
        public static implicit operator Color(ConfigColor c) => new (c.R * c.I, c.G * c.I , c.B * c.I, c.A);
    }
}