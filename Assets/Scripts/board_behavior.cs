using System;
using UnityEngine;

public class board_behavior : MonoBehaviour
{   

    [SerializeField] private int length; // X value
    [SerializeField] private int width; // Y value
    [SerializeField] private GameObject prefab; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        // X value
        for (int i = 0; i < length; i++)
        {
            // Y value
            for (int j = 0; j < width; j++)
            {   
                Vector3 temp_vector = new Vector3(i, 0, j);
                GameObject space = Instantiate(prefab, temp_vector, Quaternion.identity);
                space.name = "Space_" + i + "_" + j;
            }
        }
    }
}
