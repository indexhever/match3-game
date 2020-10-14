using Math3Game.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Math3Game.View
{
    public class Slot : MonoBehaviour
    {
        private BoardUpdater boardUpdater;

        [Inject]
        public void Construct(Vector2 initialPosition, BoardUpdater boardUpdater)
        {
            transform.position = initialPosition;
            this.boardUpdater = boardUpdater;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            
        }

        private void OnTriggerExit2D(Collider2D collision)
        {

        }

        public class Factory : PlaceholderFactory<Vector2, Slot>
        {

        }
    }
}
