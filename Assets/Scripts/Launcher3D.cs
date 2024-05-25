using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;
using System;

public class Launcher3D : MonoBehaviour
{
    public static event Action<Material> OnProjectileInstantiated;

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Material fresnelMaterial;
    [SerializeField] private float launchModifier;
    [SerializeField] private Transform launchPoint;
    [SerializeField] private int pointsCount;
    [SerializeField] private float pointSpacing;
    private bool canShoot;
    private Vector3 direction;
    private LineRenderer lineRenderer;
   

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
       
        canShoot = true;
    }

    private void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = transform.position.z - Camera.main.transform.position.z;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 launchPosition = transform.position;

        direction = (mousePosition - launchPosition);

        transform.right = -direction;

        if (Input.GetMouseButtonDown(0) && Time.deltaTime != 0)
        {
            Shoot();
        }

        UpdateLineRenderer();
    }

    private void Shoot()
    {
        if (canShoot)
        {
            
            GameObject projectile = Instantiate(projectilePrefab, launchPoint.position, Quaternion.identity);

            Material newMaterial = new Material(fresnelMaterial);

            OnProjectileInstantiated?.Invoke(newMaterial);

            projectile.GetComponent<Renderer>().material = newMaterial;
            projectile.GetComponent<Rigidbody>().AddForce(-direction.normalized * launchModifier, ForceMode.Impulse);
            
            StartCoroutine(TimeShoot());
        }
    }

    IEnumerator TimeShoot()
    {
        canShoot = false;
        yield return new WaitForSeconds(1.5f);
        canShoot = true;
    }

    private void UpdateLineRenderer()
    {
        lineRenderer.positionCount = pointsCount;

        for (int i = 0; i < pointsCount; i++)
        {
            float time = (float)i / (pointsCount - 1);
            lineRenderer.SetPosition(i, CalculatePointAlongPath(time));
        }
    }

    private Vector3 CalculatePointAlongPath(float time)
    {
        Vector3 initialPosition = launchPoint.position;
        Vector3 initialVelocity = -direction.normalized * launchModifier;
        Vector3 gravity = Physics.gravity;

        Vector3 position = initialPosition + initialVelocity * time + 0.5f * gravity * time * time;

        return position;
    }
}
