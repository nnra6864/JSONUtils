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
        
        [JsonProperty] public object Data;

        [JsonIgnore] public Type ComponentType;

        public void LoadData(object data)
        {
        }
        
        /// Adds the component to an object and updates its values from config
        public virtual void AddComponent(GameObject go)
        {
            throw new NotImplementedException();
        }

        private Type GetComponentType() =>
            Type switch
            {
                "Canvas" => typeof(ConfigCanvas),
                "CanvasScaler" => typeof(ConfigCanvasScaler),
                _ => null
            };
    }
}