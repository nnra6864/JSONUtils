using System;
using System.Runtime.Serialization;
using UnityEngine;

namespace NnUtils.Modules.JSONUtils.Scripts.Types
{
    /// This class is used as a bridge between <see cref="GradientColorKey"/> and JSON <br/>
    /// Make sure to assign null in the Reset function and default value in a function called after loading data if the value is still null <br/>
    /// This approach prevents data stacking in case not all data is defined in the config
    [Serializable]
    public class ConfigGradientColorKey
    {
        public ConfigColor Color;
        public float Time;

        [OnDeserializing]
        private void OnDeserializing(StreamingContext context)
        { Color = new(); Time  = 0;}
        
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