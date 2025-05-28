using PlayerScripts;
using UnityEngine;

namespace DefaultNamespace.Dish
{
    public class Dish: MonoBehaviour, IDishInteractable
    {
        
        public void Interact(BeanInteraction beanInteraction, GameObject dish)
        {
            beanInteraction.state = PlayerStates.HandsFree;
            dish.transform.position = transform.position;
        }

        // public void Interact(BeanInteraction beanInteraction)
        // {
        // }
    }
}