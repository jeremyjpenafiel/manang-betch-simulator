using System;
using UnityEngine;
using UnityEngine.UI;

public class IngredientButton : MonoBehaviour
{
    private int _index;
    private Button _button;
    public event Action<int> OnButtonPressed = delegate { };

    public void Initialize(int buttonIndex)
    {
        _index = buttonIndex;
        _button = GetComponent<Button>();
    }

    private void Start()
    {
        _button.onClick.AddListener(()=>OnButtonPressed(_index));
    }
    
    public void RegisterListener(Action<int> listener)
    {
        OnButtonPressed += listener;
    }
    
    public void ChangeInteractable(bool interactable)
    {
        _button.interactable = interactable;
    }
    
}
