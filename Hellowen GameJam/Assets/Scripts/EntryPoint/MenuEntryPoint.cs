using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MenuEntryPoint : EntryPoint
{
    [SerializeField] private UIController uiController;

    public override void Dispose()
    {
        
    }

    public override void Initialize()
    {
        uiController.gameObject.SetActive(true);
    }
}
