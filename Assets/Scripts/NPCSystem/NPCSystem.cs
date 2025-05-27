using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace NPCSystem
{
    public class NpcSystem: MonoBehaviour
    {
        [SerializeField] private QueueManager queueManager;
        [SerializeField] private NpcSpawner npcSpawner;

        private void ConnectManagerToSpawner()
        {
            queueManager.Initialize();
            // queueManager.OnFirstNpcLeft += npcSpawner.Spawn
            npcSpawner.DoSpawn().Forget();
        }

        public void Start()
        {
            ConnectManagerToSpawner();
            try
            {
                if (queueManager == null)
                {
                    throw new NullReferenceException("QueueManager is not assigned.");
                }
                if (npcSpawner == null)
                {
                    throw new NullReferenceException("NPCSpawner is not assigned.");
                }
                
            }catch (Exception e)
            {
                Debug.LogError("Failed to spawn NPC: " + e.Message);
            }
        }
    }
}