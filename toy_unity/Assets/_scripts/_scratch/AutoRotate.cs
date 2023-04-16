using UnityEngine;

public class AutoRotate : MonoBehaviour
{
    public float AngularSpeed = 10f;
    public Vector3 Axis = Vector3.up;
    
    void Update()
    {
        transform.RotateAround(transform.position, Axis, AngularSpeed*Time.deltaTime);        
    }
}
