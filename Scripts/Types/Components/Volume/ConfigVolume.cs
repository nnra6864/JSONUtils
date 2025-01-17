using System;
using Newtonsoft.Json;
using NnUtils.Scripts;
using UnityEngine;
using UnityEngine.Rendering;

namespace NnUtils.Modules.JSONUtils.Scripts.Types.Components.Volume
{
    /// This class is used as a bridge between <see cref="Volume"/> and JSON <br/>
    /// Make sure to assign null in the Reset function and default value in a function called after loading data if the value is still null <br/>
    /// This approach prevents data stacking in case not all data is defined in the config
    [Serializable]
    public class ConfigVolume : ConfigComponent
    {
        [JsonProperty] public bool IsGlobal;
        [JsonProperty] public float Weight;
        [JsonProperty] public float Priority;
        [JsonProperty] public float BlendDistance;
        [JsonIgnore] public VolumeProfile Profile;

        public ConfigVolume() : this(false, 1, 0, 0, null) { }

        public ConfigVolume(UnityEngine.Rendering.Volume v) : this(v.isGlobal, v.weight, v.priority, v.blendDistance, v.profile) { }

        public ConfigVolume(bool isGlobal, float weight, float priority, float blendDistance, VolumeProfile profile)
        {
            IsGlobal      = isGlobal;
            Weight        = weight;
            Priority      = priority;
            BlendDistance = blendDistance;
            Profile       = profile;
        }

        public void UpdateVolume(UnityEngine.Rendering.Volume v)
        {
            v.isGlobal      = IsGlobal;
            v.weight        = Weight;
            v.priority      = Priority;
            v.blendDistance = BlendDistance;
            v.profile       = Profile;
        }

        public static implicit operator ConfigVolume(UnityEngine.Rendering.Volume v) => new(v);
        
        public override void AddComponent(GameObject go) => UpdateVolume(go.GetOrAddComponent<UnityEngine.Rendering.Volume>());
    }
}