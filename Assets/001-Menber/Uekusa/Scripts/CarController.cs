using UnityEngine;

public class CarController : MonoBehaviour
{
    //  �ō����x�i���[�g�����b�j
    public float _maxSpeed = 20f;
    //  �����x
    public float _acceleRation = 5f;
    //  �����x
    public float brakeForce = 10f;


    //  ���݂̑��x
    private float currentSpeed = 0;
    //  Rigidbody
    private Rigidbody _rb;
    //  �{�^�����������Ƃ��̃t���O
    private bool isAccelerating = false;
    private bool isBraking = false;

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
            currentSpeed = Mathf.Clamp(currentSpeed, 0, _maxSpeed);  //  �ō����x�𒴂��Ȃ��悤�ɂ���
        }
        //  �u���[�L�𓥂�ł���Ԃ͌�������
        else if (isBraking)
        {
            currentSpeed -= brakeForce * Time.deltaTime;
            currentSpeed = Mathf.Max(currentSpeed, 0);  //  ���x���}�C�i�X�ɂȂ�Ȃ��悤�ɂ���
        }

        //  �Ԃ̑O�����ɑ��x��K�p
        _rb.velocity = transform.forward * currentSpeed;
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
