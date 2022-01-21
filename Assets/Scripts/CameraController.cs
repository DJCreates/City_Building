using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed,zoomSpeed,rotateSpeed;
    public float minXRot,maxXRot;
    public float minZoom,maxZoom;
    private float _curZoom,_curXRot;
    private Camera _cam;
    void Start()
    {
        //_cam link to main camera and _curZoom takes the main camera Y position
        _cam = Camera.main;
        _curZoom = _cam.transform.localPosition.y;
        _curXRot = -50;
    }

    void Update()
    {
        //Mouse wheel to zoom in and out in Main Camera
        _curZoom += Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        _curZoom = Mathf.Clamp(_curZoom, minZoom, maxZoom);
        _cam.transform.localPosition = Vector3.up * _curZoom;
        
        if(Input.GetMouseButton(1))
        {
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");
            _curXRot += -y * rotateSpeed;
            _curXRot = Mathf.Clamp(_curXRot, minXRot, maxXRot);
            transform.eulerAngles = new Vector3(_curXRot, transform.eulerAngles.y + (x * rotateSpeed), 0.0f);
        }

        Vector3 forward = _cam.transform.forward;
        forward.y = 0.0f;
        forward.Normalize();
        Vector3 right =  _cam.transform.right.normalized;

        //GetAxisRaw is the same as GetAxis without the smoothness
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 dir = forward * moveZ + right * moveX;
        dir.Normalize();
        dir *= moveSpeed * Time.deltaTime;
        transform.position += dir;
    }
}
