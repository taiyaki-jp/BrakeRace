using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    float _speed = 5f;  //  移動スピード

    // Update is called once per frame
    void Update()
    {
        //  移動処理
        this.transform.position += new Vector3(0, 0, _speed * Time.deltaTime);
    }
}
