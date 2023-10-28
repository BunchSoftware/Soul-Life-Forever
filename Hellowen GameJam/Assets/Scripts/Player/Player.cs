using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int badScore = 0;
    private int goodScore = 0;

    public delegate void RecountedScore(int score);
    public event RecountedScore OnRecountedBadScore;
    public event RecountedScore OnRecountedGoodScore;

    private void Start()
    {
        OnRecountedBadScore?.Invoke(badScore);
        OnRecountedGoodScore?.Invoke(goodScore);
    }
    public void RecountScore(int badScore, int goodScore)
    {
        this.goodScore = goodScore;
        this.badScore = badScore;

        OnRecountedBadScore?.Invoke(badScore);
        OnRecountedGoodScore?.Invoke(goodScore);
    }
}
