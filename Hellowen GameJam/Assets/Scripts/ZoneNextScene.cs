using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneNextScene : MonoBehaviour
{
    [SerializeField] private UIController uIController;
    [SerializeField] private int nextScene;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            uIController.LoadLevel(nextScene);
        }
    }
}
