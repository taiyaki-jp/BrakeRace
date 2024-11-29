using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineGenerate : MonoBehaviour
{
    [SerializeField] GameObject obj;
    [SerializeField] float _generateRange1;
    [SerializeField] float _generateRange2;
    [SerializeField] float _generateRange3;

    private Vector3 _position = Vector3.zero;
    GameObject instance;
    public GameObject Line=>instance;

    private void OnTriggerEnter(Collider other)
    {
        _position = other.transform.position;
        int rnd = Random.Range(1, 4);
        switch (rnd)
        {
            case 1:
                instance = Instantiate(obj, new Vector3(0.0f, 0.0f, _position.z + _generateRange1), Quaternion.Euler(0.0f,90.0f,0.0f));
                break;
            case 2:
                instance = Instantiate(obj, new Vector3(0.0f, 0.0f, _position.z + _generateRange2), Quaternion.Euler(0.0f, 90.0f, 0.0f));
                break;
            case 3:
                instance = Instantiate(obj, new Vector3(0.0f, 0.0f, _position.z + _generateRange3), Quaternion.Euler(0.0f, 90.0f, 0.0f));
                break;
        }
    }
}
