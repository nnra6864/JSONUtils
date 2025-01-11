using System;
using Newtonsoft.Json;

namespace UnityJSONUtils.Scripts.Types.Components
{
    [Serializable]
    public class ConfigComponentWrapper
    {
        [JsonProperty] public string Type;
        [JsonProperty] public object Data;
    }
}