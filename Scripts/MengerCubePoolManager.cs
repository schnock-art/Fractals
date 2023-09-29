using System.Collections.Generic;
using UnityEngine;

public class MengerCubePoolManager : MonoBehaviour
{
    public static MengerCubePoolManager Instance { get; private set; }

    public GameObject cubePrefab;
    public int initialPoolSize = 27; // 3x3x3 cubes for the initial Menger Sponge

    private Queue<GameObject> cubePool = new Queue<GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Make the Object Pool Manager persistent
            InitializeCubePool();
        }
        else
        {
            Destroy(gameObject); // Destroy duplicates
        }
    }

    private void InitializeCubePool()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject cube = Instantiate(cubePrefab, Vector3.zero, Quaternion.identity);
            cube.transform.parent = transform;
            cube.SetActive(false);
            cubePool.Enqueue(cube);
        }
    }

    public GameObject GetCubeFromPool()
    {
        if (cubePool.Count > 0)
        {
            GameObject cube = cubePool.Dequeue();
            cube.SetActive(true);
            return cube;
        }
        else
        {
            // If the pool is empty, create a new cube.
            GameObject newCube = Instantiate(cubePrefab, Vector3.zero, Quaternion.identity);
            return newCube;
        }
    }

    public void ReturnCubeToPool(GameObject cube)
    {
        cube.SetActive(false);
        cubePool.Enqueue(cube);
    }

    // Rest of your Object Pool Manager code...
}
