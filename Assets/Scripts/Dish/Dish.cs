using System;
using PlayerScripts;
using UnityEngine;

namespace DefaultNamespace.Dish
{
    public class Dish: MonoBehaviour, ITrayInteractable
    {
        private DishBehavior DishBehavior;
        private void Start()
        {
            DishBehavior = GetComponent<DishBehavior>();
        }

        public void Interact(BeanInteraction beanInteraction, GameObject dish)
        {
            dish.transform.position = transform.position;
        }

        // public void Interact(BeanInteraction beanInteraction)
        // {
        // }
    }
}