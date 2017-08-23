using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform cart;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        cart = GameObject.FindGameObjectWithTag("Player").transform;
        if (cart != null)
        {
            this.gameObject.transform.position = new Vector3(cart.position.x, 4f, cart.position.z);
        }
    }
}
