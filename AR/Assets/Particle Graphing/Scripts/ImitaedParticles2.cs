using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;

public class ImitaedParticles2 : MonoBehaviour
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
    public float[] coeflist;
    private int resolution;
    private GameObject[] points;


    // Use this for initialization
    void Start()
    {
        CreatePoints();
        currentResolution = 20;
        coeflist = new float[10];
    }
    private void CreatePoints()
    {
        //if (currentResolution < 10 || currentResolution > 100)
        //{
        //	Debug.LogWarning("Grapher resolution out of bounds, resetting to minimum.", this); //this is only for the highlighted effect when clicking on warning messages
        //	currentResolution = 10;
        //}
        resolution = currentResolution;
        points = new GameObject[resolution * resolution];
        Vector2 domain = new Vector2(2, -1);
        float increment = domain.x / (resolution - 1);
        int i = 0;
        for (int x = 0; x < resolution; x++)
        {
            for (int z = 0; z < resolution; z++)
            {
                points[i] = Instantiate(point);
                points[i].transform.position = new Vector3(domain.y + increment * x, 0f, domain.y + increment * z);
                points[i].GetComponent<MeshRenderer>().material.color = new Color(increment * x, 0f, increment * z);
                Debug.Log(i);
                i++;
            }

        }
    }

    private delegate float FunctionDelegate(Vector3 p);
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
        //float t = Time.timeSinceLevelLoad;

        for (int i = 0; i < points.Length; i++)
        {
            Vector3 p = points[i].transform.position;
            p.y = f(p);
            points[i].transform.position = p;
            points[i].GetComponent<MeshRenderer>().material.color = new Color(points[i].transform.position.x, points[i].transform.position.y, 0f);
        }
    }
    private static float Linear( Vector3 p)
    {
        return p.x;
    }
    private static float Linear(Vector3 p, float coefficeint)
    {
        return coefficeint * p.x;
    }
    private static float Linear(Vector3 p, float coefficeint, float intercept)
    {
        return coefficeint * p.x + intercept;
    }
    private static float Exponential(Vector3 p)
    {
        return Mathf.Exp(p.x);
    }

    private static float Parabola(Vector3 p)
    {
        return p.x * p.x;
    }

    private static float Sine(Vector3 p)
    {
        return 2f * Mathf.Sin(3f * p.x + Time.timeSinceLevelLoad);
    }

    private static float GeneralizedFunc(Vector3 p, params float[] coeflist)
    {
        float y;
        //ax^2+by^2+cz^2+dxy+exz+fyz+gx+hy+iz+j
        if (coeflist[1]==0)
        {
            //y = ()/(coeflist[3]*)
        }
        return 0;
    }
}
