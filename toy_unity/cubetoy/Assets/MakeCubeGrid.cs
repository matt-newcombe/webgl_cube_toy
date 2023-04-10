using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeCubeGrid : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnEnable()
    {
        CreateCubes();
    }

    public static void CreateCubes()
    {
        int x = 100;
        int y = 100;

        float step = 0.5f;

        float cubeWidth = 0.4f;

        string GameObjectName = "Cubes";

        GameObject parent = GameObject.Find(GameObjectName);
        if (parent != null) DestroyImmediate(parent);
        parent = new GameObject(GameObjectName);

        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                Vector3 newPos = new Vector3(i * step - (x/2*step), 0f, j * step - (y/2*step));
                GameObject newCube = GameObject.CreatePrimitive(PrimitiveType.Cube);

                newCube.name = newPos.ToString();
                newCube.transform.localScale *= cubeWidth;
                newCube.transform.position = newPos;
                newCube.transform.SetParent(parent.transform, true);
            }
        }
    }
}
