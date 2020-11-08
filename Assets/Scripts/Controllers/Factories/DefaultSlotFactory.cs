using GridFramework;
using Math3Game.View;
using Math3Game.Views;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Math3Game.Controller
{
    public class DefaultSlotFactory : ItemFactory<Slot>
    {
        private GemComponent.Factory gemFactory;
        private Slot.Factory slotFactory;
        private Sprite[] gemImages;

        public DefaultSlotFactory(Slot.Factory slotFactory, Vector2 measuresInUnit)
        {
            this.slotFactory = slotFactory;
            MeasuresInUnit = measuresInUnit;
        }

        public Vector2 MeasuresInUnit { get; private set; }

        public Slot Create(Vector2 newItemPosition)
        {
            return slotFactory.Create(newItemPosition);
        }

        // TODO: Set Slot to be an Interface and make a SlotComponent that inhere it
        public Slot CreateNull()
        {
            return new NullSlot();
        }
    }
}