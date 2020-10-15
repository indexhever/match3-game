using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Math3Game.Controller
{
    public class GeneralInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GameController>()
                     .To<DefaultGameController>()
                     .AsSingle();
        }
    }
}

