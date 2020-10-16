using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridFramework
{
    public class NullItem : Item
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
    }
}