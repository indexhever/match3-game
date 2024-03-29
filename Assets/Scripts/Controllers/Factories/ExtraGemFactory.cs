﻿using Math3Game.View;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Math3Game.Controller
{
    public class ExtraGemFactory
    {
        private Gem.Factory gemFactory;
        private Sprite[] gemImages;

        public ExtraGemFactory(Gem.Factory gemFactory, Vector2 measuresInUnit, Sprite[] gemImages)
        {
            this.gemFactory = gemFactory;
            MeasuresInUnit = measuresInUnit;
            this.gemImages = gemImages;
        }

        public Vector2 MeasuresInUnit { get; private set; }

        public Gem Create(Vector2 newItemPosition)
        {
            return gemFactory.Create(newItemPosition, GetRandomGemImage());
        }

        private Sprite GetRandomGemImage()
        {
            return gemImages[Random.Range(0, gemImages.Length - 1)];
        }
    }
}