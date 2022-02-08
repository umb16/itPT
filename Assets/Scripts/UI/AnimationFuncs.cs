using UnityEngine;

namespace Iptf.RadialMenuVisual
{
    public static class AnimationFuncs
    {
        public static float Smooth(float x)
        {
            x = Mathf.Clamp01(x);
            return x * x * (3 - 2 * x);
        }
    }
}
