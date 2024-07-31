using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawner : MonoBehaviour
{
    [SerializeField] private CubeLogic _cubeLogic;
    [SerializeField] private Material[] _materials = new Material[5];
    
    public event Action<Vector3> Spawn;
       
    private void OnEnable()
    {
        _cubeLogic.Reborn += OnReborn;
    }

    private void OnDisable()
    {
        _cubeLogic.Reborn -= OnReborn;
    }

    private void OnReborn(GameObject Cube)
    {  
        Cube.transform.localScale /= 2;
        
        _cubeLogic.ChanceDecrease();

        int amountToSpawn = UnityEngine.Random.Range(2, 7);        
        
        for (int i = 0; i < amountToSpawn; i++)
        {
            int randomMaterial = UnityEngine.Random.Range(0, _materials.Length);
            
            Cube.GetComponent<MeshRenderer>().material = _materials[randomMaterial];
            
            Instantiate(Cube, transform.position, Quaternion.identity);             
        } 
        
        Spawn?.Invoke(transform.position);
    }    
}
