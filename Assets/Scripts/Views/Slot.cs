using GridFramework;
using Math3Game.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Zenject;

namespace Math3Game.View
{
    public class Slot : MonoBehaviour, Item
    {
        private BoardUpdater boardUpdater;
        private Gem currentGem;

        public Vector2 Position { get => transform.position; set => transform.position = value; }
        public int Row { get; set; }
        public int Column { get; set; }

        // TODO: remove from Item. Use it only on Gem Items
        public Sprite Image => null;

        [Inject]
        public void Construct(Vector2 initialPosition, BoardUpdater boardUpdater)
        {
            transform.position = initialPosition;
            this.boardUpdater = boardUpdater;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            currentGem = collision.GetComponent<Gem>();
            currentGem.EnterSlot(this);
        }

        public void CleanGem()
        {
            if (currentGem == null)
                return;

            boardUpdater.RequireGemForColumn(currentGem.Column);
            currentGem = null;
        }

        // TODO: remove from item, use it only on Gem Items
        public bool Equals(Item other)
        {
            return false;
        }

        // TODO: remove from item, use it only on Gem Items
        public void Dispose()
        {
            
        }

        public class Factory : PlaceholderFactory<Vector2, Slot>
        {

        }
    }
}
