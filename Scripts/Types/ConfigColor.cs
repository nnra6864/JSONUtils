using System;
using UnityEngine;

namespace NnUtils.Modules.JSONUtils.Scripts.Types
{
    /// This class is used as a bridge between <see cref="Color"/> and JSON <br/>
    /// Make sure to assign null in the Reset function and default value in a function called after loading data if the value is still null <br/>
    /// This approach prevents data stacking in case not all data is defined in the config
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