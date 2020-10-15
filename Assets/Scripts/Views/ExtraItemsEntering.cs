using Math3Game.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Math3Game.View
{
    public class ExtraItemsEntering : MonoBehaviour
    {
        private BoardUpdater boardUpdater;
        private int amountThatEnteredGame;

        [Inject]
        private void Construct(BoardUpdater boardUpdater)
        {
            this.boardUpdater = boardUpdater;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            amountThatEnteredGame++;
            Debug.Log("AmountEnteredGame: " + amountThatEnteredGame);
        }
    }
}