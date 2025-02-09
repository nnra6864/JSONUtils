using UnityEngine;

namespace NnUtils.Modules.JSONUtils.Scripts.Types.Components.UI.Image
{
    public class ProjectImageManagerScript : MonoBehaviour
    {
        [SerializeField] private ProjectImage[] _images;
        public ProjectImage[] Images => _images;
    }
}