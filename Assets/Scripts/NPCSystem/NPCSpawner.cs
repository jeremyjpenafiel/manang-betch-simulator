using System;
using System.Collections;
using System.Collections.Generic;
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
        
        public IEnumerator DoSpawn()
        {
            while (true)
            {
                if (queueStartPosition.IsOccupied) yield return null;
                Spawn();
                yield return new WaitForSeconds(spawnFrequency);
            }
        }
        
        
        
    }
}