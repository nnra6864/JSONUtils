using System;
using UnityEngine;

namespace NnUtils.Modules.JSONUtils.Scripts.Types
{
    /// This class is used as a bridge between <see cref="Vector3"/> and JSON <br/>
    /// Make sure to assign null in the Reset function and default value in a function called after loading data if the value is still null <br/>
    /// This approach prevents data stacking in case not all data is defined in the config
    [Serializable]
    public class ConfigVector3
    {
        public float X;
        public float Y;
        public float Z;

        public ConfigVector3() : this(0, 0, 0) { }

        public ConfigVector3(Vector3 v) : this(v.x, v.y, v.z) { }
        
        public ConfigVector3(float x = 0, float y = 0, float z = 0) { X = x; Y = y; Z = z; }
        
        /// Implicit conversion from <see cref="Vector3"/>
        public static implicit operator ConfigVector3(Vector3 v) => new(v.x, v.y, v.z);
        
        /// Implicit conversion to <see cref="Vector3"/>
        public static implicit operator Vector3(ConfigVector3 v) => new(v.X, v.Y, v.Z);
    }
}