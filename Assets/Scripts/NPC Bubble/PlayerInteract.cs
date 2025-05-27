using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private NPCInteractable currentNPC;

    private void OnTriggerEnter(Collider other)
    {
        Debug.LogError("entered");
        Debug.Log(other.name);
        if (other.CompareTag("NPC"))
        {
            currentNPC = other.GetComponent<NPCInteractable>();
            if (currentNPC != null)
                currentNPC.ShowSpeechBubble();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC") && currentNPC != null)
        {
            NPCInteractable exitingNPC = other.GetComponent<NPCInteractable>();
            if (exitingNPC == currentNPC)
            {
                currentNPC.HideSpeechBubble();
                currentNPC = null;
            }
        }
    }
}
