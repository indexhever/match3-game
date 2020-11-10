using Math3Game.View;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Math3Game.Controller
{
    public class ExtraGemFactory
    {
        private GemComponent.Factory gemFactory;
        private Scorer scorer;
        private Sprite[] gemImages;

        public ExtraGemFactory(
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

        private Sprite GetRandomGemImage()
        {
            return gemImages[Random.Range(0, gemImages.Length - 1)];
        }
    }
}