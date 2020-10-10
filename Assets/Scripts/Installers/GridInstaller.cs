﻿using System.Collections;
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

        public override void InstallBindings()
        {
            Container.Bind<GameGrid>()
                     .AsSingle()
                     .WithArguments<int, int, Vector2, float>(rows, columns, gridTransform.position, offsetBetweenItens);

            Container.BindFactory<Vector2, Gem, Gem.Factory>()
                     .FromComponentInNewPrefab(itemPrefab);

            Container.Bind<ItemFactory>()
                     .To<DefaultItemFactory>()
                     .AsSingle()
                     .WithArguments<Vector2>(CalculateItemMeasures());
        }

        private Vector2 CalculateItemMeasures()
        {
            SpriteRenderer spriteRenderer = itemPrefab.GetComponent<SpriteRenderer>();

            return new Vector2(
                spriteRenderer.sprite.texture.width / spriteRenderer.sprite.pixelsPerUnit,
                spriteRenderer.sprite.texture.height / spriteRenderer.sprite.pixelsPerUnit
            );            
        }
    }
}