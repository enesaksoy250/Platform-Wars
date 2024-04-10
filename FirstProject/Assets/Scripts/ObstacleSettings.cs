using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSettings : MonoBehaviour
{

    [SerializeField] float topBorderY;
    [SerializeField] float bottomBorderY;
    [SerializeField] float speed;
    [SerializeField] float waitingTime;
    [SerializeField] float randomMax;
    [SerializeField] bool  fromBottomToUp;
    [SerializeField] bool isRandom;
    PlayerHealth playerHealthScript;   
    [SerializeField]BoxCollider2D boxCollider2D;
    GameObject player;
    bool isloop;
  
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        isloop = true;
        playerHealthScript=FindObjectOfType<PlayerHealth>();
    }

    
    void Update()
    {

        if (boxCollider2D.IsTouching(player.GetComponent<CapsuleCollider2D>()) && player.GetComponent<PlayerHealth>().isAlive)
        {


            player.GetComponent<PlayerHealth>().Die();

        }


        if (playerHealthScript.isAlive && isloop)
        {

            transform.Translate(new Vector2(0, speed * Time.deltaTime));

            if (transform.position.y >= topBorderY)
            {

                isloop = false;
                transform.position = new Vector2(transform.position.x, topBorderY);

                if (!fromBottomToUp)
                {

                    if (isRandom)
                    {

                        float random = Random.Range(0, randomMax);
                        Invoke(nameof(WaitForLoop), random);
                        speed = -speed;
                    }

                    else
                    {

                        Invoke(nameof(WaitForLoop), waitingTime);
                        speed = -speed;

                    }
                }

                else
                {
                    isloop=true;
                    speed = -speed;
                }

            }
         
            if(transform.position.y <= bottomBorderY)
            {

                if (fromBottomToUp)
                {

                    transform.position = new Vector2(transform.position.x, bottomBorderY);
                    isloop = false;
                    if (isRandom)
                    {

                        float random = Random.Range(0, randomMax);
                        Invoke(nameof(WaitForLoop), random);
                        speed = -speed;
                    }

                    else
                    {

                        Invoke(nameof(WaitForLoop), waitingTime);
                        speed = -speed;

                    }

                }
              
                else
                {

                    transform.position = new Vector2(transform.position.x, bottomBorderY);
                    speed = -speed;

                }
            }

        }
    }

   
    void WaitForLoop()
    {

        isloop = true;

    }

}
