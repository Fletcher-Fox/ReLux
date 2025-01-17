using System;
using UnityEngine;

public class BoardBehavior : MonoBehaviour
{   
    
    [SerializeField] private IntegerSO length;
    [SerializeField] private IntegerSO width;
    [SerializeField] private GameObject prefab; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        // X value
        for (int i = 0; i < length.Value; i++)
        {
            // Y value
            for (int j = 0; j < width.Value; j++)
            {   
                Vector3 temp_vector = new Vector3(i, 0, j);
                GameObject space = Instantiate(prefab, temp_vector, Quaternion.identity);
                space.name = "Space_" + i + "_" + j;
            }
        }
    }
}
