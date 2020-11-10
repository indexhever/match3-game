using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridFramework;
using Math3Game.View;

namespace Math3Game.Controller
{
    public class DefaultItemFactory : ItemFactory<Gem>
    {
        private GemComponent.Factory gemFactory;
        private Sprite[] gemImages;
        private Scorer scorer;

        public DefaultItemFactory(
            GemComponent.Factory gemFactory, 
            Vector2 measuresInUnit, 
            Sprite[] gemImages,
            Scorer scorer)
        {
            this.gemFactory = gemFactory;
            MeasuresInUnit = measuresInUnit;
            this.gemImages = gemImages;
            this.scorer = scorer;
        }

        public Vector2 MeasuresInUnit { get; private set; }

        public Gem Create(Vector2 newItemPosition)
        {
            return gemFactory.Create(newItemPosition, GetRandomGemImage(), scorer);
        }

        public Gem CreateNull()
        {
            return new NullGem();
        }

        private Sprite GetRandomGemImage()
        {
            return gemImages[Random.Range(0, gemImages.Length - 1)];
        }
    }
}
