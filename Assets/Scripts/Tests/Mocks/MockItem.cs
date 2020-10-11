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

        public Vector2 Position 
        {
            get
            {
                return newItemPosition;
            }
            set
            {
                newItemPosition = value;
            }
        }

        public int Row { get; set; }
        public int Column { get; set; }
    }
}