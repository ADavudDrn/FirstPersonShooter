using UnityEngine;

namespace Utility
{
    public class FpsLimiter : MonoBehaviour
    {
        [SerializeField] private int TargetFps = 60;

        private void Awake()
        {
            Application.targetFrameRate = TargetFps;
        }
    }
}