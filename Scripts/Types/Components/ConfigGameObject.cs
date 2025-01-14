using System;
using Newtonsoft.Json;
using UnityEngine;

namespace NnUtils.Modules.JSONUtils.Scripts.Types.Components
{
    [Serializable]
    [JsonObject]
    public class ConfigGameObject
    {
        [JsonProperty] public string Name;

        [JsonProperty] public ConfigComponent[] Components;

        [JsonProperty] public ConfigGameObject[] Children;

        public void Initialize(GameObject go)
        {
            go.name = Name;
            AddComponents(go);
            InstantiateChildren(go);
        }
        
        /// Adds all components to the game object
        private void AddComponents(GameObject go)
        {
            if (Components == null) return;
            
            foreach (var cmp in Components)
            {
                // Continue if component type is invalid
                if (cmp.ComponentType == null) continue;
                
                // Create a new instance of the matching type
                var component = Activator.CreateInstance(cmp.ComponentType);
            
                // Update the json data
                JsonConvert.PopulateObject(cmp.Data?.ToString() ?? "{}", component);
                
                // Get it's AddComponent method
                var methodInfo = component.GetType().GetMethod("AddComponent");

                // Run the method
                methodInfo?.Invoke(component, new object[] { go });
            } 
        }

        /// Instantiates all the child objects
        private void InstantiateChildren(GameObject go)
        {
            if (Children == null) return;
            
            foreach (var child in Children)
            {
                GameObject obj = new();
                obj.transform.SetParent(go.transform);
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localRotation = Quaternion.identity;
                obj.transform.localScale = Vector3.one;
                child.Initialize(obj);
            }
        }
    }
}