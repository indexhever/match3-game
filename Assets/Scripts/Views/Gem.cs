﻿using GridFramework;
using Math3Game.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Math3Game.View
{
    public class Gem : MonoBehaviour, Item
    {
        private Slot currentSlot;
        private BoardUpdater boardUpdater;
        private Scorer scorer;

        [SerializeField]
        private Rigidbody2D gemRigidbody;
        [SerializeField]
        private SpriteRenderer spriteRenderer;

        public Vector2 Position 
        { 
            get
            {
                return transform.localPosition;
            }
            set
            {
                gemRigidbody.MovePosition(value);
            }
        }

        public int Row { get; set; }

        public int Column { get; set; }
        public Sprite Image { get => spriteRenderer.sprite; }

        [Inject]
        public void Construct(Vector2 initialPosition, Sprite gemImage, BoardUpdater boardUpdater, Scorer scorer)
        {
            transform.position = initialPosition;
            spriteRenderer.sprite = gemImage;
            this.boardUpdater = boardUpdater;
            this.scorer = scorer;
            boardUpdater.SignOnUpdate(OnBoardUpdate);
            boardUpdater.SignOnUpdateComplete(OnBoardComplete);
        }

        public bool Equals(Item other)
        {

            return other.Image == Image;
        }

        public void OnBoardUpdate()
        {
            gemRigidbody.isKinematic = false;
        }

        public void OnBoardComplete()
        {
            gemRigidbody.isKinematic = true;
        }

        public void EnterSlot(Slot slot)
        {
            this.currentSlot = slot;
        }

        public void Dispose()
        {
            if (!gameObject.activeInHierarchy)
                return;

            scorer.IncreaseScore();
            gameObject.SetActive(false);
            currentSlot.CleanGem();
            boardUpdater.UnsignOnUpdate(OnBoardUpdate);
            boardUpdater.UnSignOnUpdateComplete(OnBoardComplete);
        }

        public class Factory : PlaceholderFactory<Vector2, Sprite, Gem>
        {

        }
    }
}