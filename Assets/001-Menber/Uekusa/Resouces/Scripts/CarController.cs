using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    //  最高速度（km/h）
    public float _maxSpeed = 200f;
    //  加速度
    public float _acceleRation = 5f;
    //  減速度
    public float brakeForce = 10f;

    //  現在の速度 (m/s)
    private float currentSpeed = 0f;
    //  Rigidbody
    private Rigidbody _rb;
    //  ボタンを押したときのフラグ
    private bool isAccelerating = false;
    private bool isBraking = false;

    //  速度表示のテキスト
    public TextMeshProUGUI speedText;

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
            currentSpeed = Mathf.Clamp(currentSpeed, 0, _maxSpeed / 3.6f);  //  最高速度(km/h)を超えないようにする
        }
        //  ブレーキを踏んでいる間は減速する
        else if (isBraking)
        {
            currentSpeed -= brakeForce * Time.deltaTime;
            currentSpeed = Mathf.Max(currentSpeed, 0);  //  速度がマイナスにならないようにする
        }

        //  車の前方向に速度を適用
        _rb.velocity = transform.forward * currentSpeed;

        //  現在の速度をkm/hに変換し、UIの表示
        float currentSpeedkmh = currentSpeed * 3.6f;
        speedText.text = "Speed: " + Mathf.RoundToInt(currentSpeedkmh) + " km/h";
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
