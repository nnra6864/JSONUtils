using System;
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

        /// Default ctor
        public ConfigGradientColorKey()
        {
            Color = new();
            Time  = 0;
        }

        /// Ctor from values
        public ConfigGradientColorKey(ConfigColor color, float time)
        {
            Color = color;
            Time  = time;
        }

        /// Ctor from <see cref="GradientAlphaKey"/>
        public ConfigGradientColorKey(GradientColorKey key)
        {
            Color = key.color;
            Time  = key.time;
        }

        /// Implicit operator from <see cref="GradientAlphaKey"/>
        public static implicit operator ConfigGradientColorKey(GradientColorKey key) => new(key);

        /// Implicit operator to <see cref="GradientAlphaKey"/>
        public static implicit operator GradientColorKey(ConfigGradientColorKey key) => new(key.Color, key.Time);
    }
}