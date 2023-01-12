using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LifeIsTheGame.TechnicalTest
{
    public class GameManager : Singleton<GameManager>
    {
        public IPlayer player { get; private set; }
        public IInputActions inputActions { get; private set; }

        public void SetPlayer(IPlayer player)
        {
            this.player = player;
        }

        public void SetInputActions(IInputActions inputActions)
        {
            this.inputActions = inputActions;
        }
    }
}