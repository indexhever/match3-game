using Math3Game.View;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Math3Game.Controller
{
    public class Scorer
    {
        private const int DEFAULT_POINT_PER_GEM = 2;

        private ScoreVisual scoreVisual;
        private int currentScore;

        public Scorer(ScoreVisual scoreVisual)
        {
            this.scoreVisual = scoreVisual;
        }

        public void IncreaseScore()
        {
            currentScore += DEFAULT_POINT_PER_GEM;
            scoreVisual.UpdateVisual(currentScore);
        }
    }
}