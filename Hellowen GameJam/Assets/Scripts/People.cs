using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class People : MonoBehaviour
{
    [SerializeField] private GameObject terrafield;
    [SerializeField] private GameObject angry;
    [SerializeField] private GameObject dancing;
    [SerializeField] private float fearTime = 20;
    [HideInInspector] public bool isScaredOrRejoiced = false;

    private void Start()
    {
        Angry();
    }

    public void ScaredOrHappy()
    {
        isScaredOrRejoiced = true;
        StartCoroutine(ScaredOrHappyIE());
    }

    private IEnumerator ScaredOrHappyIE()
    {
        yield return new WaitForSeconds(fearTime);
        isScaredOrRejoiced = false;
    }

    public void Angry()
    {
        terrafield.SetActive(false);
        dancing.SetActive(false);
        angry.SetActive(true);
    }

    public void Dancing()
    {
        terrafield.SetActive(false);
        angry.SetActive(false);
        dancing.SetActive(true);
    }

    public void Terrafield()
    {
        angry.SetActive(false);
        dancing.SetActive(false);
        terrafield.SetActive(true); 
    }
}
