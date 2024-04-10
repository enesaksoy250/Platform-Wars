using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockSettings : MonoBehaviour
{

    [SerializeField] GameObject door;
    Animator animator;
    OpenTheDoor openDoor;

    void Start()
    {
        openDoor = door.GetComponent<OpenTheDoor>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.CompareTag("Player"))
        {

            animator.Play("LeverAnimation");
            openDoor.OpenDoor();

        }

    }
}
