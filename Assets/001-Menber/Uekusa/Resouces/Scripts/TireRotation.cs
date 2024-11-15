using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TireRotation : MonoBehaviour
{
    public  float accele          = 50f;  //  �������̑��x������
    public  float brake           = 50f;  //  �������̑��x������
    public  float maxRotation     = 500f; //  ��]�̍ő呬�x
    private float currentRotation = 0f;   //  ���݂̉�]���x

    private bool isAccele = false;        //  �A�N�Z�������m����t���O
    private bool isBrake = false;         //  �u���[�L�����m����t���O

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

        //  �^�C������]������
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
