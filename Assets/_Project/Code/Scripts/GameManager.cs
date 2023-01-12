using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LifeIsTheGame.TechnicalTest
{
    public class GameManager : Singleton<GameManager>
    {
        public IPlayer Player { get; private set; }

        public void SetPlayer(IPlayer player)
        {
            Player = player;
        }
    }
}