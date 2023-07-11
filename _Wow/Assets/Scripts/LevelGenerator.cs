using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    [SerializeField] private GameObject[] platformPrefab = new GameObject[10];
    public float levelWidth = 2.5f;
    public float minX = -3.5f;
    public float maxX = 3.5f;

    private static Vector3 spawnPosition = new Vector3();

    public static Vector3 SpawnPosition { get => spawnPosition; set => spawnPosition = value; }


    // Start is called before the first frame update
    void Start()
    {
        
        for (int i = 0; i < platformPrefab.Length; i++)
        {
            spawnPosition.x += Random.Range(minX, maxX);
            spawnPosition.y = Random.Range(levelWidth, -levelWidth);
            Instantiate(platformPrefab[i], spawnPosition, Quaternion.identity);
        }
        //platformPrefab[4] = Instantiate(platformPrefab[1], spawnPosition, Quaternion.identity);;
    }
}
