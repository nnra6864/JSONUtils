using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NnUtils.Scripts;
using UnityEngine;

namespace NnUtils.Modules.JSONUtils.Scripts.Types.Components.UI
{
    /// This class is used as a bridge between <see cref="Canvas"/> and JSON <br/>
    [Serializable]
    public class ConfigCanvas : ConfigComponent
    {
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty] public RenderMode RenderMode;
        [JsonProperty] public bool PixelPerfect;
        [JsonProperty] public int SortOrder;
        [JsonProperty] public int TargetDisplay;
        [JsonProperty] public string CameraName;
        [JsonProperty] public float PlaneDistance;

        [JsonIgnore]
        [Tooltip("Whether data type defaults will be used if partially defined object is found in JSON")]
        public bool UseDataDefaults;
        
        /// Resets values to data defaults overwriting custom defined defaults if data is found in the config
        [OnDeserializing]
        private void OnDeserializing(StreamingContext context)
        {
            if (!UseDataDefaults) return;
            RenderMode = RenderMode.ScreenSpaceOverlay;
            PixelPerfect = false;
            SortOrder = 0;
            TargetDisplay = 0;
            CameraName = "";
            PlaneDistance = 100;
        }

        public ConfigCanvas() :
            this(RenderMode.ScreenSpaceOverlay, false, 0, 0, "", 100) { }

        public ConfigCanvas(Canvas c) :
            this(c.renderMode, c.pixelPerfect, c.sortingOrder, c.targetDisplay, c.worldCamera.gameObject.name, c.planeDistance) { }

        public ConfigCanvas(
            RenderMode mode = RenderMode.ScreenSpaceOverlay,
            bool pixelPerfect = false,
            int sortOrder = 0,
            int targetDisplay = 0,
            string cameraName = "",
            float planeDistance = 100
        )
        {
            RenderMode    = mode;
            PixelPerfect  = pixelPerfect;
            SortOrder     = sortOrder;
            TargetDisplay = targetDisplay;
            CameraName    = cameraName;
            PlaneDistance = planeDistance;
        }

        public static implicit operator ConfigCanvas(Canvas c) => new(c);

        /// Updates an existing <see cref="Canvas"/> component with config values
        public Canvas UpdateCanvas(Canvas c)
        {
            c.renderMode    = RenderMode;
            c.pixelPerfect  = PixelPerfect;
            c.sortingOrder  = SortOrder;
            c.targetDisplay = TargetDisplay;
            c.worldCamera   = GameObject.Find(CameraName)?.GetComponent<Camera>() ?? Camera.main;
            c.planeDistance = PlaneDistance;
            return c;
        }

        public override void AddComponent(GameObject go) => UpdateCanvas(go.GetOrAddComponent<Canvas>());
    }
}