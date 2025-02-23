using Newtonsoft.Json;
using NnUtils.Scripts.UI.Slideshow;

namespace NnUtils.Modules.JSONUtils.Scripts.Types.Components.UI.Image
{
    public class ConfigSlideshowImage
    {
        [JsonProperty] public ConfigImage Image;
        [JsonProperty] public ConfigSlideshowTransition Transition;

        public ConfigSlideshowImage() : this(new(), new()) { }

        public ConfigSlideshowImage(SlideshowImage s)
        {
            Image = new(s.Image);
            Transition = s.Transition;
        }
        
        public ConfigSlideshowImage(ConfigImage image, ConfigSlideshowTransition transition)
        {
            Image      = image;
            Transition = transition;
        }
        
        //public static implicit operator SlideshowImage(ConfigSlideshowImage s) => new(s.Image, s.Transition);
        public static implicit operator ConfigSlideshowImage(SlideshowImage s) => new(s);
    }
}