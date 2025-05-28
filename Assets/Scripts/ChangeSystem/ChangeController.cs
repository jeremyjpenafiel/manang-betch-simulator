using System.Collections.Generic;
using UnityEngine;

namespace ChangeSystem
{
    public class ChangeController
    {
        private ChangeView _changeView;
        private float _changeAmount;

        public ChangeController(ChangeView changeView)
        {
            _changeView = changeView;
            _changeAmount = 0f;

            ConnectView();
        }

        private void ConnectView()
        {
            _changeView.UpdateChangeAmount(_changeAmount);
        }

        public class Builder
        {
            public ChangeController Build(ChangeView changeView)
            {
                return new ChangeController(changeView);
            }
        }



    }
}