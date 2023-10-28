using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameplayEntryPoint : EntryPoint
{
    [Header("Player")]
    [SerializeField] private PlayerMove playerMove;
    [SerializeField] private Player player;
    [Header("Sound")]
    [SerializeField] private MusicManager musicManager;
    [SerializeField] private MusicManager soundManager;

    public override void Dispose()
    {
    }

    public override void Initialize()
    {
        playerMove.gameObject.SetActive(true);
        musicManager.gameObject.SetActive(true);
        soundManager.gameObject.SetActive(true);
    }
}
