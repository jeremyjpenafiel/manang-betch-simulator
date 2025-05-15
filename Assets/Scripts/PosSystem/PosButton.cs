using System;
using UnityEngine;
using UnityEngine.UI;

namespace PosSystem
{
    public class PosButton: MonoBehaviour
    {
        private Button _button;
        private int _index;
        public event Action OnButtonPressed =  delegate {};
        
        
        public void Initialize(int buttonIndex)
        {
            _index = buttonIndex;
            _button = GetComponent<Button>();
        }

        private void Start()
        {
            _button.onClick.AddListener(()=>OnButtonPressed());
        }

        public void RegisterListener(Action listener)
        {
            OnButtonPressed += listener;
        }
    }
}