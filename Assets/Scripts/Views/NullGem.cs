using GridFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Math3Game.View
{
    public class NullGem : Gem
    {
        public Vector2 Position { get => Vector2.zero; set => _ = value; }
        public int Row { get => -1; set => _ = value; }
        public int Column { get => -1; set => _ = value; }

        public Sprite Image => null;

        public bool Equals(Item other)
        {
            return false;
        }

        public void Dispose()
        {

        }

        public void EnterSlot(Slot slot)
        {
            
        }

        public void OnBoardUpdate()
        {
            
        }

        public void MoveToPosition(Vector2 newPosition, Action OnArrive = null)
        {
            
        }
    }
}