using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParabolicLaunch : MonoBehaviour
{
    [SerializeField] private Rigidbody ball;
    [SerializeField] private Transform target;
    [SerializeField] private GameObject point;
    public float h = 10;
    public float gravity = -18;
    public bool debugPath;
    private List<GameObject> points = new List<GameObject>();
    private bool hasCollided = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Launch();
        }

        if (debugPath && !hasCollided)
        {
            DrawPath();
        }
    }
    void Launch()
    {
        Physics.gravity = Vector3.up * gravity;
        ball.useGravity = true;
        ball.velocity = CalculateLaunchData().initialVelocity;
        StartCoroutine(DestroyPoint());
    }

    LaunchData CalculateLaunchData()
    {
        float displacementY = target.position.y - ball.position.y;
        Vector3 displacementXZ = new Vector3(target.position.x - ball.position.x, 0, target.position.z - ball.position.z);
        float time = Mathf.Sqrt(-2 * h / gravity) + Mathf.Sqrt(2 * (displacementY - h) / gravity);
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * h);
        Vector3 velocityXZ = displacementXZ / time;

        return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
    }

    void DrawPath()
    {
        ClearMarkers();

        LaunchData launchData = CalculateLaunchData();
        Vector3 previousDrawPoint = ball.position;

        int resolution = 30;
        for (int i = 1; i <= resolution; i = i + 1)
        {
            float simulationTime = i / (float)resolution * launchData.timeToTarget;
            Vector3 displacement = launchData.initialVelocity * simulationTime + Vector3.up * gravity * simulationTime * simulationTime / 2f;
            Vector3 drawPoint = ball.position + displacement;

            GameObject marker = Instantiate(point, drawPoint, Quaternion.identity);
            points.Add(marker);
        }
    }

    void ClearMarkers()
    {
        for (int i = 0; i < points.Count; i = i + 1)
        {
            Destroy(points[i]);
        }
        points.Clear();
    }

    IEnumerator DestroyPoint()
    {
        while (!hasCollided)
        {
            if (points.Count > 0)
            {
                Destroy(points[0]);
                points.RemoveAt(0);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        hasCollided = true;
        ClearMarkers();
    }

    struct LaunchData
    {
        public readonly Vector3 initialVelocity;
        public readonly float timeToTarget;

        public LaunchData(Vector3 initialVelocity, float timeToTarget)
        {
            this.initialVelocity = initialVelocity;
            this.timeToTarget = timeToTarget;
        }
    }
}
