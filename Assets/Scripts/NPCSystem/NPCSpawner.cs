using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NPCSystem
{
    public class NpcSpawner : MonoBehaviour
    {
        [Required]
        [SerializeField] private QueueSlot queueStartPosition;
        
        [Required]
        [SerializeField] private Npc npcPrefab;
        [SerializeField] private float spawnFrequency = 100f;


        public void Spawn()
        {
            var npc = Instantiate(npcPrefab, transform.position, Quaternion.identity);
            npc.SetDestination(queueStartPosition.transform);
        }
        

        public async UniTaskVoid DoSpawn()
        {
            while (true)
            {
                if (queueStartPosition.IsOccupied)
                {
                    await UniTask.Yield(); // Wait for the next frame
                    continue;
                }

                Spawn();
                await UniTask.Delay(TimeSpan.FromSeconds(spawnFrequency)); // Wait for the spawn frequency
            }
        }
        
        
        
    }
}