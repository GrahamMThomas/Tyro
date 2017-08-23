using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public CarMovement car;

    public UnityEngine.UI.Text action_indicator;
    public UnityEngine.UI.Text speed_indicator;
    public UnityEngine.UI.Text left_sensor_indicator;
    public UnityEngine.UI.Text right_sensor_indicator;
    public UnityEngine.UI.Text front_sensor_indicator;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        car = GameObject.FindGameObjectWithTag("Player").GetComponent<CarMovement>();
        action_indicator.text = "Current Action: " + car.action;
        speed_indicator.text = "Speed: " + string.Format("{0:N1}", car.speed);
        left_sensor_indicator.text = "Left Sensor Distance: " + string.Format("{0:N1}", car.left_distance);
        right_sensor_indicator.text = "Right Sensor Distance: " + string.Format("{0:N1}", car.right_distance);
        front_sensor_indicator.text = "Front Sensor Distance: " + string.Format("{0:N1}", car.front_distance);
    }
}
