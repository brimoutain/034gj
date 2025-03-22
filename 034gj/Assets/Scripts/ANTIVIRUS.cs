using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ANTIVIRUS : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Patch")
        {
            Destroy(collision.gameObject);
        }
    }

    private void OnMouseDown()
    {
        
    }
}
