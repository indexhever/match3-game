using Math3Game.Controller;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Math3Game.View
{
    public class Timer : MonoBehaviour
    {
        private float remainingGameTime;
        private GameController gameController;

        [SerializeField]
        private Image timerFill;
        [SerializeField]
        private float amountGameTimeInSeconds = 120;

        [Inject]
        private void Construct(GameController gameController)
        {
            this.gameController = gameController;
        }

        private void Start()
        {
            remainingGameTime = amountGameTimeInSeconds;
            StartCoroutine(RunTimerCoroutine());
        }

        private IEnumerator RunTimerCoroutine()
        {
            while(remainingGameTime > 0)
            {
                remainingGameTime -= Time.deltaTime;
                UpdateTimeVisual();
                yield return null;
            }
            EndGame();
        }

        private void UpdateTimeVisual()
        {
            timerFill.fillAmount = remainingGameTime / amountGameTimeInSeconds;
        }

        private void EndGame()
        {
            gameController.End();
        }
    }
}