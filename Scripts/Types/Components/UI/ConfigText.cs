using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TMPro;

namespace NnUtils.Modules.JSONUtils.Scripts.Types.Components.UI
{
    /// This class is used as a bridge between <see cref="TMP_Text"/> and JSON <br/>
    /// Make sure to assign null in the Reset function and default value in a function called after loading data if the value is still null <br/>
    /// This approach prevents data stacking in case not all data is defined in the config
    [Serializable]
    public class ConfigText : ConfigComponent
    {
        // TODO: Make a system font repo and add it as a submodule to NnUtils
        /// Leaving it empty will result in no effect <br/>
        /// Setting it to Default will result in a default system font being used <br/>
        /// Assigning it a name, e.g. CascadiaCode, will result in a system font being used if found
        public static string DefaultFont = "";
        
        // Use <br> to go to new line
        [JsonProperty] public string Text;
        
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty] public FontStyles FontStyle;

        private void Test()
        {
            TMP_Text t;
        }
    }
}