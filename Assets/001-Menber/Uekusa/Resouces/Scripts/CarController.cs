using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    //  最高速度（km/h）
    public float _maxSpeed = 200f;
    //  加速度（初期値）
    public float _acceleRation = 5f;
    //  減速度（ブレーキ）
    public float brakeForce = 10f;
    //  慣性減速度（操作していないとき）
    public float drag = 2f;
    //  加速が緩やかになる閾値（km/h）
    public  float accelerationThreshold = 100f;
    //  加速減衰率
    public float acceleDamping = 0.15f;

    //  現在の速度 (m/s)
    private  float currentSpeed = 0f;
    //  Rigidbody
    private Rigidbody _rb;
    //  ボタンを押したときのフラグ
    private bool isAccelerating = false;
    private bool isBraking = false;

    //  雨フラグ
    public bool isRain = false;

    //  速度表示のテキスト
    public TextMeshProUGUI speedText;

    [SerializeField] TireRotation _tirefl;
    [SerializeField] TireRotation _tirerl;
    [SerializeField] TireRotation _tirerr;
    [SerializeField] TireRotation _tirefr;

    void Start()
    {
        //  Rigidbody を取得
        _rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //  現在の速度をkm/hに変換し、UIの表示
        float currentSpeedkmh = currentSpeed * 3.6f;

        if (currentSpeedkmh >= 200) 
        {
            _acceleRation = 1f;
        }
        else if (currentSpeedkmh >= 180)
        {
            _acceleRation = 5f;
        }
        else if (currentSpeedkmh >= 150)
        {
            _acceleRation = 8f;
        }
        else
        {
            _acceleRation = 15f;
        }
        

        //  アクセルを踏んでいる間は加速する
        if (isAccelerating)
        {
            //  現在の速度が閾値を超えたら加速を緩やかにする
            float accele = currentSpeedkmh > accelerationThreshold
                ? _acceleRation * acceleDamping
                : _acceleRation;

                currentSpeed += accele * Time.deltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, 0, _maxSpeed / 3.6f);  //  最高速度(km/h)を超えないようにする

        }
        //  ブレーキを踏んでいる間は減速する
        else if (isBraking)
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
        //  操作をしていないとき、緩やかに減速する
        else
        {
            if (isRain)
            {
                if (currentSpeedkmh > 100f)
                {
                    currentSpeed -= drag * Time.deltaTime;
                }
                else if (currentSpeedkmh > 50f)
                {
                    currentSpeed -= drag * 1.25f * Time.deltaTime;
                }
                else if (currentSpeedkmh >= 0f)
                {
                    currentSpeed -= drag / 2 * Time.deltaTime;
                    currentSpeed = Mathf.Max(currentSpeed, 0);  //  速度がマイナスにならないようにする
                }
            }
            else
            {
                if (currentSpeedkmh > 100f)
                {
                    currentSpeed -= drag * 2f * Time.deltaTime;
                }
                else if (currentSpeedkmh > 50f)
                {
                    currentSpeed -= drag * 1.5f * Time.deltaTime;
                }
                else if (currentSpeedkmh >= 0f)
                {
                    currentSpeed -= drag * Time.deltaTime;
                    currentSpeed = Mathf.Max(currentSpeed, 0);  //  速度がマイナスにならないようにする
                }
            }
        }

        //  車の前方向に速度を適用
        _rb.velocity = transform.forward * currentSpeed;

        //  現在の速度をUIに表示
        speedText.text = "Speed: " + Mathf.RoundToInt(currentSpeedkmh) + " km/h";

        if (currentSpeedkmh == 0)
        {
            _tirefl.Brake();
            _tirerl.Brake();
            _tirerr.Brake();
            _tirefr.Brake();
        }
    }

    //  UIボタンから呼び出すメソッド
    public void StartAccelerating()
    {
        isAccelerating = true;
    }

    public void StopAccelerating()
    {
        isAccelerating = false;
    }

    public void StartBraking()
    {
        isBraking = true;
    }

    public void StopBraking()
    {
        isBraking = false;
    }
}
