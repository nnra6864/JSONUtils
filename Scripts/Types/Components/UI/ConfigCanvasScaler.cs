using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;
using UnityEngine.UI;

namespace UnityJSONUtils.Scripts.Types.Components.UI
{
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

        public ConfigCanvasScaler()
        {
            // General
            ScaleMode              = CanvasScaler.ScaleMode.ConstantPixelSize;
            ReferencePixelsPerUnit = 1;
            
            // Constant Pixel Size
            ScaleFactor         = 1;
            
            // Scale With Screen Size
            ReferenceResolution = new(800, 600);
            ScreenMatchMode     = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            MatchWidthOrHeight  = 0;
            
            // Constant Physical Size
            PhysicalUnit        = CanvasScaler.Unit.Points;
            FallbackScreenDPI   = 96;
            DefaultSpriteDPI    = 96;
        }

        public ConfigCanvasScaler(CanvasScaler scaler)
        {
            // General
            ScaleMode              = scaler.uiScaleMode;
            ReferencePixelsPerUnit = scaler.referencePixelsPerUnit;

            // Constant Pixel Size
            ScaleFactor = scaler.scaleFactor;

            // Scale With Screen Size
            ReferenceResolution = scaler.referenceResolution;
            ScreenMatchMode     = scaler.screenMatchMode;
            MatchWidthOrHeight  = scaler.matchWidthOrHeight;

            // Constant Physical Size
            PhysicalUnit      = scaler.physicalUnit;
            FallbackScreenDPI = scaler.fallbackScreenDPI;
            DefaultSpriteDPI  = scaler.defaultSpriteDPI;
        }

        public ConfigCanvasScaler(
            // General
            CanvasScaler.ScaleMode scaleMode,
            float referencePixelsPerUnit,
            
            // Constant Pixel Size
            float scaleFactor,
            
            // Scale With Screen Size
            Vector2 referenceResolution,
            CanvasScaler.ScreenMatchMode screenMatchMode,
            float matchWidthOrHeight,
            
            // Constant Physical Size
            CanvasScaler.Unit physicalUnit,
            float fallbackScreenDPI,
            float defaultSpriteDPI
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

        public static implicit operator ConfigCanvasScaler(CanvasScaler scaler) => new(scaler);

        public CanvasScaler UpdateCanvasScaler(CanvasScaler scaler)
        {
            // General
            scaler.uiScaleMode = ScaleMode;
            scaler.referencePixelsPerUnit = ReferencePixelsPerUnit;
            
            // Constant Pixel Size
            scaler.scaleFactor = ScaleFactor;
            
            // Scale With Screen Size
            scaler.referenceResolution = ReferenceResolution;
            scaler.screenMatchMode = ScreenMatchMode;
            scaler.matchWidthOrHeight = MatchWidthOrHeight;
            
            // Constant Physical Size
            scaler.physicalUnit = PhysicalUnit;
            scaler.fallbackScreenDPI = FallbackScreenDPI;
            scaler.defaultSpriteDPI = DefaultSpriteDPI;
            
            return scaler;
        }
        
        public override void AddComponent(GameObject go) => UpdateCanvasScaler(go.AddComponent<CanvasScaler>());
    }
}