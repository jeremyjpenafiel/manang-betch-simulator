using System;
using UnityEngine;

namespace NPCSystem
{
    public class NPCAnimator: MonoBehaviour
    {
        private Animator _animator;
        private static readonly int Walking = Animator.StringToHash("Walking");


        private void OnEnable()
        {
            _animator = GetComponent<Animator>();
            _animator.SetBool(Walking, true);
        }
    }
}