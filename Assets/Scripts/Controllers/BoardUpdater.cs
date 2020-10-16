using GridFramework;
using Math3Game.View;
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
        private ExtraItemSpawner extraItemSpawner;

        public delegate void BoardUpdateAction();
        private event BoardUpdateAction OnUpdatingBoard, OnUpdateComplete;

        [Inject]
        private void Construct(SwappingInputSwitch swappingInputSwitch, ExtraItemSpawner extraItemSpawner)
        {
            this.swappingInputSwitch = swappingInputSwitch;
            this.extraItemSpawner = extraItemSpawner;            
        }

        public void Run()
        {
            swappingInputSwitch.TurnOff();
            
            extraItemSpawner.StartSpawning();
        }

        public void Stop()
        {
            OnUpdatingBoard?.Invoke();
            
        }

        public void UpdateComplete()
        {
            OnUpdateComplete?.Invoke();
        }

        public void RequireGemForColumn(int column)
        {
            extraItemSpawner.RequireGemPerColumn(column);            
        }

        public void SignOnUpdate(BoardUpdateAction boardUpdateAction)
        {
            OnUpdatingBoard += boardUpdateAction;
        }

        public void UnsignOnUpdate(BoardUpdateAction boardUpdateAction)
        {
            OnUpdatingBoard -= boardUpdateAction;
        }

        public void SignOnUpdateComplete(BoardUpdateAction boardUpdateAction)
        {
            OnUpdateComplete += boardUpdateAction;
        }

        public void UnSignOnUpdateComplete(BoardUpdateAction boardUpdateAction)
        {
            OnUpdateComplete -= boardUpdateAction;
        }
    }
}