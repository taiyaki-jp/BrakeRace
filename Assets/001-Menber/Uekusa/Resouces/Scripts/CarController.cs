using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    //  �ō����x�ikm/h�j
    public float _maxSpeed = 200f;
    //  �����x
    public float _acceleRation = 5f;
    //  �����x
    public float brakeForce = 10f;

    //  ���݂̑��x (m/s)
    private float currentSpeed = 0f;
    //  Rigidbody
    private Rigidbody _rb;
    //  �{�^�����������Ƃ��̃t���O
    private bool isAccelerating = false;
    private bool isBraking = false;

    //  ���x�\���̃e�L�X�g
    public TextMeshProUGUI speedText;

    void Start()
    {
        //  Rigidbody ���擾
        _rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //  �A�N�Z���𓥂�ł���Ԃ͉�������
        if (isAccelerating)
        {
            currentSpeed += _acceleRation * Time.deltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, 0, _maxSpeed / 3.6f);  //  �ō����x(km/h)�𒴂��Ȃ��悤�ɂ���
        }
        //  �u���[�L�𓥂�ł���Ԃ͌�������
        else if (isBraking)
        {
            currentSpeed -= brakeForce * Time.deltaTime;
            currentSpeed = Mathf.Max(currentSpeed, 0);  //  ���x���}�C�i�X�ɂȂ�Ȃ��悤�ɂ���
        }

        //  �Ԃ̑O�����ɑ��x��K�p
        _rb.velocity = transform.forward * currentSpeed;

        //  ���݂̑��x��km/h�ɕϊ����AUI�̕\��
        float currentSpeedkmh = currentSpeed * 3.6f;
        speedText.text = "Speed: " + Mathf.RoundToInt(currentSpeedkmh) + " km/h";
    }

    //  UI�{�^������Ăяo�����\�b�h
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
