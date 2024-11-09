using UnityEngine;

public class CarController : MonoBehaviour
{
    //  最高速度（メートル毎秒）
    public float _maxSpeed = 20f;
    //  加速度
    public float _acceleRation = 5f;
    //  減速度
    public float brakeForce = 10f;


    //  現在の速度
    private float currentSpeed = 0;
    //  Rigidbody
    private Rigidbody _rb;
    //  ボタンを押したときのフラグ
    private bool isAccelerating = false;
    private bool isBraking = false;

    void Start()
    {
        //  Rigidbody を取得
        _rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //  アクセルを踏んでいる間は加速する
        if (isAccelerating)
        {
            currentSpeed += _acceleRation * Time.deltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, 0, _maxSpeed);  //  最高速度を超えないようにする
        }
        //  ブレーキを踏んでいる間は減速する
        else if (isBraking)
        {
            currentSpeed -= brakeForce * Time.deltaTime;
            currentSpeed = Mathf.Max(currentSpeed, 0);  //  速度がマイナスにならないようにする
        }

        //  車の前方向に速度を適用
        _rb.velocity = transform.forward * currentSpeed;
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
