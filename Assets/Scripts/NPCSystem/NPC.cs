using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using Pathfinding;
using UnityEngine;
using Order;
using TMPro;

namespace NPCSystem
{
    public class Npc : Entity
    {
        // public Transaction CurrentTransaction { get; private set; }
        private AIDestinationSetter _aiDestinationSetter;
        private IAstarAI _ai;
        public Transform targetBeforeExit;
        public Transform exitTransform;
        public Order.Order order;
        public TextMeshProUGUI orderText;

        public void OnEnable()
        {
            _aiDestinationSetter = GetComponent<AIDestinationSetter>();
            _ai = GetComponent<IAstarAI>();
        }
        
        public async void SetDestination(Transform target, Action onDestinationReached = null)
        {
            try
            {
                _aiDestinationSetter.target = target;
                // onDestinationReached?.Invoke();

                if (onDestinationReached != null)
                {
                    await CheckDestinationReached(onDestinationReached);
                }
            }
            catch (NullReferenceException e)
            {
                
                Debug.LogWarning("AIDestinationSetter component is not attached to the NPC.");
                Debug.LogError("Error setting destination: " + e.Message);
            }
        }

        private async UniTask CheckDestinationReached(Action onDestinationReached)
        {
            if (_ai == null)
            {
                return;
            }
            // Wait until the destination is reached
            await UniTask.Delay(1);
            while (!_ai.reachedDestination)
            {
                await UniTask.Yield();
            }

            // Double-check the destination status
            if (_ai.reachedDestination)
            {
                // Invoke the action once the destination is reached
                onDestinationReached?.Invoke();
            }

        }

        public void Exit()
        {
            Action action = () =>
            {
                SetDestination(exitTransform);
            };
            
            SetDestination(targetBeforeExit, action);

        }

        public async UniTask Wait(float seconds)
        {
            await UniTask.WaitForSeconds(seconds);
            Exit();
            
        }

        // public void StartTransaction(Order order)
        // {
        //     string mop = "Mop"; // Replace with actual mop logic
        //     CurrentTransaction = new Transaction(order, mop);
        // }
    }
}
