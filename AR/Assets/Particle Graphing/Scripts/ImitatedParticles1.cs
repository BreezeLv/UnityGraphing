using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImitatedParticles1 : MonoBehaviour
{
    public enum FunctionOption : int
    {
        Linear = 0,
        Exponential,
        Parabola,
        Sine
    }
    public FunctionOption function;
    public GameObject point;
    [Range(10, 300)]
    public int currentResolution;
    private int resolution;
    private GameObject[] points;
    // Use this for initialization
    void Start()
    {
        CreatePoints();
        currentResolution = 200;
    }
    private void CreatePoints()
    {
        //if (currentResolution < 10 || currentResolution > 100)
        //{
        //	Debug.LogWarning("Grapher resolution out of bounds, resetting to minimum.", this); //this is only for the highlighted effect when clicking on warning messages
        //	currentResolution = 10;
        //}
        resolution = currentResolution;
        points = new GameObject[resolution];
        float increment = 2f / (resolution - 1);
        for (int i = 0; i < resolution; i++)
        {
            points[i] = Instantiate(point);
            float x = (float)(i * increment);
            points[i].transform.position = new Vector3(x - 1, 0f, 0f);
            points[i].GetComponent<MeshRenderer>().material.color = new Color(x, 0f, 0f);
            //points[i].startcolor = new Color32((byte)x, 0, 0, 0);
            //points[i].startSize = 1f;
            Debug.Log(i);
        }
        Debug.Log("Create Points executed");
    }

    private delegate float FunctionDelegate(float x);
    private static FunctionDelegate[] functionDelegates = {
        Linear,
        Exponential,
        Parabola,
        Sine
    };




    // Update is called once per frame
    void Update()
    {
        
        if (currentResolution != resolution || points == null)
        {
            foreach (var index in points)
            {
                DestroyObject(index);
            }
            CreatePoints();
        }

        FunctionDelegate f = functionDelegates[(int)function];

        for (int i = 0; i < currentResolution; i++)
        {
            Vector3 p = points[i].transform.position;
            p.y = f(p.x);
            points[i].transform.position = p;
            points[i].GetComponent<MeshRenderer>().material.color = new Color(points[i].transform.position.x, points[i].transform.position.y, 0f);
            //points[i].GetComponent<MeshRenderer>().materials[0].mainTexture
        }
    }
    private static float Linear(float x)
    {
        return x;
    }
    private static float Linear(float x, float coefficeint)
    {
        return coefficeint * x;
    }
    private static float Linear(float x, float coefficeint, float intercept)
    {
        return coefficeint * x + intercept;
    }
    private static float Exponential(float x)
    {
        return Mathf.Exp(x);
    }

    private static float Parabola(float x)
    {
        return x * x;
    }

    private static float Sine(float x)
    {
        return 2f * Mathf.Sin(3f * x + Time.timeSinceLevelLoad);
    }
}
