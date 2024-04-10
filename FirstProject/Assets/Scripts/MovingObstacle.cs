using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{

    public float sallanmaGenisligi;
    public float sallanmaHizi;
    EdgeCollider2D myEdgeCollider;
    private float baslangicRotasyon;
    private Rigidbody2D rb;
    PlayerHealth playerHealthScript;

    void Start()
    {

        FindObject();
        baslangicRotasyon = transform.rotation.eulerAngles.z;

    }

    void Update()
    {
        float sallanmaAci = baslangicRotasyon + sallanmaGenisligi * Mathf.Sin(Time.time * sallanmaHizi);
        Quaternion yeniRotasyon = Quaternion.Euler(0f, 0f, sallanmaAci);
        rb.MoveRotation(yeniRotasyon);

        if (myEdgeCollider.IsTouchingLayers(LayerMask.GetMask("Player")) && playerHealthScript.isAlive)
        {

            playerHealthScript.Die();

        }
    }

    void FindObject()
    {

        myEdgeCollider = GetComponent<EdgeCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        playerHealthScript = FindObjectOfType<PlayerHealth>();

    }
}
