using GridFramework;
using Math3Game.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

        [SerializeField]
        private Text label;
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

        private int row;
        private int column;
        public int Row {
            get => row;
            set {
                row = value;
                UpdateText();
            }
        }

        public int Column {
            get => column;
            set {
                column = value;
                UpdateText();
            }
        }
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

        public bool Equals(Item other) {
            if (!gameObject.activeInHierarchy) {
                return false;
            }
            return other?.Image == Image;
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

        public void Destroy() {
            GameObject.Destroy(gameObject);
        }

        private void UpdateText() {
            label.text = "[" +Row +  "," + Column+  "]";
        }
        
        public class Factory : PlaceholderFactory<Vector2, Sprite, Gem>
        {

        }
    }
}