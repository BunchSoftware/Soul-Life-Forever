using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIEntryPoint : EntryPoint
{
    [SerializeField] private int nextWinLevel;
    [SerializeField] private int nextDefeatLevel;
    [SerializeField] private int nextNeutralLevel;
    [SerializeField] private UIController uIController;
    [SerializeField] private Player player;

    public override void Dispose()
    {

    }

    public override void Initialize()
    {
        uIController.gameObject.SetActive(true);
        player.OnRecountedGoodScore += (score) => {
            uIController.goodScoreSlider.value = score;
        };
        player.OnRecountedBadScore += (score) => {
            uIController.badScoreSlider.value = score;
        };
        player.OnPeopleTrigger += (active) =>
        {
            uIController.goodExplanations.gameObject.SetActive(active);
            uIController.badExplanations.gameObject.SetActive(active);
        };
        player.OnRejoiced += (active) =>
        {
            uIController.soundManager.OnPlayOneShotAndEndLast(2);
            uIController.goodExplanations.gameObject.SetActive(!active);
            uIController.badExplanations.gameObject.SetActive(!active);
        };
        player.OnScared += (active) =>
        {
            uIController.soundManager.OnPlayOneShotAndEndLast(1);
            uIController.goodExplanations.gameObject.SetActive(!active);
            uIController.badExplanations.gameObject.SetActive(!active);
        };
        player.OnWin += () =>
        {
            uIController.LoadLevel(nextWinLevel);
        };
        player.OnDefeat += () =>
        {
            uIController.LoadLevel(nextDefeatLevel);
        };
        player.OnNeutral += () =>
        {
            uIController.LoadLevel(nextNeutralLevel);
        };
        player.Initialize();
    }
}
