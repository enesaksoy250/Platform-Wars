using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{


    [SerializeField] float topBorderY;
    [SerializeField] float bottomBorderY;
    [SerializeField] float topBorderX;
    [SerializeField] float bottomBorderX;
    [SerializeField] float moveY;
    [SerializeField] float moveX;
    [SerializeField] string direction;

    void Update()
    {

        if (direction == "x")
        {

            transform.Translate(new Vector2(moveX, moveY * Time.deltaTime));

            if (transform.position.x < bottomBorderX || transform.position.x > topBorderX)
            {

                moveX = -moveX;

            }
        }

        if (direction == "y")
        {

            transform.Translate(new Vector2(moveX, moveY * Time.deltaTime));

            if (transform.position.y < bottomBorderY || transform.position.y > topBorderY)
            {

                moveY = -moveY;

            }

        }
    }
}
