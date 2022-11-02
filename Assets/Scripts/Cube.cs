using UnityEngine;

public class Cube : MonoBehaviour
{
    public Material material { get ; private set; }

    private void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
    }
}
