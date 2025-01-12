using System;
using Newtonsoft.Json;
using UnityEngine;
using UnityJSONUtils.Scripts.Types.Components.UI;

namespace UnityJSONUtils.Scripts.Types.Components
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

        // TODO: Make into a scriptable object or something more convenient for editing
        private Type GetComponentType() =>
            Type switch
            {
                "Transform" => typeof(ConfigTransform),
                "RectTransform" => typeof(ConfigRectTransform),
                "Canvas" => typeof(ConfigCanvas),
                "CanvasScaler" => typeof(ConfigCanvasScaler),
                _ => null
            };
    }
}