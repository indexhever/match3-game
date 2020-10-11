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

        private void Start()
        {
            swapper.Initialize(currentItem);
        }

        public void OnSlidingUp()
        {
            if (isSwapping)
                return;

            isSwapping = true;
            swapper.SwapUp();
        }

        public void OnSlidingDown()
        {
            if (isSwapping)
                return;

            isSwapping = true;
            swapper.SwapDown();
        }

        public void OnSlidingRight()
        {
            if (isSwapping)
                return;

            isSwapping = true;
            swapper.SwapRight();
        }

        public void OnSlidingLeft()
        {
            if (isSwapping)
                return;

            isSwapping = true;
            swapper.SwapLeft();
        }

        public void OnInitialPosition()
        {
            isSwapping = false;
            swapper.Reset();
        }

        public void OnEnd()
        {
            isSwapping = false;
        }
    }
}