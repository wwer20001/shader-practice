using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    public Transform pointPrefab;

    [Range(10, 100)]
    public int resolution = 10;

    public GraphFunctionName function;
    
    Transform[] points;

    static GraphFunction[] functions =
    {
        SineFunction,
        Sine2DFunction,
        MultiSineFunction,
        MultiSine2DFunction,
        Ripple,
        Cylinder
    };

    private void Awake()
    {
        float step = 2f / resolution;
        Vector3 scale = Vector3.one * step;
        //Vector3 position = new Vector3();
        points = new Transform[resolution * resolution];
        //for (int i = 0, z = 0; z < resolution; z++)
        //{
        //    position.z = (z + 0.5f) * step - 1f;
        //    for (int x = 0; x < resolution; x++, i++)
        //    {
        //        var point = Instantiate(pointPrefab);
        //        position.x = (x + 0.5f) * step - 1f;
        //        //position.y = position.x * position.x * position.x;
        //        point.localPosition = position;
        //        point.localScale = scale;
        //        point.SetParent(transform, false);
        //        points[i] = point;
        //    }
        //}
        for (int i = 0; i < points.Length; i++)
        {
            Transform point = Instantiate(pointPrefab);
            point.localScale = scale;
            point.SetParent(transform, false);
            points[i] = point;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float t = Time.time;
        GraphFunction f = functions[(int)function];
        //for (int i = 0; i < points.Length; i++)
        //{
        //    Transform point = points[i];
        //    Vector3 position = point.position;
        //    position.y = f(position.x, position.z, t);
        //    point.localPosition = position;
        //}
        float step = 2f / resolution;
        for (int i = 0, z = 0; z < resolution; z++)
        {
            float v = (z + 0.5f) * step - 1f;
            for(int x = 0; x < resolution; x++, i++)
            {
                float u = (x + 0.5f) * step - 1f;
                points[i].localPosition = f(u, v, t);
            }
        }
    }

    const float pi = Mathf.PI;

    static Vector3 SineFunction(float x, float z, float t)
    {
        //return Mathf.Sin(pi * (x + t));

        Vector3 p;
        p.x = x;
        p.y = Mathf.Sin(pi * (x + t));
        p.z = z;
        return p;
    }

    static Vector3 Sine2DFunction(float x, float z, float t)
    {
        //float y = Mathf.Sin(pi * (x + t));
        //y += Mathf.Sin(pi * (z + t));
        //y *= 0.5f;
        //return y;

        Vector3 p;
        p.x = x;
        p.y = Mathf.Sin(pi * (x + t));
        p.y += Mathf.Sin(pi * (z + t));
        p.y *= 0.5f;
        p.z = z;
        return p;
    }

    static Vector3 MultiSineFunction(float x, float z, float t)
    {
        //float y = Mathf.Sin(pi * (x + t));
        //y += Mathf.Sin(2f * pi * (x + 2f * t)) / 2f;
        //y *= 2f / 3f;
        //return y;

        Vector3 p;
        p.x = x;
        p.y = Mathf.Sin(pi * (x + t));
        p.y += Mathf.Sin(2f * pi * (x + 2f * t)) / 2f;
        p.y *= 2f / 3f;
        p.z = z;
        return p;
    }

    static Vector3 MultiSine2DFunction(float x, float z, float t)
    {
        //float y = 4f * Mathf.Sin(pi * (x + z + t * 0.5f));
        //y += Mathf.Sin(pi * (x + t));
        //y += Mathf.Sin(2f * pi * (z + 2f * t)) * 0.5f;
        //y *= 1f / 5.5f;
        //return y;

        Vector3 p;
        p.x = x;
        p.y = 4f * Mathf.Sin(pi * (x + z + t * 0.5f));
        p.y += Mathf.Sin(pi * (x + t));
        p.y += Mathf.Sin(2f * pi * (z + 2f * t)) * 0.5f;
        p.y *= 1f / 5.5f;
        p.z = z;
        return p;
    }

    static Vector3 Ripple(float x, float z, float t)
    {
        //float d = Mathf.Sqrt(x * x + z * z);
        //float y = Mathf.Sin(pi * (4f * d - t));
        //y /= 1f + 10f * d;
        //return y;

        Vector3 p;
        p.x = x;
        float d = Mathf.Sqrt(x * x + z * z);
        p.y = Mathf.Sin(pi * (4f * d - t));
        p.y /= 1f + 10f * d;
        p.z = z;
        return p;
    }

    static Vector3 Cylinder(float u, float v, float t)
    {
        Vector3 p;
        float r = 0.8f + Mathf.Sin(pi * (6f * u + 2f * v + t)) * 0.2f;
        p.x = r * Mathf.Sin(pi * u);
        p.y = v;
        p.z = r * Mathf.Cos(pi * u);
        return p;
    }
}
