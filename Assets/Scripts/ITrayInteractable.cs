using PlayerScripts;
using UnityEngine;

namespace DefaultNamespace
{
    public interface ITrayInteractable
    {
        void Interact(BeanInteraction beanInteraction, GameObject tray);
    }
}