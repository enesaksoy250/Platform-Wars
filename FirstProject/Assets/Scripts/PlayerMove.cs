using UnityEditor;
using System.Collections;
using UnityEngine;
using Cinemachine;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;
using UnityEngine.SceneManagement;


public class PlayerMove : MonoBehaviour
{

    [SerializeField] int moveSpeed;
    [SerializeField] float climbSpeed;
    [SerializeField] float verticalMove;
    [SerializeField] float playerPower;
    [SerializeField] float upSpeed;
    [SerializeField] float downSpeed;
    [SerializeField]ParticleSystem hitEffect;
    CircleCollider2D swordCircleCollider;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    BoxCollider2D myBoxCollider;
    CapsuleCollider2D myCapsuleCollider;
    GameObject enemy;
    PlayerHealth playerHealth;
    EnemyManager enemyManager;
    PlayerScore playerScore;
    EnemyManager[] enemyManagers;
    CinemachineVirtualCamera virtualCamera;
    CrossbowSettings crossbowSettingsScript;
    SkeletonMageManager skeletonMageManager;
    SkeletonBossManager skeletonBossManager;
    GameManager gameManager;
    CPGetPlayerInformation cpGetPlayerInfo;
    CheckPointManager checkPointManager;
    CoinManager playerCoin;
    public bool isJump;
    private float horizontalMove;
    float gravityScale;
    bool isAlive = true;
    bool isLoop = true;
    public bool isMove;
    public bool touching;
    bool first;
    public Vector2 startPosition;
    int index;


   
    void Start()
    {


        FindObjectAndGetComponent();
        CurrentBeginningControl();
        playerHealth.SetPlayerHealth(100);
        gravityScale = myRigidbody.gravityScale;
        touching = false;
        index = SceneManager.GetActiveScene().buildIndex;

    }


    void Update()
    {



        if (playerHealth.isAlive)
        {
            myRigidbody.velocity = new Vector2(horizontalMove * moveSpeed, myRigidbody.velocity.y);
            AnimationControl();
            JumpControl();
            ClimbLadder();
            FlipSprite();
            PlayerEnemyDistanceControl();
            ChangeCinemachineScreenX();
        }


        if (myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Hazard")) && playerHealth.isAlive)
        {

            playerHealth.Die();


        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Enemy"))
        {

            enemyManager = collision.gameObject.GetComponent<EnemyManager>();


        }

        else if (collision.CompareTag("SkeletonMage"))
        {

            skeletonMageManager = collision.gameObject.GetComponent<SkeletonMageManager>();

        }

        else if (collision.CompareTag("SkeletonBoss"))
        {

            skeletonBossManager = collision.gameObject.GetComponent<SkeletonBossManager>();
        }

        else if (collision.CompareTag("Crossbow"))
        {


            crossbowSettingsScript = collision.GetComponent<CrossbowSettings>();

        }

        if (collision.CompareTag("Star"))
        {

            if (myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Star")))
            {

                
                gameManager.PlayStarAudio();
                


                for (int i = 1; i < 4; i++)
                {

                    if (collision.gameObject == gameManager.starGameObjects[i - 1])
                    {


                        playerScore.EnterStar();
                        gameManager.UpdateStartText();
                      

                        if (!PlayerPrefs.HasKey("StarLevel" + index + i))
                        {


                            playerScore.EnterTotalStar();
                            PlayerPrefs.SetInt("StarLevel" + index + i, 1);

                        }


                        PlayerPrefs.SetInt("Star" + i, 1);                        
                        PlayerPrefs.Save();
                        print("Star" + i);
                        Destroy(collision.gameObject);
                    }

                }

            }

        }


    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.CompareTag("Enemy"))
        {

            if (swordCircleCollider.IsTouching(collision.GetComponent<CapsuleCollider2D>()))
            {

                touching = true;
                enemyManager = collision.GetComponent<EnemyManager>();

            }

            isAlive = collision.GetComponent<EnemyManager>().isAlive;

        }



        if (collision.CompareTag("SkeletonMage"))
        {

            touching = true;
            if (swordCircleCollider.IsTouching(collision.GetComponent<CapsuleCollider2D>()))
            {

                skeletonMageManager = collision.GetComponent<SkeletonMageManager>();

            }

            isAlive = collision.GetComponent<SkeletonMageManager>().isAlive;

        }


        if (collision.CompareTag("Crossbow"))
        {

            touching = true;
            print("crossbow ile çarpýþtým");
            crossbowSettingsScript = collision.GetComponent<CrossbowSettings>();

        }

        if (collision.CompareTag("SlowWay"))
        {

            moveSpeed = 1;
            verticalMove = 5;

        }

        if (collision.CompareTag("SkeletonBoss"))
        {


            if (swordCircleCollider.IsTouching(collision.GetComponent<CapsuleCollider2D>()))
            {
                touching = true;
                skeletonBossManager = collision.GetComponent<SkeletonBossManager>();

            }

            isAlive = collision.GetComponent<SkeletonBossManager>().isAlive;

        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        enemyManager = null;

        if (collision.CompareTag("Enemy"))
        {

            touching = false;
            CancelInvoke(nameof(EnemyTouchControl));

        }

        if (collision.CompareTag("SkeletonMage"))
        {

            touching = false;
            CancelInvoke(nameof(EnemyTouchControl));

        }


        if (collision.CompareTag("Crossbow"))
        {
            touching = false;
            CancelInvoke(nameof(CrossbowTouchControl));
        }

        if (collision.CompareTag("SlowWay"))
        {

            moveSpeed = 3;
            verticalMove = 10;

        }

        if (collision.CompareTag("SkeletonBoss"))
        {
            touching = false;
            CancelInvoke(nameof(EnemyTouchControl));

        }
    }

    public void Hit()
    {

        if (isLoop)
        {

            if (touching && myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Main")))
            {

                Invoke(nameof(PlaySwordEnemySound), .3f);

            }

            else if (myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Main")))
            {
                
                Invoke(nameof(PlaySwordSingleSound),.3f);

            }

            if (swordCircleCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")))
            {

                Invoke(nameof(PlayHitEffect),.333f);

            }

            myAnimator.SetTrigger("Attack");

            if (isAlive)
            {

                Invoke(nameof(EnemyTouchControl), .333f);
                Invoke(nameof(CrossbowTouchControl), .333f);
                StartCoroutine(WaitForLoop());

            }
            
        }

    }

    void PlaySwordEnemySound()
    {

        gameManager.PlaySwordEnemySound();

    }

    void PlaySwordSingleSound()
    {

        gameManager.PlaySwordSingleSound();

    }
    void PlayHitEffect()
    {

        hitEffect.Play();

    }
    void CurrentBeginningControl()
    {

        if (PlayerPrefs.HasKey("Current"))
        {

            transform.position = checkPointManager.GetLastCheckpointPos();
            PlayerPrefs.DeleteKey("Current");

        }

        else
        {


            transform.position = startPosition;


        }



    }
    public void Right()
    {

        horizontalMove = 2;

    }

    public void Left()
    {

        horizontalMove = -2;
    }

    public void Stop()
    {

        horizontalMove = 0;

    }

    public void Up()
    {

        if (myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {

            myRigidbody.velocity = new Vector2(0, upSpeed);

        }

    }

    public void Down()
    {

        if (myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {

            myRigidbody.velocity = new Vector2(0, -downSpeed);

        }

    }

    public void StopUpDown()
    {

        if (myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {

            myRigidbody.velocity = new Vector2(0, 0);

        }

    }

    public void Jump()
    {

        if (isJump)
        {

            myRigidbody.velocity += new Vector2(myRigidbody.velocity.x, verticalMove);
            myAnimator.SetTrigger("Jump");


        }



    }

    void EnemyTouchControl()
    {

        if (swordCircleCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {

            enemyManager.ChangeEnemyHealth(playerPower);

        }

        if (swordCircleCollider.IsTouchingLayers(LayerMask.GetMask("SkeletonMage")) && !skeletonMageManager.teleport)
        {

            skeletonMageManager.ChangeEnemyHealth(playerPower);

        }

        if (swordCircleCollider.IsTouchingLayers(LayerMask.GetMask("SkeletonBoss")))
        {

            skeletonBossManager.ChangeEnemyHealth(playerPower);
            print(skeletonBossManager.GetEnemyHealth());
        }


    }

    void CrossbowTouchControl()
    {

        if (swordCircleCollider.IsTouchingLayers(LayerMask.GetMask("Crossbow")))
        {

            print("crossbow");
            crossbowSettingsScript.DecreaseHealth(playerPower);

        }


    }

    void ChangeCinemachineScreenX()
    {


        if (transform.localScale.x > 0)
        {

            virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenX = 0.3f;

        }


        else

            virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenX = 0.7f;

    }
    void PlayerEnemyDistanceControl()
    {

        foreach (var enemy in enemyManagers)
        {

            float differenceX = Mathf.Abs(enemy.gameObject.transform.position.x - transform.position.x);
            float differenceY = Mathf.Abs(enemy.gameObject.transform.position.y - transform.position.y);


            if (differenceX < enemy.differenceX && differenceY < enemy.differenceY)
            {

                enemy.isMove = true;


            }

            else
            {

                enemy.isMove = false;

            }


        }


    }


    void JumpControl()
    {

        if (myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Main")))
        {

            isJump = true;


        }

        else
        {

            isJump = false;

        }


    }

    void AnimationControl()
    {

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Run", playerHasHorizontalSpeed);


    }

    void ClimbLadder()
    {

        if (myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {

            myRigidbody.gravityScale = 0;
            bool isClimb = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
            myAnimator.SetBool("StopClimb", false);
            myAnimator.SetBool("Climb", isClimb);


            if(myRigidbody.velocity.y > upSpeed)
            {

                myRigidbody.velocity = new Vector2(0, upSpeed);

            }

            if (myRigidbody.velocity.y == 0)
            {
                myAnimator.SetBool("StopClimb", true);

            }


        }

        else
        {

            myRigidbody.gravityScale = gravityScale;
            myAnimator.SetBool("Climb", false);
            myAnimator.SetBool("StopClimb", false);

        }

    }

    void FlipSprite()
    {

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
      
        
        if (playerHasHorizontalSpeed)
        {

            transform.localScale = new Vector3(Mathf.Sign(myRigidbody.velocity.x) * .7f, .7f, 1);
   

        }



    }


    IEnumerator WaitForLoop()
    {

        isLoop = false;

        yield return new WaitForSeconds(0.333f);

        isLoop = true;

    }

    void FindObjectAndGetComponent()
    {

        playerScore = FindObjectOfType<PlayerScore>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
        myBoxCollider = GetComponent<BoxCollider2D>();
        myAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        enemy = GameObject.FindWithTag("Enemy");
        playerHealth = FindObjectOfType<PlayerHealth>();
        enemyManager = FindObjectOfType<EnemyManager>();
        enemyManagers = FindObjectsOfType<EnemyManager>();
        swordCircleCollider = GetComponent<CircleCollider2D>();
        crossbowSettingsScript = FindObjectOfType<CrossbowSettings>();
        skeletonMageManager = FindObjectOfType<SkeletonMageManager>();
        skeletonBossManager = FindObjectOfType<SkeletonBossManager>();
        gameManager = FindObjectOfType<GameManager>();
        isMove = true;
        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        crossbowSettingsScript = FindObjectOfType<CrossbowSettings>();
        skeletonMageManager = FindObjectOfType<SkeletonMageManager>();
        skeletonBossManager = FindObjectOfType<SkeletonBossManager>();
        gameManager = FindObjectOfType<GameManager>();
        cpGetPlayerInfo = FindObjectOfType<CPGetPlayerInformation>();
        checkPointManager = FindObjectOfType<CheckPointManager>();
        playerCoin = FindObjectOfType<CoinManager>();

    }



}
