using System.Collections.Generic;
using UnityEngine;

public class ScaleIncreaser : MonoBehaviour
{
    [SerializeField] private List<Material> materials;
    private Rigidbody rb;
    private Material m;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        m = GetComponent<Renderer>().material;

        SelectRandomMaterial();
    }

    public void Increase()
    {
        transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
        rb.mass += 1;
        SelectRandomMaterial();
    }

    private void SelectRandomMaterial()
    {
        m = materials[Random.Range(0, materials.Count)];
    }
}
