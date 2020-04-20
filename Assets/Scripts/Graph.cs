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
        MultiSineFunction
    };

    private void Awake()
    {
        float step = 2f / resolution;
        Vector3 scale = Vector3.one * step;
        Vector3 position = new Vector3();
        points = new Transform[resolution * resolution];
        for (int i = 0, z = 0; z < resolution; z++)
        {
            position.z = (z + 0.5f) * step - 1f;
            for (int x = 0; x < resolution; x++, i++)
            {
                var point = Instantiate(pointPrefab);
                position.x = (x + 0.5f) * step - 1f;
                //position.y = position.x * position.x * position.x;
                point.localPosition = position;
                point.localScale = scale;
                point.SetParent(transform, false);
                points[i] = point;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        float t = Time.time;
        GraphFunction f = functions[(int)function];
        for (int i = 0; i < points.Length; i++)
        {
            Transform point = points[i];
            
            Vector3 position = point.position;
            position.y = f(position.x, position.z, t);
            point.localPosition = position;
        }
    }

    const float pi = Mathf.PI;

    static float SineFunction(float x, float z, float t)
    {
        return Mathf.Sin(pi * (x + t));
    }

    static float Sine2DFunction(float x, float z, float t)
    {
        float y = Mathf.Sin(pi * (x + t));
        y += Mathf.Sin(pi * (z + t));
        y *= 0.5f;
        return y;
    }

    static float MultiSineFunction(float x, float z, float t)
    {
        float y = Mathf.Sin(pi * (x + t));
        y += Mathf.Sin(2f * pi * (x + 2f * t)) / 2f;
        y *= 2f / 3f;
        return y;
    }
}
