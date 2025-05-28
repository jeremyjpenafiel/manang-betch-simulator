using DefaultNamespace;
using PlayerScripts;
using UnityEngine;

namespace TrashCan
{
    public class TrashCan : MonoBehaviour, ITrayInteractable
    {
        [SerializeField] private PlayerStatistics _playerStatistics;


        public void Interact(BeanInteraction beanInteraction, GameObject dish)
        {
            beanInteraction.state = PlayerStates.HandsFree;
            _playerStatistics.ThrownFoodTimes++;
            Destroy(dish);
            
        }
    }
}
