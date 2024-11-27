using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopLine : MonoBehaviour
{
    [SerializeField] Rigidbody _player;
    [SerializeField] GameObject _outLine;
    [SerializeField] Transform _line;
    // ↓加速用のやつ
    [SerializeField] float _boostLine1;
    [SerializeField] float _boostLine2;
    [SerializeField] float _boostLine3;
    [SerializeField] float _boost1;
    [SerializeField] float _boost2;
    [SerializeField] float _boost3;

    private void OnTriggerStay(Collider other)
    {
        if(_player.velocity.magnitude <= 0.1) 
        {
            float range = _player.transform.position.x - _line.position.x; 
            CarController carcontroller;
            GameObject obj = GameObject.Find("objname");
            carcontroller = obj.GetComponent<CarController>();
            //速度を変える
            if(range < _boostLine1)// 車の大きさによって距離いじってくれ
            {
                //carcontroller. = _boost1;    //初速の変数名と数値
            }
            else if(range < _boostLine2)
            {
                //carcontroller. = _boost2;
            }
            else if(range < _boostLine3)
            {
                //carcontroller. = _boost3;
            }
            else
            {

            }

            Destroy(_outLine);
        }
        
        
    }
}
