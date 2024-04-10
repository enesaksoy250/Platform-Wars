using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSettings : MonoBehaviour
{


    [SerializeField] float waitingTime;
    [SerializeField] float remainingDistance;
    [SerializeField] BoxCollider2D myBoxCollider;
    [SerializeField] Rigidbody2D myRigidbody2d;
    GameObject player;
    PlayerHealth playerHealthScript;
    PlayerMove playerMoveScript;
    EnemyManager enemyManagerScript;
    bool isKill = true;




    void Start()
    {
        FindObject();
    }


    void Update()
    {

        RemainingDistance();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.CompareTag("Player") && isKill && myBoxCollider.IsTouching(collision.GetComponent<CapsuleCollider2D>()))
        {

            playerHealthScript.Die();

        }

        else if (collision.CompareTag("MainTilemap"))
        {

            isKill = false;
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

        }

        else if (collision.CompareTag("Enemy"))
        {
            enemyManagerScript = collision.GetComponent<EnemyManager>();
            enemyManagerScript.ChangeEnemyHealth(100);
            isKill = false;

        }

        else
        {
            isKill = false;

        }

    }

    void RemainingDistance()
    {

        float distance = Mathf.Abs(gameObject.transform.position.x - player.transform.position.x);
        float distanceY = (gameObject.transform.position.y - player.transform.position.y);

        if (distance <= remainingDistance && distanceY > 0 && distanceY < 4)
        {

            Invoke(nameof(WaitForFall), waitingTime);

        }


    }

    void WaitForFall()
    {

        myRigidbody2d.gravityScale = 1;

    }

    void FindObject()
    {

        enemyManagerScript = FindObjectOfType<EnemyManager>();
        playerMoveScript = FindObjectOfType<PlayerMove>();
        playerHealthScript = FindObjectOfType<PlayerHealth>();
        player = GameObject.FindWithTag("Player");

    }
}
