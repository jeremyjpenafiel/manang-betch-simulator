using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NPCSystem
{
    public class QueueManager: MonoBehaviour
    {
        [SerializeField] private List<QueueSlot> queue;
        private readonly Queue<Npc> _npcQueue = new ();
        [Required]
        [SerializeField] private Transform exitPosition;
        [Required]
        [SerializeField] private Transform targetPositionBeforeExit;

        public void Initialize()
        {
            
            queue[0].OnNpcExited += RemoveNpcFromQueue;
            queue[^1].OnNpcEntered += NpcAddToQueue;
        }

        private void NpcAddToQueue(Npc npc)
        {
            _npcQueue.Enqueue(npc);
            npc.targetBeforeExit = targetPositionBeforeExit;
            npc.exitTransform = exitPosition;
            
            int queueIndex = _npcQueue.Count - 1;
            Action onFirstNpc = () => 
            { 
                npc.Wait(5).Forget(); 
            };
            npc.SetDestination(queue[queueIndex].transform, onFirstNpc);
        }

        private void RemoveNpcFromQueue()
        {
            if (_npcQueue.Count == 0) return;
            Npc npc = _npcQueue.Dequeue();
            RepositionNpcs();
        }
        
        private void RepositionNpcs()
        {
            for (int i = 0; i < _npcQueue.Count; i++)
            {
                Npc npc = _npcQueue.ToArray()[i];
                npc.SetDestination(queue[i].transform);
            }
        }
    }
}