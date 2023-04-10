using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class MakeCubeGrid : MonoBehaviour
{
    private List<Transform> cubes = new();
    public float yMin = 12f;
    public float yMax = 18f;
    
    public float DistanceDelayMultipler = 1f;

    [Range(5,50)]
    public int gridXDim = 20;
    
    [Range(5,50)]
    public int gridZDim = 20;

    [Range(0f,1f)]
    public float spacing = 0.5f;

    [Range(0f,1f)]
    public float cubeWidth = 0.4f;

    [MinMaxSlider(0.01f,0.05f, true)]
    public Vector2 RandomWaitTime = new Vector2();

    public float MinFreq = 0.8f;
    public float MaxFreq = 0.8f;

    public float MinHalfLife = 0.4f;
    public float MaxHalfLife = 0.4f;
    
    public 
    void Start()
    {
        GroundCube[] cubes = transform.GetComponentsInChildren<GroundCube>();
        foreach (GroundCube cube in cubes)
        {
            cube.SetChasePos(cube.transform.position);
            float posDelay = ((cube.transform.localPosition.z + 5) + (cube.transform.localPosition.x + 5)) * DistanceDelayMultipler;
            cube.transform.localPosition -= Vector3.up * Mathf.Lerp(0f, 25f, posDelay/gridXDim);
            cube.Init();
        }
    }

    [Button]
    public void CreateCubes()
    {
        string GameObjectName = "Cubes";

        GameObject parent = GameObject.Find(GameObjectName);
        if (parent != null) DestroyImmediate(parent);
        parent = new GameObject(GameObjectName);
        parent.transform.SetParent(this.transform);

        for (int i = 0; i < gridXDim; i++)
        {
            for (int j = 0; j < gridZDim; j++)
            {
                Vector3 newPos = new Vector3(i * spacing - (gridXDim/2*spacing), 0f, j * spacing - (gridZDim/2*spacing));
                GameObject newCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cubes.Add(newCube.transform);

              //  newCube.GetComponent<MeshRenderer>().shadowCastingMode = ShadowCastingMode.Off;

                GroundCube comp = newCube.AddComponent<GroundCube>();
                comp.spring.Frequency = Random.Range(MinFreq, MaxFreq);
                comp.spring.HalfLife = Random.Range(MinHalfLife, MaxHalfLife);
                comp.DistanceDelayMultipler = DistanceDelayMultipler;
                comp.MinRandWait = RandomWaitTime.x;
                comp.MaxRandWait = RandomWaitTime.y;
                comp.GridSpacing = gridXDim / 2;

    
                newCube.name = newPos.ToString();
                newCube.transform.localScale *= cubeWidth;
                newCube.transform.position = newPos;
                newCube.transform.SetParent(parent.transform, true);
            }
        }
    }
}
