using System;
using UnityEngine;
// ReSharper disable InconsistentNaming

public class Tentacle : MonoBehaviour
{
    public Camera mainCamera;

    public float raycastDistance;
    public float vectorRotation;
    public bool vibrate;
    public float rehookDistanse;

    private LineRenderer _lineRenderer;
    private SpringJoint2D _springJoint;
    private Rigidbody2D _rigidbody;
    private bool holdHook;

    public void Start()
    {
        _lineRenderer = transform.GetComponent<LineRenderer>();
        _springJoint = transform.GetComponent<SpringJoint2D>();
        _rigidbody = transform.GetComponent<Rigidbody2D>();
        _springJoint.enabled = false;
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        //Debug.Log(Math.Abs(_rigidbody.velocity.y));
        if (vibrate && Math.Abs(_rigidbody.velocity.y)>1.5)
        {
            var soundInstance = SoundParticlePool.Instance.GetObject(SoundParticlePool.ObjectInfo.ObjectType.SoundWave);
            soundInstance.GetComponent<VibrationCircle>().OnCreate(transform.position, Color.white);

        }

    }

    public void Update()
    {
        //Debug.DrawRay(transform.position, Quaternion.Euler(0, 0, vectorRotation) * transform.position , Color.green);
        //Debug.Log(Vector3.Distance(mousePos, transform.position));
        HoldHook();

        TentacleController();
    }

    private void TentacleController()
    {
        if (_lineRenderer.enabled)
            _lineRenderer.SetPosition(0, transform.position + new Vector3(0, 0, 2));

        if (Input.GetKey(KeyCode.Mouse0))
        {
            var mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            SpringForce(mousePos);

            RaycastHit2D hit = Physics2D.Raycast(transform.position,
                Quaternion.Euler(0, 0, vectorRotation) * (mousePos - transform.position),
                raycastDistance, 1 << 6);


            if (hit && (Vector3.Distance(_lineRenderer.GetPosition(1), hit.point) > rehookDistanse || !_springJoint.enabled))
            {
                _lineRenderer.SetPosition(1, hit.point);

                _springJoint.enabled = true;
                _lineRenderer.enabled = true;

                _springJoint.connectedAnchor = hit.point;
                //Debug.Log(hit.collider);
                //Instantiate(soundWave, hit.point, new Quaternion(0, 0, 0, 0));
            }
        }
        else if (!holdHook && !Input.GetKeyDown(KeyCode.Mouse0))
        {
            _springJoint.enabled = false;
            _lineRenderer.enabled = false;
        }
    }

    private void HoldHook()
    {
        if (Input.GetKeyDown(KeyCode.H))
            holdHook = !holdHook;
    }

    private void SpringForce(Vector3 mousePos)
    {
        var distance = Vector3.Distance(mousePos, transform.position);
        if (distance<11)
            _springJoint.frequency = (float)0.6;
        else if (distance<14)
            _springJoint.frequency = (float)0.4;
        else
            _springJoint.frequency = (float)0.3;
        //Debug.Log(_springJoint.frequency);
        //if (distance < 12)
        //    _springJoint.frequency = (float)0.1;
        //else if (distance < 8)
        //    _springJoint.frequency = (float)0.2;
        //else
        //    _springJoint.frequency = (float)0.3;
    }
}
