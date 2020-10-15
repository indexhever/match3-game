using Math3Game.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Zenject;

namespace Math3Game.View
{
    public class Slot : MonoBehaviour
    {
        private BoardUpdater boardUpdater;
        private Gem currentGem;

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

        public class Factory : PlaceholderFactory<Vector2, Slot>
        {

        }
    }
}
