using UnityEngine;
using System.Collections;
using Math3Game.View;

namespace Tests
{
    public class MockMatchScannerTrigger : MatchScannerTrigger
    {
        public bool WasCalled { get; internal set; }

        public void Scan()
        {
            WasCalled = true;
        }
    }
}