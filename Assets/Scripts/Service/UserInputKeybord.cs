﻿using UnityEngine;

namespace Assets.Scripts.Service
{
    public class UserInputKeybord : MonoBehaviour, IUserInput
    {
        private KeyCode _jumpKey = KeyCode.Space;
        private int _leftButtonMouse = 0;

        public bool Jump {  get; private set; }
        public bool Attack { get; private set; }
        public float HorizontalMove { get; private set; }

        private void Update()
        {
            Jump = Input.GetKeyDown(_jumpKey);
            Attack = Input.GetMouseButtonDown(_leftButtonMouse);
            HorizontalMove = Input.GetAxis(Constants.AxisHorizontal);
        }
    }
}
