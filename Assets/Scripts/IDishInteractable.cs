using PlayerScripts;
using UnityEngine;

public interface IDishInteractable
{
    void Interact(BeanInteraction beanInteraction, GameObject dish);
}