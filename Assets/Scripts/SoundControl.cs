using System.Collections.Generic;
using UnityEngine;

public class SoundControl : MonoBehaviour
{
    private const float Gravity = 0.67f;
    [SerializeField] private Camera _camera;
    private float Dist;
    private bool isActive;
    public List<GameObject> SoundParticles;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
            isActive = true;
        else if (Input.GetKeyUp(KeyCode.Mouse1))
            isActive = false;
    }

    private void FixedUpdate()
    {
        SoundGravityControl();
    }

    private void SoundGravityControl()
    {
        if (!isActive) return;
        transform.position = _camera.ScreenToWorldPoint(Input.mousePosition);
        foreach (var t in SoundParticles)
        {
            Dist = Vector3.Distance(transform.position, t.transform.position);
            t.GetComponent<Rigidbody2D>()
                .AddForce((transform.position - t.transform.position) / Dist * Gravity / (Dist * Dist + 1));
        }
    }
}