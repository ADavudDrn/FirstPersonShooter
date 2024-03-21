using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Utility
{
    public class RandomChildOnEnable : MonoBehaviour
    {

        private void OnEnable()
        {
            ActivateRandomChild();
        }

        
        [Button]
        private void ActivateRandomChild()
        {
            var rand = Random.Range(0, transform.childCount);
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(child.GetSiblingIndex() == rand);
            }
        }
    }
}