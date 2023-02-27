using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel_Spawner : MonoBehaviour
{
    public GameObject Barrel_Prefab;
    public float min_Time = 2f;
    public float max_Time = 4f;
    private void Start()
    {   
        Spawn();
    }
    public void Spawn()
    {
        Instantiate(Barrel_Prefab, transform.position, Quaternion.identity);
        Invoke(nameof(Spawn), Random.Range(min_Time, max_Time));
    }
}
