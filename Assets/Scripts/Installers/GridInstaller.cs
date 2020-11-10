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
            Container.Bind<GameGrid<Gem>>()
                     .AsSingle()
                     .WithArguments<int, int, Vector2, float>(rows, columns, gridTransform.position, offsetBetweenItens);

            Container.Bind<GameGrid<Slot>>()
                     .AsSingle()
                     .WithArguments<int, int, Vector2, float>(rows, columns, gridTransform.position, offsetBetweenItens);

            Container.BindFactory<Vector2, Sprite, Scorer, GemComponent, GemComponent.Factory>()
                     .FromMonoPoolableMemoryPool<Vector2, Sprite, Scorer, GemComponent>(
                        x => x.WithInitialSize(rows * columns)
                                .FromComponentInNewPrefab(itemPrefab)
                      );

            Container.BindFactory<Vector2, Slot, Slot.Factory>()
                     .FromComponentInNewPrefab(slotPrefab);

            // TODO: abstract ItemFactory and ExtraGemFactory to remove duplication
            Container.Bind<ItemFactory<Gem>>()
                     .To<DefaultItemFactory>()
                     .AsSingle()
                     .WithArguments<Vector2, Sprite[]>(CalculateItemMeasures(), gemImages);

            Container.Bind<ItemFactory<Slot>>()
                     .To<DefaultSlotFactory>()
                     .AsSingle()
                     .WithArguments<Vector2>(CalculateItemMeasures());

            Container.Bind<ExtraGemFactory>()
                     .AsSingle()
                     .WithArguments<Vector2, Sprite[]>(CalculateItemMeasures(), gemImages);

            Container.Bind<ExtraItemSpawner>()
                     .To<DefaultExtraItemSpawner>()
                     .FromInstance(defaultExtraItemSpawner);

        }

        private Vector2 CalculateItemMeasures()
        {
            BoxCollider2D itemCollider = itemPrefab.GetComponent<BoxCollider2D>();

            return new Vector2(
                itemCollider.size.x,
                itemCollider.size.y
            );
        }
    }
}