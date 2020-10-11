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
        private Sprite[] gemImages;

        public DefaultItemFactory(Gem.Factory gemFactory, Vector2 measuresInUnit, Sprite[] gemImages)
        {
            this.gemFactory = gemFactory;
            MeasuresInUnit = measuresInUnit;
            this.gemImages = gemImages;
        }

        public Vector2 MeasuresInUnit { get; private set; }

        public Item Create(Vector2 newItemPosition)
        {
            return gemFactory.Create(newItemPosition, GetRandomGemImage());
        }

        private Sprite GetRandomGemImage()
        {
            return gemImages[Random.Range(0, gemImages.Length - 1)];
        }
    }
}
