using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using NnUtils.Scripts;
using UnityEngine;

namespace NnUtils.Modules.JSONUtils.Scripts.Types.Components
{
    /// This class is used as a bridge between <see cref="Transform"/> and JSON
    [Serializable]
    public class ConfigTransform : ConfigComponent
    {
        [JsonProperty] public ConfigVector3 Position;
        [JsonProperty] public ConfigVector3 Rotation;
        [JsonProperty] public ConfigVector3 Scale;
        
        [Tooltip("Whether data type defaults will be used if partially defined object is found in JSON")]
        [JsonIgnore] public bool UseDataDefaults;
        
        /// Resets values to data defaults overwriting custom defined defaults if data is found in the config
        [OnDeserializing]
        private void OnDeserializing(StreamingContext context)
        {
            if (!UseDataDefaults) return;
            Position = Vector3.zero;
            Rotation = Vector3.zero;
            Scale = Vector3.one;
        }

        public ConfigTransform() : this(Vector3.zero, Vector3.zero, Vector3.zero) { }

        public ConfigTransform(Transform t) : this(t.localPosition, t.localRotation.eulerAngles, t.localScale) { }

        public ConfigTransform(Vector3 position, Vector3 rotation, Vector3 scale)
        {
            Position = position;
            Rotation = rotation;
            Scale    = scale;
        }

        public static implicit operator ConfigTransform(Transform t) => new(t);

        /// Updates an existing <see cref="Transform"/> component with config values
        public Transform UpdateTransform(Transform t)
        {
            t.localPosition    = Position;
            t.localEulerAngles = Rotation;
            t.localScale       = Scale;
            return t;
        }

        public override void AddComponent(GameObject go) => UpdateTransform(go.GetOrAddComponent<Transform>());
    }
}