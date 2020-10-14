using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridFramework;
using Math3Game.View;

namespace Math3Game.Controller
{
    public class DefaultItemFactory : ItemFactory
    {
        private Gem.Factory gemFactory;
        private Slot.Factory slotFactory;
        private Sprite[] gemImages;

        public DefaultItemFactory(Gem.Factory gemFactory, Slot.Factory slotFactory, Vector2 measuresInUnit, Sprite[] gemImages)
        {
            this.gemFactory = gemFactory;
            this.slotFactory = slotFactory;
            MeasuresInUnit = measuresInUnit;
            this.gemImages = gemImages;
        }

        public Vector2 MeasuresInUnit { get; private set; }

        public Item Create(Vector2 newItemPosition)
        {
            slotFactory.Create(newItemPosition);
            return gemFactory.Create(newItemPosition, GetRandomGemImage());
        }

        private Sprite GetRandomGemImage()
        {
            return gemImages[Random.Range(0, gemImages.Length - 1)];
        }
    }
}
