using UnityEngine;

namespace NnUtils.Modules.JSONUtils.Scripts.Types.Components.UI.Image
{
    public class ImageManagerScript : MonoBehaviour
    {
        [SerializeField] private ProjectImage[] _images;
        public ProjectImage[] Images => _images;
    }
}