using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NnUtils.Modules.TextUtils.Scripts;
using NnUtils.Modules.TextUtils.Scripts.InteractiveText;
using NnUtils.Scripts;
using TMPro;
using UnityEngine;
using Color = UnityEngine.Color;

namespace NnUtils.Modules.JSONUtils.Scripts.Types.Components.UI
{
    /// This class is used as a bridge between <see cref="TMP_Text"/> and JSON
    [Serializable]
    public class ConfigText : ConfigComponent
    {
        /// Leaving it empty will result in no effect
        [JsonIgnore] public static string DefaultFont = "";
        
        [JsonProperty] public bool Interactive;
        
        // Use <br> to go to new line
        [JsonProperty] public string Text;
        
        [JsonProperty] public string Font;
        
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty] public FontStyles FontStyle;

        [JsonProperty] public float FontSize;

        [JsonProperty] public bool AutoSize;

        [JsonProperty] public ConfigColor Color;
        
        [JsonProperty] public float CharacterSpacing;
        [JsonProperty] public float WordSpacing;
        [JsonProperty] public float LineSpacing;
        [JsonProperty] public float ParagraphSpacing;
        
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty] public TextAlignmentOptions Alignment;
        
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty] public TextWrappingModes Wrapping;
        
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty] public TextOverflowModes Overflow;

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty] public TextureMappingOptions HorizontalMapping;
        
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty] public TextureMappingOptions VerticalMapping;

        [Tooltip("Whether data type defaults will be used if partially defined object is found in JSON")]
        [JsonIgnore] public bool UseDataDefaults;

        /// Resets values to data defaults overwriting custom defined defaults if data is found in the config
        [OnDeserializing]
        private void OnDeserializing(StreamingContext context)
        {
            if (!UseDataDefaults) return;
            Interactive       = false;
            Text              = "";
            Font              = DefaultFont;
            FontStyle         = FontStyles.Normal;
            FontSize          = 36;
            AutoSize          = false;
            Color             = UnityEngine.Color.white;
            CharacterSpacing  = 0;
            WordSpacing       = 0;
            LineSpacing       = 0;
            ParagraphSpacing  = 0;
            Alignment         = TextAlignmentOptions.TopLeft;
            Wrapping          = TextWrappingModes.Normal;
            Overflow          = TextOverflowModes.Overflow;
            HorizontalMapping = TextureMappingOptions.Character;
            VerticalMapping   = TextureMappingOptions.Character;
        }

        public ConfigText() : this(
            false,
            "",
            DefaultFont, FontStyles.Normal, 36, false,
            UnityEngine.Color.white,
            0, 0, 0, 0,
            TextAlignmentOptions.TopLeft, TextWrappingModes.Normal, TextOverflowModes.Overflow,
            TextureMappingOptions.Character, TextureMappingOptions.Character
            ) { }

        public ConfigText(TMP_Text t) : this(
             false,
             t.text,
             t.font.name, t.fontStyle, t.fontSize, t.enableAutoSizing,
             t.color,
             t.characterSpacing, t.wordSpacing, t.lineSpacing, t.paragraphSpacing,
             t.alignment, t.textWrappingMode, t.overflowMode,
             t.horizontalMapping, t.verticalMapping
             ) { }

        public ConfigText(
            bool interactive,
            string text,
            string font, FontStyles fontStyle, float fontSize, bool autoSize,
            Color color,
            float characterSpacing, float wordSpacing, float lineSpacing, float paragraphSpacing,
            TextAlignmentOptions alignment, TextWrappingModes wrapping, TextOverflowModes overflow,
            TextureMappingOptions horizontalMapping, TextureMappingOptions verticalMapping)
        {
            Interactive       = interactive;
            Text              = text;
            Font              = font;
            FontStyle         = fontStyle;
            FontSize          = fontSize;
            AutoSize          = autoSize;
            Color             = color;
            CharacterSpacing  = characterSpacing;
            WordSpacing       = wordSpacing;
            LineSpacing       = lineSpacing;
            ParagraphSpacing  = paragraphSpacing;
            Alignment         = alignment;
            Wrapping          = wrapping;
            Overflow          = overflow;
            HorizontalMapping = horizontalMapping;
            VerticalMapping   = verticalMapping;
        }
        
        public static implicit operator ConfigText(TMP_Text t) => new(t);

        public TMP_Text UpdateText(TMP_Text t)
        {
            t.text              = Text;
            t.font              = GetFontAsset(t.font);
            t.fontStyle         = FontStyle;
            t.fontSize          = FontSize;
            t.enableAutoSizing  = AutoSize;
            t.color             = Color;
            t.characterSpacing  = CharacterSpacing;
            t.wordSpacing       = WordSpacing;
            t.lineSpacing       = LineSpacing;
            t.paragraphSpacing  = ParagraphSpacing;
            t.alignment         = Alignment;
            t.textWrappingMode  = Wrapping;
            t.overflowMode      = Overflow;
            t.horizontalMapping = HorizontalMapping;
            t.verticalMapping   = VerticalMapping;
            return t;
        }
        
        public override void AddComponent(GameObject go)
        {
            UpdateText(go.GetOrAddComponent<TextMeshProUGUI>());
            if (Interactive) go.GetOrAddComponent<InteractiveTMPText>();
        }

        public TMP_FontAsset GetFontAsset(TMP_FontAsset f)
        {
            // Create font asset
            TMP_FontAsset fontAsset;
            
            // If Font is set to Default, return current
            if (Font == "Default") return f;
            
            // If Font is found in the system, return that
            fontAsset = SystemFont.GenerateFontFromName(Font);
            if (fontAsset != null) return fontAsset;
            
            // If DefaultFont is set to Default, return current
            if (DefaultFont == "Default") return f;
            
            // If DefaultFont is found in the system, return that
            fontAsset = SystemFont.GenerateFontFromName(DefaultFont);
            if (fontAsset != null) return fontAsset;
            
            // Return current if nothing else worked
            return f;
        }
    }
}