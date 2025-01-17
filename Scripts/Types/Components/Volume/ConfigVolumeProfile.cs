using System;
using UnityEngine.Rendering;

namespace NnUtils.Modules.JSONUtils.Scripts.Types.Components.Volume
{
    /// This class is used as a bridge between <see cref="VolumeProfile"/> and JSON <br/>
    /// Make sure to assign null in the Reset function and default value in a function called after loading data if the value is still null <br/>
    /// This approach prevents data stacking in case not all data is defined in the config
    [Serializable]
    public class ConfigVolumeProfile : ConfigComponent
    {
        public ConfigVolumeComponent[] Components;
    }
}