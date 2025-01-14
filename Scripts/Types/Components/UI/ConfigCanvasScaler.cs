using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NnUtils.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace NnUtils.Modules.JSONUtils.Scripts.Types.Components.UI
{
    /// This class is used as a bridge between <see cref="CanvasScaler"/> and JSON <br/>
    /// Make sure to assign null in the Reset function and default value in a function called after loading data if the value is still null <br/>
    /// This approach prevents data stacking in case not all data is defined in the config
    [Serializable]
    public class ConfigCanvasScaler : ConfigComponent
    {
        [Header("General")]
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty] public CanvasScaler.ScaleMode ScaleMode;
        [JsonProperty] public float ReferencePixelsPerUnit;
        
        [Header("Constant Pixel Size")]
        [JsonProperty] public float ScaleFactor;
        
        [Header("Scale With Screen Size")]
        [JsonProperty] public ConfigVector2 ReferenceResolution;
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty] public CanvasScaler.ScreenMatchMode ScreenMatchMode;
        [Range(0, 1)] public float MatchWidthOrHeight;

        [Header("Constant Physical Size")]
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty] public CanvasScaler.Unit PhysicalUnit;
        [JsonProperty] public float FallbackScreenDPI;
        [JsonProperty] public float DefaultSpriteDPI;

        public ConfigCanvasScaler() : this(
            CanvasScaler.ScaleMode.ConstantPixelSize, 1,
            1,
            new(800, 600), CanvasScaler.ScreenMatchMode.MatchWidthOrHeight, 0,
            CanvasScaler.Unit.Points, 96, 96
            ) { }

        public ConfigCanvasScaler(CanvasScaler s) : this(
            s.uiScaleMode, s.referencePixelsPerUnit,
            s.scaleFactor,
            s.referenceResolution, s.screenMatchMode, s.matchWidthOrHeight,
            s.physicalUnit, s.fallbackScreenDPI, s.defaultSpriteDPI
            ) { }

        public ConfigCanvasScaler(
            CanvasScaler.ScaleMode scaleMode, float referencePixelsPerUnit,
            float scaleFactor,
            Vector2 referenceResolution, CanvasScaler.ScreenMatchMode screenMatchMode, float matchWidthOrHeight,
            CanvasScaler.Unit physicalUnit, float fallbackScreenDPI, float defaultSpriteDPI
        )
        {
            // General
            ScaleMode              = scaleMode;
            ReferencePixelsPerUnit = referencePixelsPerUnit;

            // Constant Pixel Size
            ScaleFactor = scaleFactor;

            // Scale With Screen Size
            ReferenceResolution = referenceResolution;
            ScreenMatchMode     = screenMatchMode;
            MatchWidthOrHeight  = matchWidthOrHeight;

            // Constant Physical Size
            PhysicalUnit      = physicalUnit;
            FallbackScreenDPI = fallbackScreenDPI;
            DefaultSpriteDPI  = defaultSpriteDPI;
        }

        public static implicit operator ConfigCanvasScaler(CanvasScaler s) => new(s);

        /// Updates an existing <see cref="CanvasScaler"/> component with config values
        public CanvasScaler UpdateCanvasScaler(CanvasScaler s)
        {
            // General
            s.uiScaleMode = ScaleMode;
            s.referencePixelsPerUnit = ReferencePixelsPerUnit;
            
            // Constant Pixel Size
            s.scaleFactor = ScaleFactor;
            
            // Scale With Screen Size
            s.referenceResolution = ReferenceResolution;
            s.screenMatchMode = ScreenMatchMode;
            s.matchWidthOrHeight = MatchWidthOrHeight;
            
            // Constant Physical Size
            s.physicalUnit = PhysicalUnit;
            s.fallbackScreenDPI = FallbackScreenDPI;
            s.defaultSpriteDPI = DefaultSpriteDPI;
            
            return s;
        }
        
        public override void AddComponent(GameObject go) => UpdateCanvasScaler(go.GetOrAddComponent<CanvasScaler>());
    }
}