using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopLine : MonoBehaviour
{
    [SerializeField] Rigidbody _player;
    [SerializeField] GameObject _outLine;
    [SerializeField] Transform _line;
    // ↓加速用のやつ
    [SerializeField, Label("一番いい場所"), BoxGroup("加速ボーナス範囲")] float _boostLine1;
    [SerializeField, Label("その次"),      BoxGroup("加速ボーナス範囲")] float _boostLine2;
    [SerializeField, Label("おまけ加速"), 　BoxGroup("加速ボーナス範囲")] float _boostLine3;
    [SerializeField, Label("一番いい"), 　　BoxGroup("加速ボーナス数値")] float _boost1;
    [SerializeField, Label("その次"),      BoxGroup("加速ボーナス数値")] float _boost2;
    [SerializeField, Label("おまけ"),      BoxGroup("加速ボーナス数値")] float _boost3;

    CarController carcontroller;
    private CameraDolly dolly;
    private OutLine outLine;
    private FadeManager fade;


    private void Start()
    {
        carcontroller = GameObject.Find("Player").GetComponent<CarController>();
        dolly =GameObject.Find("Player").GetComponent<CameraDolly>();
        outLine=_outLine.GetComponent<OutLine>();
        fade = GameObject.Find("fade").GetComponent<FadeManager>();
    }
    private async void OnTriggerStay(Collider other)
    {
        if(_player.velocity.magnitude <= 0.01) 
        {
            float range = _player.transform.position.z - _line.position.z; 
            
            //速度を変える
            if(range < _boostLine1)// 車の大きさによって距離いじってくれ
            {
                carcontroller.a = _boost1;    //初速の変数名と数値
            }
            else if(range < _boostLine2)
            {
                carcontroller.a = _boost2;
            }
            else if(range < _boostLine3)
            {
                carcontroller.a = _boost3;
            }

            await Juage();
            Destroy(_outLine);
        }
        
        
    }

    private async UniTask Juage()
    {
        dolly.SetWayPoint(GetComponentInParent<Transform>());
        await dolly.DoDolly();
        if (outLine.IsMissed)
        {
            fade.Fade("ResultScene-File");
        }
        await dolly.DoDollyBack();
    }
}
