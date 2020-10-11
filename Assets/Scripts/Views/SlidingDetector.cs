using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Math3Game.View
{
    public class SlidingDetector : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private const float MAX_MOVEMENT_THRESHOLD = 0.8f;
        private const float MIN_MOVEMENT_THRESHOLD = 0.1f;

        private Camera cam;
        private Vector2 startingPosition;
        private Vector2 currentPosition;
        private Vector2 movementDirectionVector;
        private float dotProductBetweenForwardAndMovement;

        [SerializeField]
        private UnityEvent OnSlidingUp;
        [SerializeField]
        private UnityEvent OnSlidingDown;
        [SerializeField]
        private UnityEvent OnSlidingRight;
        [SerializeField]
        private UnityEvent OnSlidingLeft;
        [SerializeField]
        private UnityEvent OnInitialPosition;
        [SerializeField]
        private UnityEvent OnEndSliding;

        private void Start()
        {
            cam = Camera.main;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            startingPosition = transform.localPosition;
        }

        public void OnDrag(PointerEventData eventData)
        {
            currentPosition = cam.ScreenToWorldPoint(eventData.position);
            movementDirectionVector = currentPosition - startingPosition;
            CheckDirectionOfSwapping();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            OnEndSliding?.Invoke();
        }

        private void CheckDirectionOfSwapping()
        {
            dotProductBetweenForwardAndMovement = Vector2.Dot(transform.up, movementDirectionVector.normalized);
            
            if (IsVerticalMovement())
                HandleVerticalMovement();
            else if (IsHorizontalMovement())
                HandleHorizontalMovement();
        }

        private void HandleVerticalMovement()
        {
            if (IsMovingUp())
                OnSlidingUp?.Invoke();
            else if (IsMovingDown())
                OnSlidingDown?.Invoke();
            else
                OnInitialPosition?.Invoke();
        }

        private void HandleHorizontalMovement()
        {
            if (IsMovingRight())
                OnSlidingRight?.Invoke();
            else if (IsMovinLeft())
                OnSlidingLeft?.Invoke();
            else
                OnInitialPosition?.Invoke();
        }

        private bool IsHorizontalMovement()
        {
            return Mathf.Abs(dotProductBetweenForwardAndMovement) < MIN_MOVEMENT_THRESHOLD;
        }

        private bool IsVerticalMovement()
        {
            return Mathf.Abs(dotProductBetweenForwardAndMovement) > MAX_MOVEMENT_THRESHOLD;
        }

        private bool IsMovingUp()
        {
            return movementDirectionVector.y > MAX_MOVEMENT_THRESHOLD;
        }

        private bool IsMovingDown()
        {
            return movementDirectionVector.y < -MAX_MOVEMENT_THRESHOLD;
        }

        private bool IsMovingRight()
        {
            return movementDirectionVector.x > MAX_MOVEMENT_THRESHOLD;
        }

        private bool IsMovinLeft()
        {
            return movementDirectionVector.x < -MAX_MOVEMENT_THRESHOLD;
        }
    }
}