using ReferenceValue;
using TMPro;
using UnityEngine;

namespace Player
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI PromptTextTransform;

        public void UpdateText(string promptMessage)
        {
                PromptTextTransform.text = promptMessage;
        }
    }
}
