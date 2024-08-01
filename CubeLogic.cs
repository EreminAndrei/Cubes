using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeLogic : MonoBehaviour
{
    [SerializeField] private float _chanceToReborn;
    
    public event Action<GameObject> Reborn;
    
    public void ChanceDecrease()
    {
        _chanceToReborn /= 2;
    }

    private void OnMouseUpAsButton ()
    {
        int random = UnityEngine.Random.Range(0, 99);        

        if (_chanceToReborn > random)
        {
            Reborn?.Invoke(gameObject);           
        }        
            
        Destroy(gameObject);        
    }
}
