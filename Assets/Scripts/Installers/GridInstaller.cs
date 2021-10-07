using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using GridFramework;
using Math3Game.View;
using Math3Game.Controller;
using System;

namespace Math3Game.Installer
{
    public class GridInstaller : MonoInstaller
    {
        [SerializeField]
        private Transform gridTransform;
        [SerializeField]
        private int rows;
        [SerializeField]
        private int columns;
        [SerializeField]
        private float offsetBetweenItens;
        [SerializeField]
        private GameObject itemPrefab;
        [SerializeField]
        private GameObject slotPrefab;
        [SerializeField]
        private Sprite[] gemImages;
        [SerializeField]
        private DefaultExtraItemSpawner defaultExtraItemSpawner;

        public override void InstallBindings()
        {
            Container.Bind<GameGrid>()
                     .AsSingle()
                     .WithArguments<int, int, Vector2, float>(rows, columns, gridTransform.position, offsetBetweenItens);

            Container.BindFactory<Vector2, Sprite, Gem, Gem.Factory>()
                     .FromComponentInNewPrefab(itemPrefab);

            // TODO: abstract ItemFactory and ExtraGemFactory to remove duplication
            Container.Bind<ItemFactory>()
                     .To<DefaultItemFactory>()
                     .AsSingle()
                     .WithArguments<Vector2, Sprite[]>(CalculateItemMeasures(), gemImages);

            Container.Bind<ExtraGemFactory>()
                     .AsSingle()
                     .WithArguments<Vector2, Sprite[]>(CalculateItemMeasures(), gemImages);

            Container.Bind<ExtraItemSpawner>()
                     .To<DefaultExtraItemSpawner>()
                     .FromInstance(defaultExtraItemSpawner);


            Container.BindFactory<Vector2, Slot, Slot.Factory>()
                     .FromComponentInNewPrefab(slotPrefab);
        }

        private Vector2 CalculateItemMeasures()
        {
            var itemCollider = itemPrefab.GetComponent<BoxCollider2D>();

            var size = itemCollider.size;
            return new Vector2(
                size.x,
                size.y
            );
        }
    }
}