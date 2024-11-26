using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineGenerate : MonoBehaviour
{
    [SerializeField] float _generateRange1;
    [SerializeField] float _generateRange2;
    [SerializeField] float _generateRange3;

    private Vector3 _position = Vector3.zero;

    private void OnTriggerEnter(Collider other)
    {
        _position = other.transform.position;
        int rnd = Random.Range(1, 4);
        switch (rnd)
        {
            case 1:
                GameObject obj = (GameObject)Resources.Load("StopLine");
                GameObject instance = (GameObject)Instantiate(obj, new Vector3(_position.x + _generateRange1, 0.0f, 0.0f), Quaternion.identity);
                break;
            case 2:
                obj = (GameObject)Resources.Load("StopLine");
                instance = (GameObject)Instantiate(obj, new Vector3(_position.x + _generateRange2, 0.0f, 0.0f), Quaternion.identity);
                break;
            case 3:
                obj = (GameObject)Resources.Load("StopLine");
                instance = (GameObject)Instantiate(obj, new Vector3(_position.x + _generateRange3, 0.0f, 0.0f), Quaternion.identity);
                break;
        }
    }
}
