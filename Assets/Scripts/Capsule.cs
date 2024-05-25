using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capsule : MonoBehaviour
{
    [SerializeField] private CustomObjects customMaterial;

    private Renderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        ApplyCustomMaterial();
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

    public CustomObjects GetCustomMaterial()
    {
        return customMaterial;
    }
}
