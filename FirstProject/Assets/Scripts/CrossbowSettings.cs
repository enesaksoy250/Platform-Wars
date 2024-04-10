using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowSettings : MonoBehaviour
{

    [SerializeField] float distance;
    [SerializeField] GameObject spear;
    [SerializeField] GameObject point;
    [SerializeField] float health;
    GameObject player;
    PlayerHealth playerHealth;
    public bool isAlive;
    float differenceX;
    float differenceY;
    bool isloop;



    void Start()
    {

        isAlive = true;
        isloop = true;
        player = GameObject.FindWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();

    }


    void Update()
    {

        if (isAlive && playerHealth.isAlive)
        {

            HealthControl();

            differenceX = gameObject.transform.position.x - player.transform.position.x;
            differenceY = gameObject.transform.position.y - player.transform.position.y;


            if (differenceX > 0 && differenceX < distance && isloop && differenceY < 1)
            {
                gameObject.GetComponent<Animator>().SetBool("Shoot", true);
                Invoke(nameof(CreateSpear), .23f);
                StartCoroutine(WaitForLoop());



            }

            if (differenceX > distance || differenceX < 0 || differenceY >= 1)
            {

                gameObject.GetComponent<Animator>().SetBool("Shoot", false);

            }
        }
    }

    void CreateSpear()
    {

        Instantiate(spear, point.transform.position, Quaternion.identity);
        spear.GetComponent<SpearForCrossbow>();

    }


    IEnumerator WaitForLoop()
    {
        isloop = false;
        yield return new WaitForSeconds(0.23f);
        isloop = true;
    }

    void HealthControl()
    {

        if (health <= 0)
        {

            GetComponent<Animator>().SetTrigger("Break");
            isAlive = false;
            GetComponent<BoxCollider2D>().enabled = false;

        }

    }

    public void DecreaseHealth(float health1)
    {

        print("can düþürülüyo");
        health -= health1;

    }

}
