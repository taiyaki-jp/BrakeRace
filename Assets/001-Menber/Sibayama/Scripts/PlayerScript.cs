using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    float _speed = 5f;  //  �ړ��X�s�[�h

    // Update is called once per frame
    void Update()
    {
        //  �ړ�����
        this.transform.position += new Vector3(0, 0, _speed * Time.deltaTime);
    }
}
