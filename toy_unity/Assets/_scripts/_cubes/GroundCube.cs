using System.Collections;
using UnityEngine;

public class GroundCube : MonoBehaviour
{
    public VecSpringDamp spring = new();

    public float GridSpacing;
    public float DistanceDelayMultipler = 1f;

    public float MinRandWait = 0.1f;
    public float MaxRandWait = 1f;
    

    private Vector3 _chasePos;
    private bool _falling = false;

    public void Init()
    {
        spring.Init(transform.position);
        StartCoroutine(WaitForFall());
    }

    private IEnumerator WaitForFall()
    {
        float posDelay = ((transform.position.z + GridSpacing) + (transform.position.x + GridSpacing)) * DistanceDelayMultipler;
        yield return new WaitForSeconds(Random.Range(posDelay * 0.03f, posDelay * 0.03f));
        _falling = true;
    }

    public void SetChasePos(Vector3 chasePos)
    {
        _chasePos = chasePos;
    }
    
    void Update()
    {
        if (_falling)
        {
            Vector3 currPos = spring.MoveTowards(_chasePos, 0f);
            transform.position = currPos;
        }
    }
}