using UnityEngine;

public class SpringTestPackage : MonoBehaviour
{
    public Transform chaseTransform;
    public VecSpringDamp spring = new();

    void Start()
    {
        spring.Init(transform.position);
    }
    
    void Update()
    {
        Vector3 currPos = spring.MoveTowards(chaseTransform.position, 0f);
        transform.position = currPos;
    }
}