using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NnUtils.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace NnUtils.Modules.JSONUtils.Scripts.Types.Components.UI
{
    /// This class is used as a bridge between <see cref="VerticalLayoutGroup"/> and JSON
    [Serializable]
    public class ConfigVerticalLayoutGroup : ConfigComponent
    {
        [Header("Padding")]
        [JsonProperty] public int PaddingLeft;
        [JsonProperty] public int PaddingRight;
        [JsonProperty] public int PaddingTop;
        [JsonProperty] public int PaddingBottom;

        [Header("")]
        [JsonProperty] public float Spacing;

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty] public TextAnchor ChildAlignment;

        [JsonProperty] public bool ReverseArrangement;

        [Header("Control Child Size")]
        [JsonProperty] public bool ControlWidth;

        [JsonProperty] public bool ControlHeight;

        [Header("Use Child Scale")]
        [JsonProperty] public bool UseWidth;

        [JsonProperty] public bool UseHeight;

        [Header("Child Force Expand")]
        [JsonProperty] public bool ForceExpandWidth;

        [JsonProperty] public bool ForceExpandHeight;

        [Tooltip("Whether data type defaults will be used if partially defined object is found in JSON")]
        [JsonIgnore] public bool UseDataDefaults;
        
        /// Resets values to data defaults overwriting custom defined defaults if data is found in the config
        [OnDeserializing]
        private void OnDeserializing(StreamingContext context)
        {
            if (!UseDataDefaults) return;
            PaddingLeft = 0;
            PaddingRight = 0;
            PaddingTop = 0;
            PaddingBottom = 0;
            Spacing = 0;
            ChildAlignment = TextAnchor.UpperLeft;
            ReverseArrangement = false;
            ControlWidth = false;
            ControlHeight = false;
            UseWidth = false;
            UseHeight = false;
            ForceExpandWidth = true;
            ForceExpandHeight = true;
        }

        public ConfigVerticalLayoutGroup() : this(
            0, 0, 0, 0,
            0, TextAnchor.UpperLeft, false,
            false, false,
            false, false,
            true, true
            ) { }

        public ConfigVerticalLayoutGroup(VerticalLayoutGroup vg) : this(
            vg.padding.left, vg.padding.right, vg.padding.top, vg.padding.bottom,
            vg.spacing, vg.childAlignment, vg.reverseArrangement,
            vg.childControlWidth, vg.childControlHeight,
            vg.childScaleWidth, vg.childScaleHeight,
            vg.childForceExpandWidth, vg.childForceExpandHeight
            ) { }

        public ConfigVerticalLayoutGroup(
            int paddingLeft, int paddingRight, int paddingTop, int paddingBottom,
            float spacing, TextAnchor childAlignment, bool reverseArrangement,
            bool controlWidth, bool controlHeight,
            bool useWidth, bool useHeight,
            bool forceExpandWidth, bool forceExpandHeight
        )
        {
            // Padding
            PaddingLeft   = paddingLeft;
            PaddingRight  = paddingRight;
            PaddingTop    = paddingTop;
            PaddingBottom = paddingBottom;

            // General
            Spacing            = spacing;
            ChildAlignment     = childAlignment;
            ReverseArrangement = reverseArrangement;

            // Control Child Size
            ControlWidth  = controlWidth;
            ControlHeight = controlHeight;

            // Use Child Scale
            UseWidth  = useWidth;
            UseHeight = useHeight;

            // Child Force Expand
            ForceExpandWidth  = forceExpandWidth;
            ForceExpandHeight = forceExpandHeight;
        }

        public static implicit operator ConfigVerticalLayoutGroup(VerticalLayoutGroup vg) => new(vg);

        public VerticalLayoutGroup UpdateVerticalLayoutGroup(VerticalLayoutGroup vg)
        {
            // Padding
            vg.padding.left = PaddingLeft;
            vg.padding.right = PaddingRight;
            vg.padding.top = PaddingTop;
            vg.padding.bottom = PaddingBottom;
            
            // General
            vg.spacing = Spacing;
            vg.childAlignment = ChildAlignment;
            vg.reverseArrangement = ReverseArrangement;
            
            // Control Child Size
            vg.childControlWidth = ControlWidth;
            vg.childControlHeight = ControlHeight;
            
            // Use Child Size
            vg.childScaleWidth = UseWidth;
            vg.childScaleHeight = UseHeight;
            
            // Child Force Expand
            vg.childForceExpandWidth = ForceExpandWidth;
            vg.childForceExpandHeight = ForceExpandHeight;
            
            return vg;
        }
        
        public override void AddComponent(GameObject go) => UpdateVerticalLayoutGroup(go.GetOrAddComponent<VerticalLayoutGroup>());
    }
}