using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopLine : MonoBehaviour
{
    [SerializeField] Rigidbody _player;
    [SerializeField] GameObject _outLine;
    [SerializeField] Transform _line;
    [SerializeField] float _plusSpeed;

    private void OnTriggerStay(Collider other)
    {
        if(_player.velocity.magnitude <= 0.1) 
        {
            float range = _player.transform.position.x - _line.position.x; 
            //‘¬“x‚ð•Ï‚¦‚é
            if(range < 1)
            {

            }
            else if(range < 2)
            { }
            else if(range < 3)
            { }
            else
            { }
        }
        
        Destroy(_outLine);
    }
}
