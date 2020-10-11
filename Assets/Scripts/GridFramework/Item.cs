﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridFramework
{
    public interface Item
    {
        Vector2 Position { get; set; }
        int Row { get; set; }
        int Column { get; set; }
    }
}
