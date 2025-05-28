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
        [Button]
        private void PauseSpawn()
        {   
            isPaused = true;
        }

        [Button]
        private void ResumeSpawn()
        {
            isPaused = false;
        }
        
        [Required]
        [SerializeField] private QueueSlot queueStartPosition;
        
        [Required]
        [SerializeField] private Npc npcPrefab;
        [SerializeField] private float spawnFrequency = 100f;

        public bool isPaused;


        public void Spawn()
        {
            var npc = Instantiate(npcPrefab, transform.position, Quaternion.identity);
            npc.SetDestination(queueStartPosition.transform);
        }
 

        public async UniTaskVoid DoSpawn()
        {   Debug.Log("NPC Spawner started");
            while (true)
            {
                if (queueStartPosition.IsOccupied || isPaused)
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