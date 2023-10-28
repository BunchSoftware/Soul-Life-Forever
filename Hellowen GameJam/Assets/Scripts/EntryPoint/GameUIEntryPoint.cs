using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIEntryPoint : EntryPoint
{
    [SerializeField] private UIController uIController;
    public override void Dispose()
    {

    }

    public override void Initialize()
    {
        uIController.gameObject.SetActive(true);
    }
}
