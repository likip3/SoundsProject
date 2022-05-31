using UnityEngine;

public class ArtifactSpawner : MonoBehaviour
{
    public GameObject[] artifacts;

    private void Start()
    {
        Instantiate(artifacts[Random.Range(0, artifacts.Length)], transform.position, new Quaternion());
        Destroy(gameObject);
    }
}