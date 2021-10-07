using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridFramework
{
    public interface Item : IEquatable<Item>, IDisposable
    {
        Vector2 Position { get; set; }
        int Row { get; set; }
        int Column { get; set; }
        Sprite Image { get; }
        void Destroy();
    }
}