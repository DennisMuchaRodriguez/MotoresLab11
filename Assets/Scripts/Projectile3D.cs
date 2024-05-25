using UnityEngine;

public class Projectile3D : MonoBehaviour
{
    [SerializeField] private Rigidbody _compRigidbody3d;
    [SerializeField] private CustomObjects customMaterial;
    private GameManager gameManager;
    private Renderer _renderer;

    private void Start()
    {
        _compRigidbody3d = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
        gameManager = FindObjectOfType<GameManager>();

        ApplyCustomMaterial();
    }

    private void Update()
    {
        float angle = Mathf.Atan2(_compRigidbody3d.velocity.y, _compRigidbody3d.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Capsules"))
        {
            Capsule capsule = collision.gameObject.GetComponent<Capsule>();
            if (capsule != null)
            {
                ChangeColorOnCollision(capsule.GetCustomMaterial());
            }

            Destroy(collision.gameObject);

            gameManager.puntacion = gameManager.puntacion + 1;
        }
    }

    private void ApplyCustomMaterial()
    {
        if (_renderer != null && customMaterial != null)
        {
            _renderer.material.color = customMaterial.color;
            _renderer.material.SetFloat("_Shininess", customMaterial.shinines);
            _renderer.material.mainTexture = customMaterial.texture;
        }
    }

    private void ChangeColorOnCollision(CustomObjects capsuleMaterial)
    {
        if (_renderer != null && capsuleMaterial != null)
        {
            _renderer.material.color = capsuleMaterial.color;
            _renderer.material.SetFloat("_Shininess", capsuleMaterial.shinines);
            _renderer.material.mainTexture = capsuleMaterial.texture;
        }
    }
}
