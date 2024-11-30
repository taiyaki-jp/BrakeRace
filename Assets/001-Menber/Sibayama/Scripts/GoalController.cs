using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalController : MonoBehaviour
{
    
    [SerializeField] GameObject _player;
    [SerializeField] private TextMeshProUGUI _distanceText; //  Textをいれる

    private FadeManager _fadeManager;

    void Start()
    {
        //Instantiate(_goal, new Vector3(0, _player.transform.position.y, _goalPosition), Quaternion.identity);
        _fadeManager = GameObject.Find("FadeManager").GetComponent<FadeManager>();
    }
    /// <summary>
    /// ゴール判定
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("ゴール!!");
            _fadeManager.Fade("ResultScene-Crear");
            //SceneManager.LoadScene("シーン名");
        }
    }

    /// <summary>
    /// ゴールまでの距離
    /// </summary>
    private void Update()
    {
        float dis = Vector3.Distance(this.transform.position, _player.transform.position) - 1f;   //  Cube までとの間の距離算出(Cubeの中心座標から - 0.5引いてる)
        _distanceText.text = $"残り{dis.ToString("F0")}m";
    }
}
