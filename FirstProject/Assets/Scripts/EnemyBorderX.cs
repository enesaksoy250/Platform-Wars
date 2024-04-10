using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBorderX : MonoBehaviour
{

    [SerializeField] float MaxBorderX;
    [SerializeField] float MinBorderX;
    EnemyManager enemyManager;

    void Start()
    {

        enemyManager = FindObjectOfType<EnemyManager>();

    }


    void Update()
    {

        if (transform.position.x <= MinBorderX)
        {

            enemyManager.isMove = false;
            transform.position = new Vector2(MinBorderX + 0.01f, transform.position.y);

        }

        if (transform.position.x >= MaxBorderX)
        {

            enemyManager.isMove = false;
            transform.position = new Vector2(MaxBorderX - 0.01f, transform.position.y);

        }

    }
}
