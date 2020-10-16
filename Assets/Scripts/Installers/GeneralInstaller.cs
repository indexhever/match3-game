﻿using Math3Game.View;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Math3Game.Controller
{
    public class GeneralInstaller : MonoInstaller
    {
        [SerializeField]
        private Score scoreVisual;
        [SerializeField]
        private ExtraItemsEntering extraItemsEntering;

        public override void InstallBindings()
        {
            Container.Bind<GameController>()
                     .To<DefaultGameController>()
                     .AsSingle();

            Container.Bind<ScoreVisual>()
                     .FromInstance(scoreVisual)
                     .AsSingle();

            Container.Bind<Scorer>()
                     .AsSingle();

            Container.Bind<ExtraItemsEntering>()
                     .FromInstance(extraItemsEntering)
                     .AsSingle();
        }
    }
}