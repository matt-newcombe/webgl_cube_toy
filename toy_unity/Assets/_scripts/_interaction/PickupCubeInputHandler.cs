using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCubeInputHandler : MonoBehaviour
{
    public float explosionForce = 1f;
    public float explosionRadius = 10f;
    
    public Camera MousePickCamera;
    public float HeightAboveCubeField = 2f;
    
    private GroundCube _touchCube;
    private Vector3 _cubeStartPos;

    public Transform CubeParent;
    private Rigidbody[] _cubes;
    
    enum State
    {
        Holding,
        Released
    }

    private State _state = State.Released;

    private void Start()
    {
        _cubes = CubeParent.GetComponentsInChildren<Rigidbody>();
    }
    
    private void Update()
    {
        switch (_state)
        {
            case State.Holding:
                // On Left Click
                if (Input.GetMouseButtonDown(0))
                {
                    MouseReleaseCube();
                }
                else UpdateTouchCube();
                break;
            case State.Released:
                // On Left Click
                if (Input.GetMouseButtonDown(0))
                {
                    Impulse(GetTouchOnPlane());
                    MousePickCube();
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

    }

    private void MousePickCube()
    {
        Vector3 worldPoint = GetTouchOnPlane();
        
        // Now find the cube at that point - we can do this with a logical structure, but I'm feeling lazy so let's raycast again
        Ray mouseRay = MousePickCamera.ScreenPointToRay(Input.mousePosition);
        Ray downRay = new Ray(worldPoint, Vector3.down);
        Physics.SphereCast(mouseRay, 0.15f, out RaycastHit hitInfo);
        Debug.DrawRay(downRay.origin, downRay.direction*5f, Color.green, 8f);

        Transform cubeHit = hitInfo.transform;
            _cubeStartPos = cubeHit.position;
        _touchCube = cubeHit.GetComponent<GroundCube>();
        _touchCube.SetChasePos(worldPoint);

        _state = State.Holding;
    }

    private Vector3 GetTouchOnPlane()
    {
        Ray mouseRay = MousePickCamera.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, HeightAboveCubeField*Vector3.up);
        plane.Raycast(mouseRay, out float enter);

        return mouseRay.GetPoint(enter);
    }

    private void UpdateTouchCube()
    {
        _touchCube.SetChasePos(GetTouchOnPlane());
    }

    private void MouseReleaseCube()
    {
        _touchCube.SetChasePos(_cubeStartPos);
        _state = State.Released;
    }

    private void Impulse(Vector3 pos)
    {
        foreach (var cube in _cubes)
        {
            cube.AddExplosionForce(explosionForce, pos, explosionRadius);
        }   
    }
}
