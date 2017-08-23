using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    CarMovement cart;

    // Use this for initialization
    void Start()
    {
        cart = GetComponent<CarMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            cart.action = "Throttle";
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            cart.action = "Brake";
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            cart.action = "Turn Left";
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            cart.action = "Turn Right";
        }
        if (!Input.GetKey(KeyCode.Space))
        {
            //dcart.action = "Nothing";
        }

    }
}
