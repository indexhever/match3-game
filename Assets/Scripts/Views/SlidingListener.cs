using Math3Game.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Math3Game.View
{
    public class SlidingListener : MonoBehaviour
    {
        private Swapper swapper;
        private bool isSwapping;

        [SerializeField]
        private Gem currentItem;

        [Inject]
        private void Construct(Swapper swapper)
        {
            this.swapper = swapper;
        }

        public void OnSlidingUp()
        {
            if (isSwapping)
                return;

            isSwapping = true;
            swapper.SwapUp(currentItem);
        }

        public void OnSlidingDown()
        {
            if (isSwapping)
                return;

            isSwapping = true;
            swapper.SwapDown(currentItem);
        }

        public void OnSlidingRight()
        {
            if (isSwapping)
                return;

            isSwapping = true;
            swapper.SwapRight(currentItem);
        }

        public void OnSlidingLeft()
        {
            if (isSwapping)
                return;

            isSwapping = true;
            swapper.SwapLeft(currentItem);
        }

        public void OnInitialPosition()
        {
            isSwapping = false;
        }

        public void OnEnd()
        {
            isSwapping = false;
        }
    }
}