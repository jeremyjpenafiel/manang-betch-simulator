using System;
using UnityEngine;
using UnityEngine.UI;

namespace PosSystem
{
    public class PosButton : MonoBehaviour
    {
        private Button _button;
        private int _index;
        public event Action OnButtonPressed = delegate { };


        public void Initialize(int buttonIndex)
        {
            _index = buttonIndex;
            _button = GetComponent<Button>();
            _button.onClick.AddListener(() => OnButtonPressed());
        }


        public void RegisterListener(Action listener)
        {
            Debug.Log("hehe");
            OnButtonPressed += listener;
        }

        public void SetInteractable(bool interactable)
        {
            Debug.Log($"Setting button interactable: {interactable} for button index: {_index}");
            if (_button != null)
            {
                _button.interactable = interactable;
            }
            else
            {
                Debug.LogWarning("Button component is not initialized.");
            }
        }
    }
}