using System;
using Newtonsoft.Json;
using NnUtils.Modules.JSONUtils.Scripts.Types.Components.UI;
using NnUtils.Modules.JSONUtils.Scripts.Types.Components.UI.Image;
using UnityEngine;

namespace NnUtils.Modules.JSONUtils.Scripts.Types.Components
{
    [Serializable]
    public class ConfigComponent
    {
        private string _type;
        [JsonProperty] public string Type
        {
            get => _type;
            set
            {
                _type         = value;
                ComponentType = GetComponentType();
            }
        }
        [JsonIgnore] public Type ComponentType;
        
        [JsonProperty] public object Data;
        
        /// Adds the component to an object and updates its values from config
        public virtual void AddComponent(GameObject go) { throw new NotImplementedException(); }

        private Type GetComponentType() =>
            Type switch
            {
                "Transform" => typeof(ConfigTransform),
                "RectTransform" => typeof(ConfigRectTransform),
                "Canvas" => typeof(ConfigCanvas),
                "CanvasScaler" => typeof(ConfigCanvasScaler),
                "VerticalLayoutGroup" => typeof(ConfigVerticalLayoutGroup),
                "Text" => typeof(ConfigText),
                "Image" => typeof(ConfigImage),
                _ => null
            };
    }
}