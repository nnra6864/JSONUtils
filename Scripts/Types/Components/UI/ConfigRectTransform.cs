using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using NnUtils.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace NnUtils.Modules.JSONUtils.Scripts.Types.Components.UI
{
    /// This class is used as a bridge between <see cref="RectTransform"/> and JSON <br/>
    [Serializable]
    public class ConfigRectTransform : ConfigComponent
    {
        // Anchors
        [JsonProperty] public ConfigVector2 AnchorsX;
        [JsonProperty] public ConfigVector2 AnchorsY;

        // Pivot
        [JsonProperty] public ConfigVector2 Pivot;
        
        // Transform
        [JsonProperty] public ConfigVector3 Position;
        [JsonProperty] public ConfigVector3 Rotation;
        [JsonProperty] public ConfigVector3 Scale;
        
        // Size
        [JsonProperty] public ConfigVector2 Size;
        
        // Offset
        [JsonProperty] public ConfigVector2 HorizontalOffset;
        [JsonProperty] public ConfigVector2 VerticalOffset;

        [JsonIgnore]
        [Tooltip("Whether data type defaults will be used if partially defined object is found in JSON")]
        public bool UseDataDefaults;

        /// Resets values to data defaults overwriting custom defined defaults if data is found in the config
        [OnDeserializing]
        private void OnDeserializing(StreamingContext context)
        {
            if (!UseDataDefaults) return;
            Position         = Vector3.zero;
            Rotation         = Vector3.zero;
            Scale            = Vector3.one;
            Size             = Vector2.one * 100;
            HorizontalOffset = Vector2.zero;
            VerticalOffset   = Vector2.zero;
            AnchorsX         = Vector2.one * 0.5f;
            AnchorsY         = Vector2.one * 0.5f;
            Pivot            = Vector2.one * 0.5f;
        }

        public ConfigRectTransform() : this(
            Vector3.zero, Vector3.zero, Vector3.one,
            Vector2.one * 100,
            Vector2.zero, Vector2.zero,
            Vector2.one * 0.5f, Vector2.one * 0.5f,
            Vector2.one * 0.5f
            ) { }

        public ConfigRectTransform(RectTransform t) : this(
            new(t.anchoredPosition3D.x, t.anchoredPosition3D.y, t.anchoredPosition3D.z), t.localEulerAngles, t.localScale,
            t.sizeDelta,
            new(t.offsetMin.x, -t.offsetMax.x), new(t.offsetMin.y, -t.offsetMax.y),
            new(t.anchorMin.x, t.anchorMax.x), new(t.anchorMin.y, t.anchorMax.y),
            t.pivot
        ) { }

        public ConfigRectTransform(
            Vector3 position, Vector3 rotation, Vector3 scale,
            Vector2 size,
            Vector2 horizontalOffset, Vector2 verticalOffset,
            Vector2 anchorsX, Vector2 anchorsY,
            Vector2 pivot
            )
        {
            // Anchors
            AnchorsX = anchorsX;
            AnchorsY = anchorsY;
            
            // Pivot
            Pivot = pivot;
            
            // Transform
            Position = position;
            Rotation = rotation;
            Scale    = scale;
            
            // Size
            Size = size;
            
            // Offset
            HorizontalOffset = horizontalOffset;
            VerticalOffset = verticalOffset;
        }

        public static implicit operator ConfigRectTransform(RectTransform t) => new(t);

        /// Updates an existing <see cref="Transform"/> component with config values
        public RectTransform UpdateRectTransform(RectTransform t)
        {
            // Store offset states
            var offsetHorizontal = !Mathf.Approximately(AnchorsX.X, AnchorsX.Y);
            var offsetVertical = !Mathf.Approximately(AnchorsY.X, AnchorsY.Y);

            // Anchors
            t.anchorMin = new(AnchorsX.X, AnchorsY.X);
            t.anchorMax = new(AnchorsX.Y, AnchorsY.Y);

            // Pivot
            t.pivot = new(Pivot.X, Pivot.Y);

            // Store needed values
            var pos = t.anchoredPosition;
            var size = t.sizeDelta;

            // Handle positioning
            if (!offsetHorizontal)
            {
                // Update position and size
                pos.x  = Position.X;
                size.x = Size.X;

                // Update the rect transform
                t.anchoredPosition = pos;
                t.sizeDelta          = size;
            }
            
            if (!offsetVertical)
            {
                // Update position and size
                pos.y  = Position.Y;
                size.y = Size.Y;

                // Update the rect transform
                t.anchoredPosition = pos;
                t.sizeDelta          = size;
            }
            
            // Handle offset
            if (offsetHorizontal)
            {
                // Update rect transform
                t.offsetMin = new(HorizontalOffset.X, t.offsetMin.y);
                t.offsetMax = new(-HorizontalOffset.Y, t.offsetMax.y);
            }

            if (offsetVertical)
            {
                // Update rect transform
                t.offsetMin = new(t.offsetMin.x, VerticalOffset.X);
                t.offsetMax = new(t.offsetMax.x, -VerticalOffset.Y);
            }

            // Transform
            t.localPosition    = new(t.localPosition.x, t.localPosition.y, Position.Z);
            t.localEulerAngles = Rotation;
            t.localScale       = Scale;

            return t;
        }

        public override void AddComponent(GameObject go) => UpdateRectTransform(go.GetOrAddComponent<RectTransform>());
    }
}