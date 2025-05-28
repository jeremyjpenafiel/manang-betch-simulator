using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NPCSystem
{
    public class QueueManager : MonoBehaviour
    {
        [SerializeField] private List<QueueSlot> queue;
        private readonly Queue<Npc> _npcQueue = new();
        [Required]
        [SerializeField] private Transform exitPosition;
        [Required]
        [SerializeField] private Transform targetPositionBeforeExit;

        [Required, SerializeField] private QueueSlot paymentSectionSlot;

        [SerializeField] private float npcWaitTime = 60f;

        public void Initialize()
        {

            BeanInteraction.OnTrayPlacedInPaymentSection += MoveNpcToPaymentSection;
            paymentSectionSlot.OnNpcExited += RemoveNpcFromQueue;
            queue[^1].OnNpcEntered += NpcAddToQueue;
        }

        private void NpcAddToQueue(Npc npc)
        {
            if(_npcQueue.Count >= queue.Count)
            {
                Debug.LogWarning("Queue is full, cannot add more NPCs.");
                npc.SetDestination(exitPosition, () =>
                {
                    Destroy(npc.gameObject);
                });
                return;
            }

            _npcQueue.Enqueue(npc);
            npc.targetBeforeExit = targetPositionBeforeExit;
            npc.exitTransform = exitPosition;

            int queueIndex = _npcQueue.Count - 1;
            // if ()

            Action onFirstNpc = () =>
            {
                npc.Wait(npcWaitTime).Forget();
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
                if (i == 0)
                {
                    SetDestinationToOrderGeneratorSlot(npc).Forget();
                }
                npc.SetDestination(queue[i].transform);
            }
        }

        public async UniTask SetDestinationToOrderGeneratorSlot(Npc npc)
        {
            npc.SetDestination(queue[0].transform, async () =>
            {
                await UniTask.WaitUntil(() => npc.mustGoToPaymentSection == true);
            });
        }

        public void RegisterNPCEnterListener(Action<Npc> listener)
        {
            queue[0].OnNpcEntered += listener;
        }

        public void RegisterNPCExitListener(Action listener)
        {
            queue[0].OnNpcExited += listener;
        }

        public void MoveNpcToPaymentSection()
        {
            _npcQueue.TryPeek(out Npc npc);
            npc.mustGoToPaymentSection = true;
            npc.SetDestination(paymentSectionSlot.transform, () =>
            {
                npc.Exit();
            });

        }
    }
}