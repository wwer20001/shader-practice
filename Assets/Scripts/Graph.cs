using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    public Transform pointPrefab;

    [Range(10, 100)]
    public int resolution = 10;

    Transform[] points;


    private void Awake()
    {
        float step = 2f / resolution;
        Vector3 scale = Vector3.one * step;
        Vector3 position = new Vector3();
        points = new Transform[resolution];
        for (int i = 0; i < points.Length; i++)
        {
            var point = Instantiate(pointPrefab);
            position.x = (i + 0.5f) * step - 1f;
            //position.y = position.x * position.x * position.x;
            point.localPosition = position;
            point.localScale = scale;
            point.SetParent(transform, false);
            points[i] = point;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float t = Time.time;
        for(int i = 0; i < points.Length; i++)
        {
            Transform point = points[i];
            Vector3 position = point.position;
            //position.y = position.x * position.x * position.x;
            //position.y = Mathf.Sin(Mathf.PI * (position.x + Time.time));
            position.y = SineFunction(position.x, t);
            point.localPosition = position;
        }
    }

    float SineFunction(float x, float t)
    {
        return Mathf.Sin(Mathf.PI * (x + t));
    }
}
