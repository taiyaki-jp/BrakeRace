using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    //  �ō����x�ikm/h�j
    public float _maxSpeed = 200f;
    //  �����x�i�����l�j
    public float _acceleRation = 5f;
    //  �����x�i�u���[�L�j
    public float brakeForce = 10f;
    //  ���������x�i���삵�Ă��Ȃ��Ƃ��j
    public float drag = 2f;
    //  �������ɂ₩�ɂȂ�臒l�ikm/h�j
    public  float accelerationThreshold = 100f;
    //  ����������
    public float acceleDamping = 0.15f;

    //  ���݂̑��x (m/s)
    private  float currentSpeed = 0f;
    //  Rigidbody
    private Rigidbody _rb;
    //  �{�^�����������Ƃ��̃t���O
    private bool isAccelerating = false;
    private bool isBraking = false;

    //  �J�t���O
    public bool isRain = false;

    //  ���x�\���̃e�L�X�g
    public TextMeshProUGUI speedText;

    [SerializeField] TireRotation _tirefl;
    [SerializeField] TireRotation _tirerl;
    [SerializeField] TireRotation _tirerr;
    [SerializeField] TireRotation _tirefr;

    void Start()
    {
        //  Rigidbody ���擾
        _rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //  ���݂̑��x��km/h�ɕϊ����AUI�̕\��
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
        

        //  �A�N�Z���𓥂�ł���Ԃ͉�������
        if (isAccelerating)
        {
            //  ���݂̑��x��臒l�𒴂�����������ɂ₩�ɂ���
            float accele = currentSpeedkmh > accelerationThreshold
                ? _acceleRation * acceleDamping
                : _acceleRation;

                currentSpeed += accele * Time.deltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, 0, _maxSpeed / 3.6f);  //  �ō����x(km/h)�𒴂��Ȃ��悤�ɂ���

        }
        //  �u���[�L�𓥂�ł���Ԃ͌�������
        else if (isBraking)
        {
            if (isRain)
            {
                currentSpeed -= brakeForce / 2f * Time.deltaTime;
                currentSpeed = Mathf.Max(currentSpeed, 0);  //  ���x���}�C�i�X�ɂȂ�Ȃ��悤�ɂ���
            }
            else
            {
                currentSpeed -= brakeForce * Time.deltaTime;
                currentSpeed = Mathf.Max(currentSpeed, 0);  //  ���x���}�C�i�X�ɂȂ�Ȃ��悤�ɂ���
            }
        }
        //  ��������Ă��Ȃ��Ƃ��A�ɂ₩�Ɍ�������
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
                    currentSpeed = Mathf.Max(currentSpeed, 0);  //  ���x���}�C�i�X�ɂȂ�Ȃ��悤�ɂ���
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
                    currentSpeed = Mathf.Max(currentSpeed, 0);  //  ���x���}�C�i�X�ɂȂ�Ȃ��悤�ɂ���
                }
            }
        }

        //  �Ԃ̑O�����ɑ��x��K�p
        _rb.velocity = transform.forward * currentSpeed;

        //  ���݂̑��x��UI�ɕ\��
        speedText.text = "Speed: " + Mathf.RoundToInt(currentSpeedkmh) + " km/h";

        if (currentSpeedkmh == 0)
        {
            _tirefl.Brake();
            _tirerl.Brake();
            _tirerr.Brake();
            _tirefr.Brake();
        }
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
