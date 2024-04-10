using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CPGetPlayerInformation : MonoBehaviour
{

    PlayerHealth playerHealth;
    CheckPointManager checkPointManager;
    public float health;

    private void Awake()
    {

        health = 100;

    }

    void Start()
    {

        checkPointManager = FindObjectOfType<CheckPointManager>();
        playerHealth = FindObjectOfType<PlayerHealth>();


    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {


            health = playerHealth.GetPlayerHealth();
            checkPointManager.SetCheckpoint(transform.position);


        }
    }



}
