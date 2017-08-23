using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour
{
    public Transform cart;
    public float best_time = 0;

    private bool first_run = true;
    // Use this for initialization
    void Start()
    {
        Time.timeScale = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (first_run)
        {
            Destroy(GameObject.FindGameObjectWithTag("Player"));
            first_run = false;
        }

        GameObject[] carts = GameObject.FindGameObjectsWithTag("Player");
        if (carts.Length == 0)
        {
            Debug.Log("------------------------------------------------");
            //Debug.Log("Spawning a brand new Cart!");
            Instantiate(cart, new Vector3(-6.129f, .282f, -12.91f), cart.transform.rotation);
        }
    }
}
