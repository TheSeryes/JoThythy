﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConsumable 
{
    void Use();

    void Destroy();

    void PlaceItem();

    void RemoveItem();
}
