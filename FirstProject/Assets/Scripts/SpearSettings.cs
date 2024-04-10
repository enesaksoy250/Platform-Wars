using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearSettings : MonoBehaviour
{

    PlayerHealth playerHealth;
    BoxCollider2D boxCollider2D;
    GameObject player;
    [SerializeField] float spearFast;
    GameObject SkeletonArcher;
    EnemyManager skeletonEnemyManager;
    float direction;

    private void Start()
    {

        FindObjects();

    }


    void Update()
    {


        if (skeletonEnemyManager.isAlive)
        {

            direction = SkeletonArcher.transform.localScale.x;
            transform.Translate(spearFast * direction * Time.deltaTime, 0, 0);

        }



        if (boxCollider2D.IsTouchingLayers(LayerMask.GetMask("Main")))
        {


            Destroy(gameObject);

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (boxCollider2D.IsTouching(player.GetComponent<CapsuleCollider2D>()))
        {

            if (playerHealth.GetPlayerHealth() > 0)
            {

                playerHealth.ChangeHealth(20);
                Destroy(gameObject);

            }



        }

    }

    void FindObjects()
    {

        SkeletonArcher = GameObject.Find("SkeletonArcher");
        skeletonEnemyManager = SkeletonArcher.GetComponent<EnemyManager>();
        player = GameObject.FindWithTag("Player");
        boxCollider2D = GetComponent<BoxCollider2D>();
        playerHealth = player.GetComponent<PlayerHealth>();

    }


}
