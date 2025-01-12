using System;
using Newtonsoft.Json;
using NnUtils.Scripts;
using UnityEngine;

namespace UnityJSONUtils.Scripts.Types.Components
{
    /// This class is used as a bridge between <see cref="Transform"/> and JSON <br/>
    /// Make sure to assign null in the Reset function and default value in a function called after loading data if the value is still null <br/>
    /// This approach prevents data stacking in case not all data is defined in the config
    [Serializable]
    public class ConfigTransform : ConfigComponent
    {
        [JsonProperty] public ConfigVector3 Position;
        [JsonProperty] public ConfigVector3 Rotation;
        [JsonProperty] public ConfigVector3 Scale;

        public ConfigTransform()
        {
            Position = Vector3.zero;
            Rotation = Vector3.zero;
            Scale    = Vector3.one;
        }

        public ConfigTransform(Transform t)
        {
            Position = t.localPosition;
            Rotation = t.localEulerAngles;
            Scale    = t.localScale;
        }

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