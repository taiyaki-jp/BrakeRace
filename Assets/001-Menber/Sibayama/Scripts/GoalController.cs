using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalController : MonoBehaviour
{
    
    [SerializeField] GameObject _player;
    [SerializeField] private TextMeshProUGUI _distanceText; //  Text�������

    private FadeManager _fadeManager;

    void Start()
    {
        //Instantiate(_goal, new Vector3(0, _player.transform.position.y, _goalPosition), Quaternion.identity);
        _fadeManager = GameObject.Find("FadeManager").GetComponent<FadeManager>();
    }
    /// <summary>
    /// �S�[������
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("�S�[��!!");
            _fadeManager.Fade("ResultScene-Crear");
            //SceneManager.LoadScene("�V�[����");
        }
    }

    /// <summary>
    /// �S�[���܂ł̋���
    /// </summary>
    private void Update()
    {
        float dis = Vector3.Distance(this.transform.position, _player.transform.position) - 1f;   //  Cube �܂łƂ̊Ԃ̋����Z�o(Cube�̒��S���W���� - 0.5�����Ă�)
        _distanceText.text = $"�c��{dis.ToString("F0")}m";
    }
}
