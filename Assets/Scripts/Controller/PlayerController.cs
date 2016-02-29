﻿using System;
using UnityEngine;
using System.Collections;
using DevilMind;
using UnityStandardAssets._2D;
using Event = DevilMind.Event;
using EventType = DevilMind.EventType;

namespace Ritualist.Controller 
{
    public class PlayerController : DevilBehaviour
    {

        [SerializeField] private bool _jump;
        [SerializeField] private PlatformerCharacter2D _characterAnimationController;

        private float _xAxisMoveValue;

        protected override void Awake()
        {
            EventsToListen.Add(EventType.ButtonClicked);
            EventsToListen.Add(EventType.ButtonReleased);
            EventsToListen.Add(EventType.RightTriggerReleased);
            EventsToListen.Add(EventType.RightTriggerClicked);
            base.Awake();
        }

        protected override void OnEvent(Event gameEvent)
        {
           if (gameEvent.Type == EventType.ButtonClicked)
            {
                switch ((InputButton) gameEvent.Parameter)
                {
                    case InputButton.A:
                        Jump();
                        break;
                    case InputButton.B:
                        break;
                    case InputButton.X:
                        break;
                }
            }
        }

        protected override void Update()
        {
            Move();
        }

        private void Jump()
        {
            _jump = true;
        }

        private void Move()
        {
            _xAxisMoveValue = MyInputManager.GetAxis(InputAxis.LeftStickX);
            _characterAnimationController.Move(_xAxisMoveValue, false, _jump);
            _jump = false;
        }
    }
}

