using Math3Game.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Math3Game.Installer
{
    public class SwappingInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Swapper>()
                     .AsTransient();

            Container.Bind<ItemSearcher>()
                     .To<GridBasedItemSearcher>()
                     .AsSingle();
        }
    }
}