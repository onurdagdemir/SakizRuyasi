using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gumball : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player" && transform.localScale.x <= 0.9f)
        {
            GumballManager.Instance.AddGumball(1);
            Destroy(gameObject);
        }
    }
}
