using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThrowSkull : MonoBehaviour
{

    [SerializeField] GameObject skull;
    bool isCreate = true;
   
    
    void Update()
    {

        
        if (!gameObject.GetComponent<EnemyManager>().isAlive&&isCreate)
        {
            isCreate = false;
            skull.SetActive(true);            
            skull.GetComponent<Rigidbody2D>().velocity=new Vector2(0,5);
            

        }

    }

    
}
