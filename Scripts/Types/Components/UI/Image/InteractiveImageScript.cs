using System;
using System.Collections;
using System.Linq;
using NnUtils.Scripts;
using NnUtils.Scripts.UI;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace NnUtils.Modules.JSONUtils.Scripts.Types.Components.UI.Image
{
    public class InteractiveImageScript : MonoBehaviour
    {
        private static ProjectImageManagerScript ImageManager => NnManager.ImageManager;

        private UnityEngine.UI.Image _image;
        private AspectRatioFitter _imageARF;

        public void LoadImage(ConfigImage configImage)
        {
            this.StopRoutine(ref _webImageRoutine);
            this.RestartRoutine(ref _loadImageRoutine, LoadImageRoutine(configImage));
        }

        private Coroutine _loadImageRoutine, _webImageRoutine;
        private IEnumerator LoadImageRoutine(ConfigImage configImage)
        {
            // Destroy components if they already exist
            if (_image != null) DestroyImmediate(_image);
            if (_imageARF != null) DestroyImmediate(_imageARF);
            
            // Add components
            if (configImage.Envelope)
            {
                _image    = gameObject.AddComponent<EnvelopedImage>();
                _imageARF = gameObject.GetComponent<AspectRatioFitter>();
            }
            else _image = gameObject.AddComponent<UnityEngine.UI.Image>();
            
            // Try to load the sprite from a file or project images and assign it to the Image component
            var sprite = ImageManager.Images.FirstOrDefault(x => x.Name == configImage.Image).Sprite ??
                         Misc.SpriteFromFile(configImage.Image);
            
            // Try to load the sprite from the web
            if (sprite == null)
            {
                var isDone = false;
                _webImageRoutine = StartCoroutine(Misc.SpriteFromURL(configImage.Image, s =>
                {
                    sprite = s;
                    isDone = true;
                }));
                yield return new WaitUntil(() => isDone);
                _webImageRoutine = null;
            }
            
            // Update the Image component's sprite
            _image.sprite = sprite;
        }
    }
}