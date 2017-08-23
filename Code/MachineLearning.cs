
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Accord.Statistics.Models.Regression.Linear;

public class MachineLearning : MonoBehaviour
{
    string last_action = "";
    int total_times_scored = 0;
    MultipleLinearRegression regression;
    OrdinaryLeastSquares learner;
    CarMovement car;
    TyroData data;

    // Use this for initialization
    void Start()
    {
        learner = new OrdinaryLeastSquares()
        {
            UseIntercept = true
        };

        data = GetComponent<TyroData>();
        car = GetComponent<CarMovement>();

        // Now, we can use the learner to finally estimate our model:
        if (Time.time > 10)
        {
            data.Mutate();
        }
        regression = learner.Learn(data.current_tdata.input.ToArray(), data.current_tdata.output.ToArray());

    }

    // Update is called once per frame
    void Update()
    {

        float nothing_threshold = .75f;
        SetMaxes();
        double[][] input = { new double[] { car.left_distance, car.front_distance, car.right_distance } };
        double[] actions;
        try
        {
            actions = regression.Transform(input);
        }
        catch
        {
            actions = new double[] { 0 };
        }
        double action = actions[0];
        if (action <= 0 + nothing_threshold)
        {
            car.action = "Turn Left";
        }
        else if (action >= 2 - nothing_threshold)
        {
            car.action = "Turn Right";
        }
        else
        {
            car.action = "Nothing";
        }
        if (car.action != last_action)
        {
            //Debug.Log("Action: " + action + " - " + car.action);
            last_action = car.action;
        }
    }

    void SetMaxes()
    {
        if (car.left_distance > data.current_tdata.max_left_sensor)
        {
            data.current_tdata.max_left_sensor = car.left_distance;
        }
        if (car.front_distance > data.current_tdata.max_front_sensor)
        {
            data.current_tdata.max_front_sensor = car.front_distance;
        }
        if (car.right_distance > data.current_tdata.max_right_sensor)
        {
            data.current_tdata.max_right_sensor = car.right_distance;
        }
    }


}
