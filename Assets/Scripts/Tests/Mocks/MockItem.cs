using UnityEngine;
using System.Collections;
using GridFramework;
using Math3Game.View;

namespace Tests
{
    public class MockItem : Gem
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
        public string GemType => gemType;

        public void Dispose()
        {
            
        }

        public void EnterSlot(Slot slot)
        {
            
        }

        public bool Equals(Item other)
        {
            return (other as MockItem).GemType == gemType;
        }

        public void MoveToPosition(Vector2 newPosition)
        {
            
        }

        public void OnBoardUpdate()
        {
            
        }
    }
}