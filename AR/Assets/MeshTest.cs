using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshTest : MonoBehaviour
{
    public enum FunctionOption : int
    {
        Linear = 0,
        Exponential,
        Parabola,
        Sine
    }
    public FunctionOption function;
    public List<Vector3> points;
    [Range(10, 100)]
    public int accuracy;
    public Vector2 domain;
    public float[] coeflist;
    public float increment;
    // Use this for initialization
    void Start()
    {

        accuracy = 5;
        coeflist = new float[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        //increment = 20f / ((float)accuracy - 1f);
        increment = 5f;
        domain = new Vector2(-10f, 10f);
        points = CreateVertices();

        //create mymesh and set the vertices tris array
        Mesh myMesh = new Mesh();
        //myMesh.triangles = new int[] { 0, 1, 2, 3, 4, 5 };
        myMesh.SetVertices(points);
        //myMesh.SetTriangles(points);
        AssignTris(myMesh);
        this.GetComponent<MeshFilter>().mesh = myMesh;
        //give it to this gameobject

        //myMesh.vertices = new Vector3[]{ new Vector3(0,0,0), new Vector3(1, 0, 0) , new Vector3(0, 0, 1) , new Vector3(1, 0, 1) };
        //myMesh.triangles = new int[] { 0, 1, 2, 0, 1, 3, 0, 2, 3, 1, 2, 3,0,0,0};


    }

    /// <summary>
    /// Assign how to connect the closest points found and the indice itself
    /// </summary>
    /// <param name="myMesh"></param>
    private void AssignTris(Mesh myMesh)
    {
        Debug.Log("kkkkk");
        int[] tempa = new int[60000];//(accuracy - 1) * (accuracy - 1) * 4 * 3
        List<List<int>> triangles;
        int zz = 0;
        for (int i = 0; i < points.Count; i++)
        {
            triangles = FindClosestPoints(points, i, increment);
            if (triangles[0].Count == 1 && triangles[1].Count == 1)
            {
                tempa[zz] = i;
                zz++;
                tempa[zz] = triangles[0][0];
                zz++;
                tempa[zz] = triangles[1][0];
                zz++;
            }
            else if (triangles[0].Count == 2 && triangles[1].Count == 2)
            {
                Debug.Log("kk" + triangles[0].Count);
                Debug.Log("kk" + triangles[1].Count);
                Debug.Log(points.Count);
                for (int ix = 0; ix < 2; ix++)
                {
                    for (int iz = 0; iz < 2; iz++)
                    {

                        tempa[zz] = i;
                        zz++;
                        tempa[zz] = triangles[0][ix];
                        zz++;
                        tempa[zz] = triangles[1][iz];
                        zz++;
                    }
                }
            }
            else
            {
                if (triangles[0].Count == 1)
                {
                    Debug.Log("kkk" + triangles[0].Count);
                    Debug.Log("kkk" + triangles[1].Count);
                    Debug.Log(points.Count);
                    for (int iz = 0; iz < 2; iz++)
                    {

                        tempa[zz] = i;
                        zz++;
                        tempa[zz] = triangles[0][0];
                        zz++;
                        tempa[zz] = triangles[1][iz];
                        zz++;
                    }
                }
                else
                {
                    //for debugging only
                    Debug.Log("kkkk" + triangles[0].Count);
                    Debug.Log("kkkk" + triangles[1].Count);
                    Debug.Log(points.Count);

                    for (int ix = 0; ix < 2; ix++)
                    {

                        tempa[zz] = i;
                        zz++;
                        tempa[zz] = triangles[0][ix];
                        zz++;
                        tempa[zz] = triangles[1][0];
                        zz++;
                    }
                }
            }
        }

        //give the values of tempa to TRUE triangles array
        myMesh.triangles = tempa;
    }

    private List<Vector3> CreateVertices()
    {
        List<Vector3> points = new List<Vector3>();
        Vector3 temp = new Vector3();
        //Vector2 domain = new Vector2(2, -1);
        //increment = 2 / (accuracy - 1);
        for (int x = 0; x <= (domain.y - domain.x) / increment; x++)
        {
            for (int z = 0; z <= (domain.y - domain.x) / increment; z++)
            {
                temp.x = domain.x + increment * x;
                temp.z = domain.x + increment * z;
                temp.y = x * x + z * z;//YRelationFunc(temp.x, temp.z, coeflist);
                points.Add(temp);

                //points[i].GetComponent<MeshRenderer>().material.color = new Color(increment * x, 0f, increment * z);
                Debug.Log(temp);
            }
        }
        return points;
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.localScale = new Vector3(currentResolution, currentResolution, currentResolution);
    }

    /// <summary>
    /// calculte y from the relationship f(x,z)
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    /// <param name="coeflist"></param>
    /// <returns></returns>
    private static float YRelationFunc(float x, float z, float[] coeflist)
    {
        float y;
        //ax ^ 2 + by ^ 2 + cz ^ 2 + dxy + exz + fyz + gx + hy + iz + j

        //if (coeflist[1] != 0)
        //{

        //}

        //else
        //{
        //    y = () / (coeflist[3] *)
        //}

        y = coeflist[0] * x * x + coeflist[1] * z * z + coeflist[2] * x * z + coeflist[3] * x + coeflist[4] * z + coeflist[5];
        return 0;
    }

    //fail version of FingClosedPoints Method
    //private static List<int[]> FindClosestPoints(List<Vector3> points, int targetindex, float distance)
    //{
    //    List<int[]> triangles = new List<int[]>();
    //    int[] xtriangles = new int[] { };
    //    int[] ztriangles = new int[] { };
    //    int lengthx = 0;
    //    int lengthz = 0;
    //    int complementx = 0;
    //    int complementz = 0;
    //    int[] temp = new int[2];
    //    for (int i = 0; i < points.Count; i++)
    //    {
    //        if (points[i].x == points[targetindex].x + distance || points[targetindex].x == points[i].x + distance)
    //        {
    //            lengthx++;
    //            temp[lengthx - 1] = i;
    //            xtriangles = new int[lengthx];
    //            for (int j = 0; j < lengthx; j++)
    //            {
    //                xtriangles[j] = temp[lengthx - 1 + j - complementx];
    //            }
    //            complementx++;
    //        }
    //        if (points[i].z == points[targetindex].z + distance || points[targetindex].z == points[i].z + distance)
    //        {
    //            lengthz++;
    //            temp[lengthz - 1] = i;
    //            ztriangles = new int[lengthz];
    //            for (int j = 0; j < lengthz; j++)
    //            {
    //                ztriangles[j] = temp[lengthz - 1 + j - complementz];
    //            }
    //            complementz++;
    //        }
    //    }
    //    triangles.Add(xtriangles);
    //    triangles.Add(ztriangles);
    //    return triangles;
    //}

    /// <summary>
    /// Find possible closest points of a indice
    /// </summary>
    /// <param name="points">indices</param>
    /// <param name="targetindex"></param>
    /// <param name="distance"></param>
    /// <returns></returns>
    private static List<List<int>> FindClosestPoints(List<Vector3> points, int targetindex, float distance)
    {
        List<List<int>> triangles = new List<List<int>>();
        triangles.Add(new List<int>());
        triangles.Add(new List<int>());
        for (int i = 0; i < points.Count; i++)
        {
            if ((points[i].x == points[targetindex].x + distance || points[targetindex].x == points[i].x + distance) && points[i].z == points[targetindex].z)
            {
                triangles[0].Add(i);
            }
            if ((points[i].z == points[targetindex].z + distance || points[targetindex].z == points[i].z + distance) && points[i].x == points[targetindex].x)
            {
                triangles[1].Add(i);
            }
        }
        return triangles;
    }
}
