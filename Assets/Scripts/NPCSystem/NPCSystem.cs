using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Order;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace NPCSystem
{
    public class NpcSystem: MonoBehaviour
    {
        [Required, SerializeField] private QueueManager queueManager;
        [Required, SerializeField] private NpcSpawner npcSpawner;
        [Required, SerializeField] private OrderSystem orderSystem;

        private void ConnectManagerToSpawner()
        {
            orderSystem.Initialize();
            queueManager.Initialize();
            queueManager.RegisterNPCEnterListener(orderSystem.OnNewCustomer);
            queueManager.RegisterNPCExitListener(orderSystem.ResetOrder);
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