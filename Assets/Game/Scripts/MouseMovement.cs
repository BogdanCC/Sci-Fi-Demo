using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour {

    [SerializeField]
    private float _mouseSensitivity = 2f;
    private float _mouseX;
    private float _mouseY;
    private Transform lookY;

	// Use this for initialization
	void Start () {
        lookY = transform.Find("LookY");
	}
	
	// Update is called once per frame
	void Update () {

        _mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity;
        _mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity;

        Vector3 thisRotation = transform.localEulerAngles;
        Vector3 lookYRotation = lookY.localEulerAngles;

        thisRotation.y += _mouseX;
        lookYRotation.x -= _mouseY;

        transform.localEulerAngles = thisRotation;
        lookY.localEulerAngles = lookYRotation;

    }
}
