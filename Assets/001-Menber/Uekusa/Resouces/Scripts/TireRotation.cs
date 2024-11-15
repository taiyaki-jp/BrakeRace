using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TireRotation : MonoBehaviour
{
    public  float accele          = 50f;  //  加速時の速度増加量
    public  float brake           = 50f;  //  減速時の速度減少量
    public  float maxRotation     = 500f; //  回転の最大速度
    private float currentRotation = 0f;   //  現在の回転速度

    private bool isAccele = false;        //  アクセルを検知するフラグ
    private bool isBrake = false;         //  ブレーキを検知するフラグ

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isAccele)
        {
            currentRotation += accele * Time.deltaTime;
            currentRotation = Mathf.Clamp(currentRotation, 0f, maxRotation);
        }
        else if (isBrake)
        {
            currentRotation -= brake * Time.deltaTime;
            currentRotation = - Mathf.Max(currentRotation, 0f);
        }

        //  タイヤを回転させる
        transform.Rotate(Vector3.right, currentRotation * Time.deltaTime);
    }

    public void OnAcceleButtonDown(BaseEventData eventData)
    {
        isAccele = true;
    }

    public void OnAcceleButtonUp(BaseEventData eventData)
    {
        isAccele = false;
    }

    public void OnBrakeButtonDown(BaseEventData eventData)
    {
        isBrake = true;
    }

    public void OnBrakeButtonUp(BaseEventData eventData)
    {
        isBrake = false;
    }
}
