using GridFramework;
using Math3Game.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Math3Game.View
{
    public interface Gem : Item
    {
        void EnterSlot(Slot slot);
        void OnBoardUpdate();
    }
}