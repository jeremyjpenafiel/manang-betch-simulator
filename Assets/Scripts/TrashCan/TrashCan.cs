using UnityEngine;

namespace TrashCan
{
    public class TrashCan : MonoBehaviour, IInteractable
    {
        [SerializeField] private PlayerStatistics _playerStatistics;
        public void Interact()
        {
            _playerStatistics.ThrownFoodTimes++;
        }
    }
}
