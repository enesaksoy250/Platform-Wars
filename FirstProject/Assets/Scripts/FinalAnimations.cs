using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalAnimations : MonoBehaviour
{

    [SerializeField] GameObject[] doors;


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {


            foreach (GameObject door in doors)
            {

                door.GetComponent<CloseTheDoor>().CloseDoor();

            }


        }


    }


}
