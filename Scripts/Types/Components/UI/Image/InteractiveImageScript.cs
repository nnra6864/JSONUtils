using System.Linq;
using NnUtils.Scripts;
using NnUtils.Scripts.UI;
using UnityEngine;
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
            var sprite = Misc.SpriteFromFile(configImage.Image) ??
                         ImageManager.Images.FirstOrDefault(x => x.Name == configImage.Image).Sprite;
            _image.sprite = sprite;

            // Return if Sprite is null
            if (sprite == null) return;
            
            // Return if ARF is null
            if (_imageARF == null) return;
            
            // Set aspect ratio from resolution
            var rect = sprite.rect;
            _imageARF.aspectRatio = rect.width / rect.height;
        }
    }
}