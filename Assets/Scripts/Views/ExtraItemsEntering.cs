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
        private const float AMOUNT_SECONDS_WAIT = 2f;

        private BoardUpdater boardUpdater;
        private int amountThatEnteredGame;
        private int amountExtraItemsCreated;

        [Inject]
        private void Construct(BoardUpdater boardUpdater)
        {
            this.boardUpdater = boardUpdater;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            amountThatEnteredGame++;
            if (amountExtraItemsCreated == amountThatEnteredGame)
                StartPlayingAgain();
        }

        private void StartPlayingAgain()
        {
            Debug.Log($"Start playing again.");
            StartCoroutine(StartPlayingAgainCoroutine());
        }

        private IEnumerator StartPlayingAgainCoroutine()
        {
            yield return new WaitForSeconds(AMOUNT_SECONDS_WAIT);

            boardUpdater.UpdateComplete();
        }

        public void SetAmountItemsToEnter(int amountExtraItemsCreated)
        {
            this.amountExtraItemsCreated = amountExtraItemsCreated;
            Debug.Log("Amount intems should Enter: " + amountExtraItemsCreated);
        }
    }
}