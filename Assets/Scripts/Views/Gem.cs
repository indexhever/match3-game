using GridFramework;
using Math3Game.Controller;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Math3Game.View
{
    public interface Gem : Item, IDisposable, IEquatable<Gem>
    {
        Sprite Image { get; }

        void EnterSlot(Slot slot);

        void MoveToPosition(Vector2 newPosition, Action OnArrive = null);
    }
}