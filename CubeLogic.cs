using System;
using UnityEngine;

public class CubeLogic : MonoBehaviour
{
    [SerializeField] private float _chanceToReborn;
    
    public event Action<GameObject> Reborn;

    private int _minRandomChance = 0;
    private int _maxRandomChance = 99;
    private int _decreaseRate = 2;
    
    public void ChanceDecrease()
    {
        _chanceToReborn /= _decreaseRate;
    }

    private void OnMouseUpAsButton ()
    {
        int random = UnityEngine.Random.Range(_minRandomChance, _maxRandomChance +1);        

        if (_chanceToReborn > random)
        {
            Reborn?.Invoke(gameObject);           
        }        
            
        Destroy(gameObject);        
    }
}
