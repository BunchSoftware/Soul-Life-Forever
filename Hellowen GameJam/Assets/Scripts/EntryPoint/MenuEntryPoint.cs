using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MenuEntryPoint : EntryPoint
{
    [SerializeField] private MenuUIController menuUIController;

    public override void Dispose()
    {
        
    }

    public override void Initialize()
    {
        menuUIController.gameObject.SetActive(true);
    }
}
