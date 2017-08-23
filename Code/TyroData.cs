using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[System.Serializable]
public class TData
{
    public float best_time = 1;
    public float best_score = 1000; // Lower is better
    public float max_left_sensor = 10;
    public float max_front_sensor = 10;
    public float max_right_sensor = 10;

    /*
    public double[][] input = {              
                // left, front, right, \
    new double[] { 1.0, 1.0, 1.0},
 
    };
    */

    public List<double[]> input = new List<double[]>();
    //True is left and false is right
    //Actions
    // 0 => Turn Left
    // 1 => Do Nothing
    // 2 => Turn Right
    public List<double> output = new List<double>();


    public TData()
    {

        input.Add(new double[] { 2, 3, 2 });
        /*
        input.Add(new double[] { 2, 4, 2 });
        input.Add(new double[] { 5, 3, 1 });
        input.Add(new double[] { 3, 2, 1 });
        input.Add(new double[] { 2, 1, 1 });
        input.Add(new double[] { 4, 1, 1 });
        input.Add(new double[] { 1.5, 2, 0.5 });
        input.Add(new double[] { 0.5, 12, 1.5 });
        input.Add(new double[] { 1, 1, 4 });
        input.Add(new double[] { 1, 1, 2 });
        input.Add(new double[] { 1, 3, 5 });
        input.Add(new double[] { 1, 2, 3 });
        */
        output.Add(1);
        /*
        output.Add(1);
        output.Add(0);
        output.Add(0);
        output.Add(0);
        output.Add(0);
        output.Add(0);
        output.Add(2);
        output.Add(2);
        output.Add(2);
        output.Add(2);
        output.Add(2);
        */
    }
}

public class TyroData : MonoBehaviour
{
    public TData current_tdata;

    void Start()
    {
        Load();
        //Debug.Log("Best Time: " + current_tdata.best_time + " - Left: " + current_tdata.max_left_sensor + " - Front: " + current_tdata.max_front_sensor + " - Right: " + current_tdata.max_right_sensor);
        //PrintData();
        if (Time.time > 10)
        {
            Mutate();
        }
        GetComponent<Visualizer>().Visualize(current_tdata.input.ToArray(), current_tdata.output.ToArray());
    }

    public void Mutate()
    {
        current_tdata.input.Add(new double[] { Random.Range(0, current_tdata.max_left_sensor),
                                               Random.Range(0, current_tdata.max_front_sensor),
                                               Random.Range(0, current_tdata.max_right_sensor) });
        int rand_num = Random.Range(0, 2);
        if (rand_num == 1)
        {
            current_tdata.output.Add(2);
        }
        else
        {
            current_tdata.output.Add(0);
        }

        if (current_tdata.input.Count > 3)
        {
            if (Random.Range(0, 20) <= current_tdata.input.Count)
            {
                int index_to_remove = Random.Range(0, current_tdata.input.Count);
                current_tdata.input.RemoveAt(index_to_remove);
                current_tdata.output.RemoveAt(index_to_remove);
            }
        }
        //current_tdata.best_time -= .1f;
    }

    void PrintData()
    {
        int rowLength = current_tdata.input.Count;
        int colLength = current_tdata.input[0].GetLength(0);

        //Debug.Log(current_tdata.input.Count + " " + current_tdata.output.Count);

        for (int i = 0; i < rowLength; i++)
        {
            string temp = "";
            for (int j = 0; j < colLength; j++)
            {
                temp += string.Format("{0} ", current_tdata.input[i][j]) + " ";
            }
            Debug.Log(temp + "\n" + current_tdata.output[i].ToString());
        }
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        //Debug.Log("Saving To: " + Application.persistentDataPath + "/TYROdata.gd");
        FileStream file = File.Create(Application.persistentDataPath + "/TYROdata.gd");
        bf.Serialize(file, current_tdata);
        file.Close();
    }
    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/TYROdata.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            //Debug.Log("Loading From: " + Application.persistentDataPath + "/TYROdata.gd");
            FileStream file = File.Open(Application.persistentDataPath + "/TYROdata.gd", FileMode.Open);
            current_tdata = (TData)bf.Deserialize(file);
            file.Close();
        }
    }
    void OnApplicationQuit()
    {
        Debug.Log("Application ending after " + Time.time + " seconds");
    }
}
