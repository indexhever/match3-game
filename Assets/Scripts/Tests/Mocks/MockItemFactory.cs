using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridFramework;

namespace Tests
{
    public class MockItemFactory : ItemFactory
    {
        public MockItemFactory(Vector2 itemMeasuresInUnit)
        {
            MeasuresInUnit = itemMeasuresInUnit;
        }
        public Vector2 MeasuresInUnit { get; private set; }

        public Item Create(Vector2 newItemPosition)
        {
            return new MockItem(newItemPosition, "");
        }
        public Item Create(Vector2 newItemPosition, string gemType)
        {
            return new MockItem(newItemPosition, gemType);
        }
    }
}