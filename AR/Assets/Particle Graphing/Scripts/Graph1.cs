using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph1 : MonoBehaviour
{
    public int resolution = 10;
	public ParticleSystem delegatedPS;
	private ParticleSystem.Particle[] points;
    private int currentResolution;

    // Use this for initialization
    void Start()
    {
        CreatePoints();
        //GetComponent<ParticleSystem>().SetParticles(points, points.Length);
    }

    private void CreatePoints()
    {
        //myPS = (ParticleSystem)GetComponent("ParticleSystem");
        if (resolution < 10 || resolution > 100)
        {
            Debug.LogWarning("Grapher resolution out of bounds, resetting to minimum.", this); //this is only for the highlighted effect when clicking on warning messages
            resolution = 10;
        }
        currentResolution = resolution;
		points = new ParticleSystem.Particle[resolution];
        float increment = 1f / (resolution - 1);
        for (int i = 0; i < resolution; i++)
        {
			float x = (float)(i * increment);
			points[i].position = new Vector3(x, 0f, 0f);
			//points[i].startColor = new Color32(255,0,0,0);
			points[i].startSize = 1f;
            Debug.Log(i);
        }
        Debug.Log("Create Points executed");
        GetComponent<ParticleSystem>().SetParticles(points, points.Length);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentResolution != resolution)
        {
            CreatePoints();
        }
        //delegatedPS.SetParticles(points,points.Length);
        //GetComponent<ParticleSystem>().SetParticles(points, points.Length);
        //GetComponent<ParticleSystem>().Emit(points.Length);
        //gb.GetComponent<ParticleSystem>().Emit(10);
        //gb.GetComponent<ParticleSystem> ().Play ();
        Debug.Log(delegatedPS.GetParticles(points));
        //GetComponent<ParticleSystem>().SetCustomParticleData()
		//delegatedPS.Emit(delegatedPS.GetParticles(points));
    }
}
