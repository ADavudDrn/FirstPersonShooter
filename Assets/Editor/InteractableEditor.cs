using UnityEditor;

[CustomEditor(typeof(Interactable), true)]
public class InteractableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Interactable interactable = (Interactable)target;
        if (target.GetType() == typeof(EventOnlyInteractable))
        {
            interactable.PromptMessage = EditorGUILayout.TextField("Prompt Message", interactable.PromptMessage);
            EditorGUILayout.HelpBox("EventOnlyInteractable can ONLY use UnityEvents", MessageType.Info);
            if (!interactable.TryGetComponent(out InteractionEvent interactionEvent))
            {
                interactable.UseEvents = true;
                interactable.gameObject.AddComponent<InteractionEvent>();
            }
        }
        else
        {

            base.OnInspectorGUI();
            if (interactable.UseEvents)
            {
                if (!interactable.TryGetComponent(out InteractionEvent interactionEvent))
                    interactable.gameObject.AddComponent<InteractionEvent>();
            }
            else
            {
                if (interactable.TryGetComponent(out InteractionEvent interactionEvent))
                    DestroyImmediate(interactionEvent);
            }
        }
    }
}
