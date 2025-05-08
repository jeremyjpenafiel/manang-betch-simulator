using System;
using UnityEngine;
using UnityEngine.UI;

public class IngredientButton : MonoBehaviour
{
    private int index;
    public event Action<int> OnButtonPressed = delegate { };

    public void Initialize(int buttonIndex)
    {
        index = buttonIndex;
    }

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(()=>OnButtonPressed(index));
    }
    
    public void RegisterListener(Action<int> listener)
    {
        OnButtonPressed += listener;
    }
}
