using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Tilemaps;
using UnityEngine;

public class EnemyIdleMove : MonoBehaviour
{

    EnemyManager enemyManager;
    public float startPointX;
    public float endPointX;
    public float speed = 1f;
    public float waitingTime;
    private float targetPositionX;
    private Rigidbody2D rb;
    private bool isLoop = true;
    float scaleX;

    void Start()
    {

        scaleX = transform.localScale.x;
        enemyManager = GetComponent<EnemyManager>();
        rb = GetComponent<Rigidbody2D>();

        if (transform.position.x == endPointX)
        {

            targetPositionX = startPointX;

        }

        else if (transform.position.x == startPointX)
        {

            targetPositionX = endPointX;

        }

    }


    void Update()
    {

        if (!enemyManager.isMove && isLoop)
        {

            rb.velocity = new Vector2(Mathf.Sign(targetPositionX - transform.position.x) * speed, 0);



            if (transform.position.x > endPointX)
            {

                isLoop = false;
                targetPositionX = startPointX;
                Invoke(nameof(WaitForLoop), waitingTime);

            }

            if (transform.position.x < startPointX)
            {

                isLoop = false;
                targetPositionX = endPointX;
                Invoke(nameof(WaitForLoop), waitingTime);

            }

        }

        if (enemyManager.isMove)
        {

            isLoop = false;

        }

    }

    void WaitForLoop()
    {

        isLoop = true;


    }



}
