using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Math3Game.View
{
    public interface ExtraItemSpawner
    {
        float SpawnerTime { get; }
        void RequireGemPerColumn(int column);
        void StartSpawning();
    }
}