using System;
using System.Collections;
using Pathfinding;
using UnityEngine;

namespace NPCSystem
{
    public class Npc : Entity
    {
        public Transaction CurrentTransaction { get; private set; }
        private AIDestinationSetter _aiDestinationSetter;
        private IAstarAI _ai;
        public Transform targetBeforeExit;
        public Transform exitTransform;

        public void OnEnable()
        {
            _aiDestinationSetter = GetComponent<AIDestinationSetter>();
            _ai = GetComponent<IAstarAI>();
        }
        
        public void SetDestination(Transform target, Action onDestinationReached = null)
        {
            Debug.LogWarning("Going to destination " + target.name);
            try
            {
                _aiDestinationSetter.target = target;
                // onDestinationReached?.Invoke();

                if (onDestinationReached != null)
                {
                    StartCoroutine(CheckDestinationReached(onDestinationReached));
                }
            }
            catch (NullReferenceException e)
            {
                
                Debug.LogWarning("AIDestinationSetter component is not attached to the NPC.");
            }
        }

        private IEnumerator CheckDestinationReached(Action onDestinationReached)
        {
            if (_ai == null)
            {
                yield break;
            }

            Debug.Log("Checking");
            Debug.Log(_ai.reachedDestination);

            // Wait until the destination is reached
            yield return new WaitForSeconds(0.1f);
            while (!_ai.reachedDestination)
            {
                yield return null;
            }

            // Add a small delay to ensure stability

            // Double-check the destination status
            Debug.Log("Reached destination: " + _ai.reachedDestination);
            if (_ai.reachedDestination)
            {
                // Invoke the action once the destination is reached
                Debug.LogWarning("Destination reached");
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

        public IEnumerator Wait(float seconds)  
        {
            yield return new WaitForSeconds(seconds);
            Exit();
            
        }

        public void StartTransaction(Order order)
        {
            string mop = "Mop"; // Replace with actual mop logic
            CurrentTransaction = new Transaction(order, mop);
        }
    }
}
