using UnityEngine;
using System.Collections;
using GridFramework;

namespace Tests
{
    public class MockItem : Item
    {
        private Vector2 newItemPosition;
        private string gemType;

        public MockItem(Vector2 newItemPosition, string gemType)
        {
            this.newItemPosition = newItemPosition;
            this.gemType = gemType;
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

        public Sprite Image => throw new System.NotImplementedException();
        public void Destroy() {
            
        }

        public string GemType => gemType;

        public void Dispose()
        {
            
        }

        public bool Equals(Item other)
        {
            return (other as MockItem).GemType == gemType;
        }
    }
}