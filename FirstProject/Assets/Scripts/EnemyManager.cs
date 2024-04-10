using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyManager : MonoBehaviour
{

    [SerializeField] float enemyHealth;
    [SerializeField] float enemyPower;
    [SerializeField] float enemySpeed;
    [SerializeField] float topSpeed;
    [SerializeField] float attackAnimationDuration;
    [SerializeField] GameObject spear;
    CapsuleCollider2D enemyCapsuleCollider;
    BoxCollider2D enemyBoxCollider;
    Rigidbody2D enemyRigidbody;
    PlayerHealth playerHealth;
    Animator enemyAnimator;
    public float differenceX;
    public float differenceY;
    private GameObject player;
    public bool isMove;
    public bool isAlive = true;
    bool isLoop = true;
    [SerializeField]float difference;


    void Start()
    {

        FindObjectAndGetComponent();
        isMove = false;

    }


    void Update()
    {

        if (gameObject.name == "SkeletonArcher" && isAlive)
        {

            float differenceX = Mathf.Abs(player.gameObject.transform.position.x - gameObject.transform.position.x);
            float differenceY = Mathf.Abs(player.gameObject.transform.position.y - gameObject.transform.position.y);

            if (differenceX <= 4 && differenceY <= .75f && isLoop)
            {

                if (playerHealth.isAlive)
                {

                    enemyAnimator.SetBool("Attack", true);
                    StartCoroutine(nameof(WaitForLoop));
                    spear.GetComponent<SpearSettings>();


                }

            }

            if (differenceX > 4)
            {


                enemyAnimator.SetBool("Attack", false);


            }


            if (Mathf.Abs(player.transform.position.x - gameObject.transform.position.x) < 4f)
            {

                isMove = false;

            }

            float differenceXFlip = Mathf.Sign(gameObject.transform.position.x - player.transform.position.x);
            transform.localScale = new Vector3(-differenceXFlip, transform.localScale.y, transform.localScale.z);

        }

        if (isAlive)
        {

            MoveEnemy();
            EnemyFlipSprite();
            AnimationControl();
            EnemyTouchControl();

        }


    }




    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.CompareTag("Player") && isLoop)
        {


            if (enemyBoxCollider.IsTouching(player.GetComponent<CapsuleCollider2D>()) && playerHealth.isAlive)
            {

                enemyAnimator.SetBool("Attack", true);
                StartCoroutine(nameof(WaitForLoop));
                Invoke(nameof(ChangePlayerHealth), attackAnimationDuration);


            }


            if (!playerHealth.isAlive)
            {

                enemyAnimator.SetBool("Attack", false);
                isMove = false;

            }

        }


    }
    public void CreateSpear()
    {
        Vector3 position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Instantiate(spear, position, Quaternion.identity);
        spear.GetComponent<SpearSettings>();

    }

    void ChangePlayerHealth()
    {

        playerHealth.ChangeHealth(enemyPower);

    }

    IEnumerator WaitForLoop()
    {

        isLoop = false;

        yield return new WaitForSeconds(attackAnimationDuration);

        isLoop = true;


    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {

            enemyAnimator.SetBool("Attack", false);
            CancelInvoke(nameof(ChangePlayerHealth));

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


        enemyAnimator.SetTrigger("Die");
        isAlive = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        enemyRigidbody.bodyType = RigidbodyType2D.Static;


    }

    void EnemyFlipSprite()
    {



        float differenceX = Mathf.Abs(player.transform.position.x - transform.position.x);
        bool playerHasHorizontalSpeed = Mathf.Abs(enemyRigidbody.velocity.x) > Mathf.Epsilon;
        enemyAnimator.SetBool("Run", playerHasHorizontalSpeed);



        if (playerHasHorizontalSpeed /*&& differenceX > 1*/)
        {

            transform.localScale = new Vector3(Mathf.Sign(enemyRigidbody.velocity.x), 1f, 1f);

        }

        if (differenceX < difference)
        {

            enemyAnimator.SetBool("Run", false);
            enemyRigidbody.velocity = Vector3.zero;

        }


    }

    void MoveEnemy()
    {



        if (isMove && enemyCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Main")))
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

    void AnimationControl()
    {

        if (enemyCapsuleCollider.IsTouching(player.GetComponent<BoxCollider2D>()))
        {

            enemyAnimator.SetBool("Attack", false);

        }


    }

    void EnemyTouchControl()
    {

        if (enemyCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Hazard")))
        {

            enemyAnimator.SetTrigger("Die");
            GetComponent<EnemyManager>().enabled = false;
        }
    }

    void FindObjectAndGetComponent()
    {


        enemyBoxCollider = GetComponent<BoxCollider2D>();
        enemyCapsuleCollider = GetComponent<CapsuleCollider2D>();
        enemyAnimator = GetComponent<Animator>();
        enemyRigidbody = GetComponent<Rigidbody2D>();
        playerHealth = FindObjectOfType<PlayerHealth>();
        player = GameObject.FindWithTag("Player");


    }



}
