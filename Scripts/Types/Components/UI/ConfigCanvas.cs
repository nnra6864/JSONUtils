using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;

namespace UnityJSONUtils.Scripts.Types.Components.UI
{
    [Serializable]
    public class ConfigCanvas : ConfigComponent
    {
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty] public RenderMode RenderMode;
        [JsonProperty] public bool PixelPerfect;
        [JsonProperty] public int SortOrder;
        [JsonProperty] public int TargetDisplay;
        [JsonProperty] public string CameraName;

        public ConfigCanvas()
        {
            RenderMode = RenderMode.ScreenSpaceOverlay;
            PixelPerfect = false;
            SortOrder = 0;
            TargetDisplay = 0;
            CameraName = "";
        }

        public ConfigCanvas(Canvas canvas)
        {
            RenderMode    = canvas.renderMode;
            PixelPerfect  = canvas.pixelPerfect;
            SortOrder     = canvas.sortingOrder;
            TargetDisplay = canvas.targetDisplay;
            CameraName    = canvas.worldCamera.gameObject.name;
        }

        public ConfigCanvas(
            RenderMode mode = RenderMode.ScreenSpaceOverlay,
            bool pixelPerfect  = false,
            int sortOrder = 0,
            int targetDisplay = 0,
            string cameraName = ""
            )
        {
            RenderMode = mode;
            PixelPerfect = pixelPerfect;
            SortOrder = sortOrder;
            TargetDisplay = targetDisplay;
            CameraName = cameraName;
        }
        
        public static implicit operator ConfigCanvas(Canvas canvas) => new(canvas);
        
        public static implicit operator Canvas(ConfigCanvas configCanvas) => new()
        {
            renderMode = configCanvas.RenderMode,
            pixelPerfect = configCanvas.PixelPerfect,
            sortingOrder = configCanvas.SortOrder,
            targetDisplay = configCanvas.TargetDisplay,
            worldCamera = GameObject.Find(configCanvas.CameraName)?.GetComponent<Camera>()
        };

        /// <summary>
        /// Updates canvas values with ConfigCanvas values
        /// </summary>
        /// <param name="canvas">Canvas to update</param>
        /// <returns></returns>
        public Canvas UpdateCanvas(Canvas canvas)
        {
            canvas.renderMode = RenderMode;
            canvas.pixelPerfect = PixelPerfect;
            canvas.sortingOrder = SortOrder;
            canvas.targetDisplay = TargetDisplay;
            canvas.worldCamera = GameObject.Find(CameraName)?.GetComponent<Camera>();
            return canvas;
        }
        
        public override void AddComponent(GameObject go) => UpdateCanvas(go.AddComponent<Canvas>());
    }
}