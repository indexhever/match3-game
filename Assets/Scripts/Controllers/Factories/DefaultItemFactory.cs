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

        public DefaultItemFactory(Gem.Factory gemFactory, Vector2 measuresInUnit)
        {
            this.gemFactory = gemFactory;
            MeasuresInUnit = measuresInUnit;
        }

        public Vector2 MeasuresInUnit { get; private set; }

        public Item Create(Vector2 newItemPosition)
        {
            return gemFactory.Create(newItemPosition);
        }
    }
}
