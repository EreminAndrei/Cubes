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
    
    public event Action<Vector3, List <Rigidbody>> Spawned;
       
    private void OnEnable()
    {
        _cubeLogic.Reborn += OnReborn;
    }

    private void OnDisable()
    {
        _cubeLogic.Reborn -= OnReborn;
    }

    private void OnReborn(GameObject cube)
    {  
        int minAmountToSpawn = 2;
        int maxAmountToSpwan = 6;
        int amountToSpawn = UnityEngine.Random.Range(minAmountToSpawn, maxAmountToSpwan + 1);        
        
        List <Rigidbody> cubesToMove = new ();
        
        cube.transform.localScale /= 2;
        
        _cubeLogic.ChanceDecrease();
        
        Vector3 position = cube.transform.position;
        
        for (int i = 0; i < amountToSpawn; i++)
        {
            int randomMaterial = UnityEngine.Random.Range(0, _materials.Length);

            if (cube.GetComponent<MeshRenderer>() != null)
            {
                MeshRenderer rand = cube.GetComponent<MeshRenderer>();
                rand.material = _materials[randomMaterial];
            }            
            
            GameObject newCube = Instantiate(cube, position, Quaternion.identity);
            
            if (newCube.GetComponent<Rigidbody>() != null)
                cubesToMove.Add(newCube.GetComponent<Rigidbody>());           
        } 
        
        Spawned?.Invoke(position, cubesToMove);
    }    
}
