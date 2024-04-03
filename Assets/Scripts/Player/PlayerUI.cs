using ReferenceValue;
using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TransformRef PromptTextTransform;
    private TextMeshProUGUI _promptText;

    private void Start()
    {
        _promptText = PromptTextTransform.Value.GetComponent<TextMeshProUGUI>();
    }

    public void UpdateText(string promptMessage)
    {
        _promptText.text = promptMessage;
    }
}
