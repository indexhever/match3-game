using Math3Game.Controller;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Math3Game.View
{
    public class ExtraItemsEntering : MonoBehaviour
    {
        private const float AMOUNT_SECONDS_WAIT = 1f;

        private BoardUpdater boardUpdater;
        private int amountThatEnteredGame;
        private int amountExtraItemsCreated;

        [Inject]
        private void Construct(BoardUpdater boardUpdater)
        {
            this.boardUpdater = boardUpdater;
        }

        //private void OnTriggerExit2D(Collider2D collision)
        //{
        //    IncreaseAmountThatAlreadEntered();
        //}

        public void IncreaseAmountThatAlreadEntered()
        {
            amountThatEnteredGame++;
            Debug.Log($"Amount entered: {amountThatEnteredGame}. AmountThatShouldEnter: {amountExtraItemsCreated}");
            if (amountExtraItemsCreated == amountThatEnteredGame)
                StartPlayingAgain();
        }

        private void StartPlayingAgain()
        {
            amountThatEnteredGame = 0;
            //StartCoroutine(StartPlayingAgainCoroutine());
            boardUpdater.UpdateComplete();
        }

        private IEnumerator StartPlayingAgainCoroutine()
        {
            yield return new WaitForSeconds(AMOUNT_SECONDS_WAIT);

            boardUpdater.UpdateComplete();
        }

        public void SetAmountItemsToEnter(int amountExtraItemsCreated)
        {
            this.amountExtraItemsCreated = amountExtraItemsCreated;
        }
    }
}