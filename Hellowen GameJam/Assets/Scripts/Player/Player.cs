using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int maxBadScore = 100;
    [SerializeField] private int maxGoodScore = 100;
    private int badScore = 0;
    private int goodScore = 0;
    private bool isTriggerPeople = false;
    private People currentPeople;

    public delegate void RecountedScore(int score);
    public event RecountedScore OnRecountedBadScore;
    public event RecountedScore OnRecountedGoodScore;
    public Action<bool> OnPeopleTrigger;
    public Action<bool> OnScared;
    public Action<bool> OnRejoiced;
    public Action OnWin;
    public Action OnDefeat;
    public Action OnNeutral;

    public void Initialize()
    {      
        OnRecountedBadScore?.Invoke(badScore);
        OnRecountedGoodScore?.Invoke(goodScore);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            Rejoiced();
        if (Input.GetKeyDown(KeyCode.E))
            Scared();
    }
    public void RecountBadScore(int badScore)
    {
        this.badScore += badScore;
        this.badScore = Mathf.Clamp(this.badScore, 0, maxBadScore);
        OnRecountedBadScore?.Invoke(this.badScore);
        if (this.badScore == maxBadScore-1 && this.goodScore == maxGoodScore-1)
            OnNeutral?.Invoke();
        else if (this.badScore == maxBadScore)
            OnDefeat?.Invoke();
    }
    public void RecountGoodScore(int goodScore)
    {
        this.goodScore += goodScore;
        this.goodScore = Mathf.Clamp(this.goodScore, 0, maxGoodScore);
        OnRecountedGoodScore?.Invoke(this.goodScore);
        if (this.badScore == maxBadScore-1 && this.goodScore == maxGoodScore-1)
            OnNeutral?.Invoke();
        else if(this.goodScore == maxGoodScore)
            OnWin?.Invoke();
    }
    public void Rejoiced()
    {
        if (isTriggerPeople && currentPeople.isScaredOrRejoiced == false)
        {
            RecountGoodScore(1);
            currentPeople.ScaredOrHappy();
            currentPeople.Dancing();
            OnRejoiced?.Invoke(true);
        }
    }
    public void Scared()
    {
        if (isTriggerPeople && currentPeople.isScaredOrRejoiced == false)
        {
            RecountBadScore(1);
            currentPeople.ScaredOrHappy();
            currentPeople.Terrafield();
            OnScared?.Invoke(true);
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "People")
        {
            People people = other.gameObject.GetComponent<People>();
            if (people.isScaredOrRejoiced == false)
            {
                isTriggerPeople = true;
                currentPeople = people;
                OnPeopleTrigger?.Invoke(true);
            }
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "People")
        {
            isTriggerPeople = false;
            OnPeopleTrigger?.Invoke(false);
        }
    }
}
