using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visualizer : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Visualize(double[][] input, double[] output)
    {
        double xmax = 10;
        double ymax = 10;
        double zmax = 10;

        // Uncomment for Dynamic Scale
        /*
        for (int i = 0; i < input.Length; i++)
        {
            if (input[i][0] > xmax)
            {
                xmax = input[i][0];
            }
            if (input[i][2] > ymax)
            {
                ymax = input[i][2];
            }
            if (input[i][1] > zmax)
            {
                zmax = input[i][1];
            }
        }
        */
        for (int i = 0; i < output.Length; i++)
        {
            GameObject point = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            point.tag = "Datapoint";
            point.transform.position = new Vector3(((float)input[i][0] * 10 / (float)xmax) - 30,
                                                    (float)input[i][2] * 10 / (float)ymax,
                                                    (float)input[i][1] * 10 / (float)zmax);
            if (output[i] == 0)
            {
                point.GetComponent<Renderer>().material.color = Color.red;
            }
            else if (output[i] == 1)
            {
                point.GetComponent<Renderer>().material.color = Color.white;
            }
            else if (output[i] == 2)
            {
                point.GetComponent<Renderer>().material.color = Color.blue;
            }
            else
            {
                point.GetComponent<Renderer>().material.color = Color.yellow;
            }
        }
    }
}
