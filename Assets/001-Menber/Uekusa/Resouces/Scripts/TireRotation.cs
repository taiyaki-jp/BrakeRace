using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class TireRotation : MonoBehaviour
{
    //  加速時の速度増加量
    public  float accele          = 50f;
    //  減速時の速度減少量
    public  float brake           = 50f;
    //  回転の最大速度
    public  float maxRotation     = 500f;
    //  現在の回転速度
    private float currentRotation = 0f;
    //  自然減速（摩擦）
    public  float friction        = 10f;
    //  
    private float rotateM         = 0f;

    //  アクセルを検知するフラグ
    private bool isAccele = false;
    //  ブレーキを検知するフラグ
    private bool isBrake = false;
    //  もう一度ブレーキを踏むまでfalseに戻らないフラグ
    private bool brakeflag = false;

    //  雨フラグ
    public bool isRain = false;
    //  タイヤ回転数テキスト
    public TextMeshProUGUI _tireText;

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
            if (isRain)
            {
                currentRotation -= brake / 1.5f * Time.deltaTime;
                currentRotation = Mathf.Max(currentRotation, 0f);
            }
            else
            {
                currentRotation -= brake * Time.deltaTime;
                currentRotation = Mathf.Max(currentRotation, 0f);
            }
        }
        else
        {
            if (brakeflag)
            {
                if (currentRotation > 2000f && currentRotation <= 3000f)
                {
                    rotateM = currentRotation;
                    brakeflag = false;
                }
                else if (currentRotation > 1000f && currentRotation < 2000f)
                {
                    rotateM = currentRotation;
                    brakeflag = false;
                }
                else if (currentRotation > 0f && currentRotation < 1000f)
                {
                    rotateM = currentRotation;
                    brakeflag = false;
                }
            }
            if (isRain)
            {
                if (rotateM > 2000f && rotateM <= 3000f)
                {
                    currentRotation -= friction / 1.5f * Time.deltaTime;
                    currentRotation = Mathf.Max(currentRotation, 0f);
                }
                if (rotateM > 1000f && rotateM < 2000f)
                {
                    currentRotation -= friction * 1.3f * Time.deltaTime;
                    currentRotation = Mathf.Max(currentRotation, 0f);
                }
                else if (rotateM > 0f && rotateM < 1000f)
                {
                    currentRotation -= friction * 2f * Time.deltaTime;
                    currentRotation = Mathf.Max(currentRotation, 0f);
                }
            }
            else
            {
                if (rotateM > 2000f && rotateM <= 3000f)
                {
                    currentRotation -= friction * Time.deltaTime;
                    currentRotation = Mathf.Max(currentRotation, 0f);
                }
                if (rotateM > 1000f && rotateM < 2000f)
                {
                    currentRotation -= friction * 2f * Time.deltaTime;
                    currentRotation = Mathf.Max(currentRotation, 0f);
                }
                else if (rotateM > 0f && rotateM < 1000f)
                {
                    currentRotation -= friction * 3f * Time.deltaTime;
                    currentRotation = Mathf.Max(currentRotation, 0f);
                }
            }
        }

        //  タイヤを回転させる
        transform.Rotate(Vector3.right, currentRotation * Time.deltaTime);

        //  回転数をUIに表示
        _tireText.text = "Tire : " + Mathf.RoundToInt(currentRotation);
    }

    public void OnAcceleButtonDown(BaseEventData eventData)
    {
        isAccele = true;
        brakeflag = false;
    }

    public void OnAcceleButtonUp(BaseEventData eventData)
    {
        isAccele = false;
        brakeflag = true;
    }

    public void OnBrakeButtonDown(BaseEventData eventData)
    {
        isBrake = true;
        brakeflag = false;
    }

    public void OnBrakeButtonUp(BaseEventData eventData)
    {
        isBrake = false;
        brakeflag = true;
    }

    public void Brake()
    {
        currentRotation = 0f;
    }
}
