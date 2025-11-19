using NaughtyAttributes;
using TMPro;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField,Label("最高速度（km/h）")] private float _maxSpeed = 200f;
    [SerializeField,Label("加速度（初期値）")] private float _acceleRation = 5f;
    public float a 
    { 
        get { return _acceleRation; }
        set { _acceleRation = value; }
    }
    [SerializeField,Label("減速度（ブレーキ）")] private float brakeForce = 10f;
    [SerializeField,Label("慣性減速度（操作していないとき）")] private float drag = 2f;
    [SerializeField,Label("加速が緩やかになるタイミング（km/h）")] private float accelerationThreshold = 100f;
    [SerializeField,Label("加速減衰率")] private float acceleDamping = 0.15f;

    //  現在の速度 (m/s)
    private float currentSpeed = 0f;
    //  Rigidbody
    private Rigidbody _rb;
    //  ボタンを押したときのフラグ
    private bool isAccelerating = false;
    public bool Accel
    {
        get { return isAccelerating; }
        set { isAccelerating = value; }
    }

    private bool isBraking = false;
    public bool Braki
    {
        get { return isBraking; }
        set { isBraking = value; }
    }

    //  雨フラグ
    private bool isRain = false;
    public bool IsRain
    {
        set { isRain = value; }
    }

    //  速度表示のテキスト
    [SerializeField,Label("速度のtext")] private TextMeshProUGUI speedText;

    [SerializeField,Label("タイヤ")] TireRotation[] _tires=new TireRotation[4];

    void Start()
    {
        //  Rigidbody を取得
        _rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //  現在の速度をkm/hに変換し、UIの表示
        float currentSpeedkmh = currentSpeed * 3.6f;

        switch (currentSpeedkmh)
        {
            case >= 200.0f:
                _acceleRation = 1f;
                break;
            case >= 180:
                _acceleRation = 5f;
                break;
            case >= 150:
                _acceleRation = 8f;
                break;
            default:
                _acceleRation = 15f;
                break;
        }
        
        //  アクセルを踏んでいる間は加速する
        if (isAccelerating)
        {
            Accele(currentSpeedkmh);
        }
        //  ブレーキを踏んでいる間は減速する
        if (isBraking)
        {
            Brake();
        }
        //  操作をしていないとき、緩やかに減速する
        if( ! isAccelerating && ! isBraking)
        {
            kansei(currentSpeedkmh);
        }

        //  車の前方向に速度を適用
        _rb.velocity = transform.forward * currentSpeed;

        //  現在の速度をUIに表示
        speedText.text = "Speed: " + Mathf.RoundToInt(currentSpeedkmh) + " km/h";

        //タイヤの回転
        if (currentSpeedkmh == 0)
        {
            foreach (var item in _tires)//forEachで配列を回す
            {
                item.Brake();
            }
        }
    }
    private void Accele(float currentSpeedkmh)
    {
        //  現在の速度が閾値を超えたら加速を緩やかにする
        float accele = currentSpeedkmh > accelerationThreshold
            ? _acceleRation * acceleDamping
            : _acceleRation;

        currentSpeed += accele * Time.deltaTime;
        currentSpeed = Mathf.Clamp(currentSpeed, 0, _maxSpeed / 3.6f);  //  最高速度(km/h)を超えないようにする
    }

    private void Brake()
    {
        if (isRain)
        {
            currentSpeed -= brakeForce / 2f * Time.deltaTime;
            currentSpeed = Mathf.Max(currentSpeed, 0);  //  速度がマイナスにならないようにする
        }
        else
        {
            currentSpeed -= brakeForce * Time.deltaTime;
            currentSpeed = Mathf.Max(currentSpeed, 0);  //  速度がマイナスにならないようにする
        }
    }

    private void kansei(float currentSpeedkmh)
    {
        float gensokuritu=Mathf.Min(1, 2 - currentSpeed / 100);
        if (isRain)
        {
            currentSpeed -= drag * (gensokuritu/2) * Time.deltaTime;
            currentSpeed = Mathf.Max(currentSpeed, 0);  //  速度がマイナスにならないようにする
        }
        else
        {
                currentSpeed -= drag  *gensokuritu* Time.deltaTime;
                currentSpeed = Mathf.Max(currentSpeed, 0);  //  速度がマイナスにならないようにする
        }
    }
}
