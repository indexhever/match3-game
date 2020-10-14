using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Math3Game.Controller
{
    public class BoardUpdater
    {
        private SwappingInputSwitch swappingInputSwitch;
        private int amountItemsOnBoard;

        public delegate void BoardUpdateAction();
        private event BoardUpdateAction OnUpdatingBoard, OnUpdateComplete;

        [Inject]
        private void Construct(SwappingInputSwitch swappingInputSwitch)
        {
            this.swappingInputSwitch = swappingInputSwitch;
        }

        public void Run()
        {
            swappingInputSwitch.TurnOff();
            OnUpdatingBoard?.Invoke();
        }

        public void Stop()
        {
            OnUpdateComplete?.Invoke();
        }

        public void IncreaseAmountItemsOnBoard()
        {
            amountItemsOnBoard++;
            Debug.Log("Amount item: " + amountItemsOnBoard);
        }

        public void DecreaseAmountItemsOnBoard()
        {
            amountItemsOnBoard++;
            Debug.Log("Amount item: " + amountItemsOnBoard);
        }

        public void SignOnUpdate(BoardUpdateAction boardUpdateAction)
        {
            OnUpdatingBoard += boardUpdateAction;
        }

        public void SignOnUpdateComplete(BoardUpdateAction boardUpdateAction)
        {
            OnUpdateComplete += boardUpdateAction;
        }
    }
}