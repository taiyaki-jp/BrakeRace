using NaughtyAttributes;
using TMPro;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField,Label("�ō����x�ikm/h�j")] private float _maxSpeed = 200f;
    [SerializeField,Label("�����x�i�����l�j")] private float _acceleRation = 5f;
    public float a 
    { 
        get { return _acceleRation; }
        set { _acceleRation = value; }
    }
    [SerializeField,Label("�����x�i�u���[�L�j")] private float brakeForce = 10f;
    [SerializeField,Label("���������x�i���삵�Ă��Ȃ��Ƃ��j")] private float drag = 2f;
    [SerializeField,Label("�������ɂ₩�ɂȂ�^�C�~���O�ikm/h�j")] private float accelerationThreshold = 100f;
    [SerializeField,Label("����������")] private float acceleDamping = 0.15f;

    //  ���݂̑��x (m/s)
    private float currentSpeed = 0f;
    //  Rigidbody
    private Rigidbody _rb;
    //  �{�^�����������Ƃ��̃t���O
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

    //  �J�t���O
    private bool isRain = false;
    public bool IsRain
    {
        set { isRain = value; }
    }

    //  ���x�\���̃e�L�X�g
    [SerializeField,Label("���x��text")] private TextMeshProUGUI speedText;

    [SerializeField,Label("�^�C��")] TireRotation[] _tires=new TireRotation[4];

    void Start()
    {
        //  Rigidbody ���擾
        _rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //  ���݂̑��x��km/h�ɕϊ����AUI�̕\��
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
        
        //  �A�N�Z���𓥂�ł���Ԃ͉�������
        if (isAccelerating)
        {
            Accele(currentSpeedkmh);
        }
        //  �u���[�L�𓥂�ł���Ԃ͌�������
        if (isBraking)
        {
            Brake();
        }
        //  ��������Ă��Ȃ��Ƃ��A�ɂ₩�Ɍ�������
        if( ! isAccelerating && ! isBraking)
        {
            kansei(currentSpeedkmh);
        }

        //  �Ԃ̑O�����ɑ��x��K�p
        _rb.velocity = transform.forward * currentSpeed;

        //  ���݂̑��x��UI�ɕ\��
        speedText.text = "Speed: " + Mathf.RoundToInt(currentSpeedkmh) + " km/h";

        //�^�C���̉�]
        if (currentSpeedkmh == 0)
        {
            foreach (var item in _tires)//forEach�Ŕz�����
            {
                item.Brake();
            }
        }
    }
    private void Accele(float currentSpeedkmh)
    {
        //  ���݂̑��x��臒l�𒴂�����������ɂ₩�ɂ���
        float accele = currentSpeedkmh > accelerationThreshold
            ? _acceleRation * acceleDamping
            : _acceleRation;

        currentSpeed += accele * Time.deltaTime;
        currentSpeed = Mathf.Clamp(currentSpeed, 0, _maxSpeed / 3.6f);  //  �ō����x(km/h)�𒴂��Ȃ��悤�ɂ���
    }

    private void Brake()
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

    private void kansei(float currentSpeedkmh)
    {
        float gensokuritu=Mathf.Min(1, 2 - currentSpeed / 100);
        if (isRain)
        {
            currentSpeed -= drag * (gensokuritu/2) * Time.deltaTime;
            currentSpeed = Mathf.Max(currentSpeed, 0);  //  ���x���}�C�i�X�ɂȂ�Ȃ��悤�ɂ���
        }
        else
        {
                currentSpeed -= drag  *gensokuritu* Time.deltaTime;
                currentSpeed = Mathf.Max(currentSpeed, 0);  //  ���x���}�C�i�X�ɂȂ�Ȃ��悤�ɂ���
        }
    }
}
