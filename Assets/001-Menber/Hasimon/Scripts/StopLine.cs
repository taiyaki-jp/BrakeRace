using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using System;
using UnityEngine;
using UnityEngine.UI;

public class StopLine : MonoBehaviour
{
     
    [SerializeField] GameObject _outLine;
    [SerializeField] Transform _line;
    // ↓加速用のやつ
    [SerializeField, Label("一番いい場所"), BoxGroup("加速ボーナス範囲")] float _boostLine1;
    [SerializeField, Label("その次"),      BoxGroup("加速ボーナス範囲")] float _boostLine2;
    [SerializeField, Label("おまけ加速"), 　BoxGroup("加速ボーナス範囲")] float _boostLine3;
    [SerializeField, Label("一番いい"), 　　BoxGroup("加速ボーナス数値")] float _boost1;
    [SerializeField, Label("その次"),      BoxGroup("加速ボーナス数値")] float _boost2;
    [SerializeField, Label("おまけ"),      BoxGroup("加速ボーナス数値")] float _boost3;

    private Rigidbody _player;
    private CarController carcontroller;
    private CameraDolly dolly;
    private OutLine outLine;
    private FadeManager fade;

    private bool isdone = false;

    private void Start()
    {
        _player=GameObject.Find("Player").GetComponent<Rigidbody>();
        carcontroller = GameObject.Find("Player").GetComponent<CarController>();
        dolly =GameObject.Find("Player").GetComponent<CameraDolly>();
        outLine=_outLine.GetComponent<OutLine>();
        fade = GameObject.Find("FadeManager").GetComponent<FadeManager>();
    }
    private async void OnTriggerStay(Collider other)
    {
        if(_player.velocity.magnitude <= 0.01&& ! isdone) 
        {
            isdone = true;
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
            fade.Fade<Enum>("TitleScene",Image.FillMethod.Radial180,Radial_180_Origin.Buttom,Image.FillMethod.Vertical,VerticalOrigin.Top);
            return;
        }
        await dolly.DoDollyBack();
    }
}
