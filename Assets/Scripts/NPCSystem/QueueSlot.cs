using System;
using UnityEngine;

namespace NPCSystem
{
    public class QueueSlot: MonoBehaviour
    {
        
        public event Action OnNpcExited;
        public event Action<Npc> OnNpcEntered;

        public bool IsOccupied { get; private set; }
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("NPC")) return;
            
            IsOccupied = true;
            if (other.TryGetComponent(out Npc npc))
            {
                OnNpcEntered?.Invoke(npc);
                
            }else
            {
                Debug.LogWarning("Collider does not have an NPC component: " + other.name);
            }
            Debug.Log("NPC entered the slot: " + other.name);
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("NPC")) return;
            IsOccupied = false;
            // Optionally, you can add logic to handle when an NPC exits the slot
            Debug.Log("NPC exited the slot: " + other.name);
            OnNpcExited?.Invoke();
        }
    }
}