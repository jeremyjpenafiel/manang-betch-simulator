using Sirenix.OdinInspector;
using UnityEngine;

public class NPCInteractable : MonoBehaviour
{
    [Required]
    [SerializeField] private GameObject speechBubble;  // Assign in Inspector

    private void Awake()
    {
        if (speechBubble != null)
            speechBubble.SetActive(false);
    }

    public void ShowSpeechBubble()
    {
        if (speechBubble != null)
            speechBubble.SetActive(true);
    }

    public void HideSpeechBubble()
    {
        if (speechBubble != null)
            speechBubble.SetActive(false);
    }
}
