using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{

    const float TOPSPEED = 1f;
    const float ACCELCONSTANT = 0.05f;
    float turnspeed = 60f;
    int total_times_scored = 0;
    float total_score = 0;

    public string action;
    public float start_time = 0.0f;
    public float speed = 1f;
    public float left_distance = 3f;
    public float right_distance = 3f;
    public float front_distance = 3f;
    TyroData data;

    void Awake()
    {
        start_time = Time.time;
    }

    // Use this for initialization
    void Start()
    {
        action = "Nothing";
        data = GetComponent<TyroData>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Wall")
        {
            Explode();
        }
    }

    void Update()
    {
        UpdateSensors();
        CalculateScore();
    }

    void CalculateScore()
    {
            float score = Mathf.Pow(left_distance, 2f) + Mathf.Pow(right_distance, 2f);
            score = score / (left_distance + right_distance);
            total_score += score;
            total_times_scored++;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        transform.position += transform.forward * Time.deltaTime * speed;

        if (action == "Nothing")
        {
            //Derp
        }
        else if (action == "Throttle")
        {
            Throttle();
        }
        else if (action == "Brake")
        {
            Brake();
        }
        else if (action == "Turn Left")
        {
            TurnLeft();
        }
        else if (action == "Turn Right")
        {
            TurnRight();
        }
    }

    void UpdateSensors()
    {
        // Left Sensor
        Debug.DrawRay(transform.position, (transform.forward + (-transform.right)).normalized, Color.green);
        RaycastHit left_hit;
        Ray left_ray = new Ray(transform.position, (transform.forward + (-transform.right)).normalized);
        if (Physics.Raycast(left_ray, out left_hit))
        {
            left_distance = left_hit.distance;
        }

        // Right Sensor
        Debug.DrawRay(transform.position, (transform.forward + (transform.right)).normalized, Color.green);
        RaycastHit right_hit;
        Ray right_ray = new Ray(transform.position, (transform.forward + (transform.right)).normalized);
        if (Physics.Raycast(right_ray, out right_hit))
        {
            right_distance = right_hit.distance;
        }

        // Front Sensor
        Debug.DrawRay(transform.position, transform.forward, Color.green);
        RaycastHit forward_hit;
        Ray forward_ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(forward_ray, out forward_hit))
        {
            front_distance = forward_hit.distance;
        }
    }

    // Update is called once per frame
    void Throttle()
    {
        if (speed <= TOPSPEED - ACCELCONSTANT)
        {
            speed += ACCELCONSTANT;
        }
        else
        {
            speed = TOPSPEED;
        }
    }

    void Brake()
    {
        if (speed >= 0 + ACCELCONSTANT)
        {
            speed -= ACCELCONSTANT;
        }
        else
        {
            speed = 0;
        }
    }

    void TurnLeft()
    {
        transform.Rotate(0, turnspeed * Time.deltaTime * -1, 0);
    }

    void TurnRight()
    {
        transform.Rotate(0, turnspeed * Time.deltaTime, 0);
    }

    void Explode()
    {
        Driver best_time_obj = GameObject.Find("Floor").GetComponent<Driver>();
        TyroData data = GetComponent<TyroData>();

        float average_score = total_score / total_times_scored;

        if (average_score < data.current_tdata.best_score)//(data.current_tdata.best_time - (data.current_tdata.best_time * .3f) < Time.time - start_time)
        {
            //Debug.Log("New Best Time! \nOld Time: " + data.current_tdata.best_time + "   -   New Time: " + (Time.time - start_time));
            Debug.Log("######### New best score: " + average_score + " - Old: " + data.current_tdata.best_score);
            data.current_tdata.best_time = (Time.time - start_time);
            data.current_tdata.best_score = average_score;
            // Destroy the visualization for that car so they don't stack

            data.Save();
        }
        else
        {
            Debug.Log("Score: " + average_score);
            //current_tdata.current_tdata.best_time = (current_tdata.current_tdata.best_time * 10 + Time.time - start_time) / 11 ;
        }

        foreach (GameObject data_point in GameObject.FindGameObjectsWithTag("Datapoint"))
        {
            Destroy(data_point);
        }
        Destroy(this.gameObject);
    }
}
