using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridFramework;
using Math3Game.View;

namespace Tests
{
    public class MockItemFactory : ItemFactory<Gem>
    {
        public MockItemFactory(Vector2 itemMeasuresInUnit)
        {
            MeasuresInUnit = itemMeasuresInUnit;
        }
        public Vector2 MeasuresInUnit { get; private set; }

        public Gem Create(Vector2 newItemPosition)
        {
            return new MockItem(newItemPosition, "");
        }
        public Gem Create(Vector2 newItemPosition, string gemType)
        {
            return new MockItem(newItemPosition, gemType);
        }

        public Gem CreateNull()
        {
            return new NullGem();
        }
    }
}