using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;

namespace NnUtils.Modules.JSONUtils.Scripts.Types
{
    /// This class is used as a bridge between <see cref="Gradient"/> and JSON <br/>
    /// Make sure to assign null in the Reset function and default value in a function called after loading data if the value is still null <br/>
    /// This approach prevents data stacking in case not all data is defined in the config
    [Serializable]
    public class ConfigGradient
    {
        // BUG: For some reason, gradient mode doesn't work properly
        [JsonConverter(typeof(StringEnumConverter))]
        public GradientMode Mode;
        public List<ConfigGradientAlphaKey> AlphaKeys;
        public List<ConfigGradientColorKey> ColorKeys;

        [OnDeserializing]
        public void OnDeserializing(StreamingContext context)
        { Mode = GradientMode.Blend; AlphaKeys = new(); ColorKeys = new(); }

        public ConfigGradient() : this(GradientMode.Blend, new(), new()) { }

        public ConfigGradient(Gradient gradient) : this(
            gradient.mode,
            new(gradient.alphaKeys.Select(key => (ConfigGradientAlphaKey)key)),
            new(gradient.colorKeys.Select(key => (ConfigGradientColorKey)key))
            ) { }

        public ConfigGradient(GradientMode mode, List<ConfigGradientAlphaKey> alphaKeys, List<ConfigGradientColorKey> colorKeys)
        {
            Mode      = mode;
            AlphaKeys = alphaKeys;
            ColorKeys = colorKeys;
        }

        /// Implicit conversion from <see cref="Gradient"/>
        public static implicit operator ConfigGradient(Gradient g) => new(g);

        /// Implicit conversion to <see cref="Gradient"/>
        public static implicit operator Gradient(ConfigGradient g) => new()
        {
            mode      = g.Mode,
            alphaKeys = g.AlphaKeys.Select(key => (GradientAlphaKey)key).ToArray(),
            colorKeys = g.ColorKeys.Select(key => (GradientColorKey)key).ToArray()
        };
    }
}