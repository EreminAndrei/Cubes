using System;
using UnityEngine;

public class CubeLogic : MonoBehaviour
{
    [SerializeField] private float _chanceToReborn;    

    private int _minRandomChance = 0;
    private int _maxRandomChance = 99;
    private int _decreaseRate = 2;
    
    public event Action<CubeLogic> Reborn;

    private void OnMouseUpAsButton ()
    {
        int random = UnityEngine.Random.Range(_minRandomChance, _maxRandomChance +1);        

        if (_chanceToReborn > random)
        {
            Reborn?.Invoke(this);           
        }        
            
        Destroy(gameObject);        
    }

    public void ChanceDecrease()
    {
        _chanceToReborn /= _decreaseRate;
    }
}
