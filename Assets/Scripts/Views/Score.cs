using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Math3Game.View
{
    public class Score : MonoBehaviour, ScoreVisual
    {
        [SerializeField]
        private Text textComponent;

        public void UpdateVisual(int currentScore)
        {
            textComponent.text = currentScore.ToString();
        }
    }
}