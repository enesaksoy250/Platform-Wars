using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBossManager : MonoBehaviour
{


    [SerializeField] float enemyHealth;
    [SerializeField] float enemyPower;
    [SerializeField] float enemySpeed;
    [SerializeField] float topSpeed;
    [SerializeField] float attackAnimationDuration;
    [SerializeField] float attackAnimation2Duration;
    [SerializeField] GameObject door;
    Animator enemyAnimator;
    Rigidbody2D enemyRigidbody;
    GameObject player;
    CapsuleCollider2D enemyCapsuleCollider;
    BoxCollider2D enemyBoxCollider;
    PlayerHealth playerHealth;
    CircleCollider2D enemyCircleCollider;
    ShakeCamera shakeCamera;
    public float moveDifferenceX;
    public float differenceY;
    public bool isMove;
    public bool isAlive;
    bool isLoop;
    int i;
    bool Attack1;
    bool Attack2;

    void Start()
    {


        FindObjects();
        Attack1 = false;
        Attack2 = false;
        i = 1;
        isAlive = true;
        isLoop = true;
        isMove = true;


    }




    void Update()
    {



        if (isAlive)
        {

            MoveEnemy();
            EnemyFlipSprite();
            EnemyTouchControl();

        }


    }



    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.CompareTag("Player") && isLoop)
        {

            if (enemyCircleCollider.IsTouching(player.GetComponent<CapsuleCollider2D>()) && playerHealth.isAlive && isAlive)
            {



                if (i % 3 != 0)
                {

                    Attack1 = true;
                    enemyAnimator.SetBool("Attack1", true);
                    StartCoroutine(nameof(WaitForLoop));
                    Invoke(nameof(ChangePlayerHealth), attackAnimationDuration);


                }

                else
                {

                    Attack2 = true;
                    enemyAnimator.SetBool("Attack2", true);
                    StartCoroutine(nameof(WaitForLoop2));
                    Invoke(nameof(ChangePlayerHealth2), attackAnimation2Duration);
                    Invoke(nameof(AddForce), attackAnimation2Duration);

                }

                i++;


            }

            if (!playerHealth.isAlive)
            {

                enemyAnimator.SetBool("Attack1", false);
                isMove = false;

            }

        }



    }

    void AddForce()
    {

        Vector2 itmeYonu = (player.transform.position - transform.position).normalized;
        Vector2 itmeKuvveti = itmeYonu * 100000;
        player.GetComponent<Rigidbody2D>().AddForce(itmeKuvveti, ForceMode2D.Impulse);


    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {

            if (!enemyCircleCollider.IsTouching(player.GetComponent<CapsuleCollider2D>()))
            {



                if (Attack1)
                {

                    enemyAnimator.SetBool("Attack1", false);
                    CancelInvoke(nameof(ChangePlayerHealth));

                }

                else
                {

                    enemyAnimator.SetBool("Attack2", false);
                    CancelInvoke(nameof(ChangePlayerHealth2));

                }

            }



        }



    }

    public float GetEnemyHealth()
    {

        return enemyHealth;
    }

    void EnemyFlipSprite()
    {


        float differenceX = Mathf.Abs(player.transform.position.x - transform.position.x);
        bool playerHasHorizontalSpeed = Mathf.Abs(enemyRigidbody.velocity.x) > Mathf.Epsilon;
        enemyAnimator.SetBool("Run", playerHasHorizontalSpeed);
        isMove = differenceX < moveDifferenceX;

        AnimatorStateInfo stateInfo = enemyAnimator.GetCurrentAnimatorStateInfo(0);
        string mevcutAnimasyonAdi = stateInfo.IsName("Base Layer.Idle") ? "Idle" : stateInfo.fullPathHash.ToString();



        if (mevcutAnimasyonAdi == 281010773.ToString())
        {

            shakeCamera.TriggerShakeForBoss(1, 1);

        }

        else if (mevcutAnimasyonAdi == 1543576472.ToString())
        {

            shakeCamera.StopShake();

        }

        if (Attack1 || Attack2 || playerHealth.GetPlayerHealth() <= 0)
        {

            shakeCamera.StopShake();

        }

        if (playerHasHorizontalSpeed && differenceX > 1)
        {

            transform.localScale = new Vector3(Mathf.Sign(enemyRigidbody.velocity.x), 1f, 1f);

        }

        if (differenceX < 3.2)
        {

            enemyAnimator.SetBool("Run", false);
            enemyRigidbody.velocity = Vector3.zero;

        }



    }

    void MoveEnemy()
    {

        if (isMove)
        {

            enemyRigidbody.velocity = new Vector2((player.transform.position.x - transform.position.x) * enemySpeed, enemyRigidbody.velocity.y);

            if (enemyRigidbody.velocity.x < -topSpeed)
            {
                enemyRigidbody.velocity = new Vector2(-topSpeed, 0);
            }

            if (enemyRigidbody.velocity.x > topSpeed)
            {
                enemyRigidbody.velocity = new Vector2(topSpeed, 0);
            }

        }

    }

    void ChangePlayerHealth()
    {

        if (enemyHealth > 0)
        {

            playerHealth.ChangeHealth(enemyPower);
            shakeCamera.TriggerShake(.5f, 1, 1);

        }

    }

    void ChangePlayerHealth2()
    {
        if (enemyHealth > 0)
        {

            playerHealth.ChangeHealth(enemyPower / 4);
            shakeCamera.TriggerShake(.5f, 1, 1);

        }



    }

    public void ChangeEnemyHealth(float power)
    {


        enemyHealth -= power;

        if (enemyHealth <= 0)
        {

            EnemyDie();

        }

    }

    void EnemyDie()
    {


        door.GetComponent<OpenTheDoor>().OpenDoor();
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
        Attack1 = false;

    }

    IEnumerator WaitForLoop2()
    {

        isLoop = false;

        yield return new WaitForSeconds(attackAnimation2Duration);

        isLoop = true;
        Attack2 = false;

    }

    void FindObjects()
    {

        playerHealth = FindObjectOfType<PlayerHealth>();
        enemyAnimator = GetComponent<Animator>();
        enemyRigidbody = GetComponent<Rigidbody2D>();
        enemyCircleCollider = GetComponent<CircleCollider2D>();
        enemyCapsuleCollider = GetComponent<CapsuleCollider2D>();
        player = GameObject.FindWithTag("Player");
        shakeCamera = GameObject.FindWithTag("ShakeCam").GetComponent<ShakeCamera>();



    }
}
