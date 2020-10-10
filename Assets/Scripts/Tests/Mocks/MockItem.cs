using UnityEngine;
using System.Collections;
using GridFramework;

namespace Tests
{
    public class MockItem : Item
    {
        private Vector2 newItemPosition;

        public MockItem(Vector2 newItemPosition)
        {
            this.newItemPosition = newItemPosition;
        }
    }
}