using System;
using System.Collections.Generic;
using UnityEngine;

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

    private void OnReborn(CubeLogic cube)
    {  
        int minAmountToSpawn = 2;
        int maxAmountToSpwan = 6;
        int amountToSpawn = UnityEngine.Random.Range(minAmountToSpawn, maxAmountToSpwan + 1);        
        
        List <Rigidbody> cubesToMove = new ();
        
        cube.transform.localScale /= 2;
        
        cube.ChanceDecrease();
        
        Vector3 position = cube.transform.position;

        for (int i = 0; i < amountToSpawn; i++)
        {
            int randomMaterial = UnityEngine.Random.Range(0, _materials.Length);

            if (cube.GetComponent<MeshRenderer>() != null)
            {
                MeshRenderer randerer = cube.GetComponent<MeshRenderer>();
                randerer.material = _materials[randomMaterial];
            }

            Rigidbody newCube = cube.GetComponent<Rigidbody>();

            if (newCube != null)
            { 
                newCube = Instantiate(newCube, position, Quaternion.identity);
                cubesToMove.Add(newCube);
            }
        } 
        
        Spawned?.Invoke(position, cubesToMove);
    }    
}
