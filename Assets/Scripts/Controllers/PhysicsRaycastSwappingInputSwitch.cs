using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Math3Game.Controller
{
    public class PhysicsRaycastSwappingInputSwitch : SwappingInputSwitch
    {
        private Physics2DRaycaster physics2DRaycaster;
        private readonly LayerMask turnOffLayerMask;
        private readonly LayerMask turnOnLayerMask;

        public PhysicsRaycastSwappingInputSwitch(Physics2DRaycaster physics2DRaycaster)
        {
            this.physics2DRaycaster = physics2DRaycaster;
            turnOffLayerMask = 1 << LayerMask.NameToLayer("UI");
            turnOnLayerMask = (1 << LayerMask.NameToLayer("UI")) | (1 << LayerMask.NameToLayer("Gem"));
        }

        public void TurnOff()
        {
            physics2DRaycaster.eventMask = turnOffLayerMask;
        }

        public void TurnOn()
        {
            physics2DRaycaster.eventMask = turnOnLayerMask;
        }
    }
}