using Math3Game.Controller;
using Math3Game.View;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Math3Game.Installer
{
    public class SwappingInstaller : MonoInstaller
    {
        [SerializeField]
        private Physics2DRaycaster physics2DRaycaster;
        [SerializeField]
        private MatchScanner matchScanner;

        public override void InstallBindings()
        {
            Container.Bind<Swapper>()
                     .AsTransient();

            Container.Bind<ItemSearcher>()
                     .To<GridBasedItemSearcher>()
                     .AsSingle();

            Container.Bind<Physics2DRaycaster>()
                     .FromInstance(physics2DRaycaster)
                     .AsSingle();

            Container.Bind<SwappingInputSwitch>()
                     .To<PhysicsRaycastSwappingInputSwitch>()
                     .AsSingle();

            Container.Bind<MatchScannerTrigger>()
                     .FromInstance(matchScanner);

            Container.Bind<BoardUpdater>()
                     .AsSingle();
        }
    }
}