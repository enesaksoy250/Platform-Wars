using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class SkeletonMageManager : MonoBehaviour
{

    [SerializeField] float enemyHealth;
    [SerializeField] float enemyPower;
    [SerializeField] float enemySpeed;
    [SerializeField] float topSpeed;
    [SerializeField] float attackAnimationDuration;
    [SerializeField] float differencePlayerEnemy;
    [SerializeField] GameObject[] teleportPoints;
    [SerializeField] GameObject door;
    GameObject player;
    Animator enemyAnimator;
    Rigidbody2D enemyRigidbody;
    CapsuleCollider2D enemyCapsuleCollider;
    PlayerHealth playerHealth;
    OpenTheDoor openDoor;
    ShakeCamera shakeCamera;
    public bool isMove = true;
    public bool isAlive = true;
    public bool teleport;
    bool isLoop;
    float differenceX;
    int a = 0;


    private void Start()
    {

        FindObjects();
        teleport = false;
        isLoop = true;


    }



    private void Update()
    {

        if (isAlive && !teleport)
        {

            AttackEnemy();
            EnemyFlipSprite();
            EnemyTouchControl();


        }


    }


    void EnemyFlipSprite()
    {


        bool playerHasHorizontalSpeed = Mathf.Abs(enemyRigidbody.velocity.x) > Mathf.Epsilon;
        enemyAnimator.SetBool("Run", playerHasHorizontalSpeed);

        float differenceXFlip = Mathf.Sign(gameObject.transform.position.x - player.transform.position.x);
        transform.localScale = new Vector3(-differenceXFlip, transform.localScale.y, transform.localScale.z);

    }



    void AttackEnemy()
    {

        if (playerHealth.isAlive)
        {

            differenceX = transform.position.x - player.transform.position.x;

            if (Math.Abs(differenceX) <= differencePlayerEnemy && isLoop)
            {


                StartCoroutine(nameof(WaitForLoop));
                Invoke(nameof(ChangeHealth), attackAnimationDuration - 0.2f);
                enemyAnimator.SetBool("Attack1", true);


            }

            else if (Math.Abs(differenceX) > differencePlayerEnemy)
            {

                CancelInvoke(nameof(ChangeHealth));
                enemyAnimator.SetBool("Attack1", false);

            }
        }

        else
        {

            enemyAnimator.SetBool("Attack1", false);

        }
    }

    public void ChangeEnemyHealth(float power)
    {



        if (!teleport)
        {

            enemyHealth -= power;
            print(enemyHealth);

        }


        if (enemyHealth <= 0)
        {

            EnemyDie();
            openDoor.OpenDoor();

        }


        teleport = true;

        bool isPlay = enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Atack1");


        if (enemyHealth > 0 && !isPlay)
        {

            enemyAnimator.SetBool("Teleport", true);
            enemyAnimator.SetBool("TeleportReverse", true);

        }


    }

    void EnemyDie()
    {


        enemyAnimator.SetTrigger("Die");
        isAlive = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        enemyRigidbody.bodyType = RigidbodyType2D.Static;


    }

    void EnemyTouchControl()
    {

        if (enemyCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Hazard")))
        {

            enemyAnimator.SetTrigger("Die");
            GetComponent<EnemyManager>().enabled = false;
        }
    }

    IEnumerator WaitForLoop()
    {


        isLoop = false;

        yield return new WaitForSeconds(attackAnimationDuration);

        isLoop = true;


    }

    void ChangeHealth()
    {

        playerHealth.ChangeHealth(enemyPower);
        shakeCamera.TriggerShake(.5f, 1f, 1f);

    }

    public void Teleport()
    {


        transform.position = teleportPoints[a].transform.position;
        a++;

        if (a == teleportPoints.Length)
        {

            a = 0;
        }

        teleport = false;

    }

    void FindObjects()
    {

        playerHealth = FindObjectOfType<PlayerHealth>();
        enemyCapsuleCollider = GetComponent<CapsuleCollider2D>();
        enemyRigidbody = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        enemyAnimator = GetComponent<Animator>();
        openDoor = door.GetComponent<OpenTheDoor>();
        shakeCamera = FindObjectOfType<ShakeCamera>();


    }


}