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
        private BoardUpdater boardUpdater;
        private int amountThatEnteredGame;
        private int amountExtraItemsCreated;

        [Inject]
        private void Construct(BoardUpdater boardUpdater)
        {
            this.boardUpdater = boardUpdater;
        }

        public void IncreaseAmountThatAlreadEntered()
        {
            amountThatEnteredGame++;
            
            if (amountExtraItemsCreated == amountThatEnteredGame)
                StartPlayingAgain();
        }

        private void StartPlayingAgain()
        {
            amountThatEnteredGame = 0;
            boardUpdater.UpdateComplete();
        }

        public void SetAmountItemsToEnter(int amountExtraItemsCreated)
        {
            this.amountExtraItemsCreated = amountExtraItemsCreated;
        }
    }
}