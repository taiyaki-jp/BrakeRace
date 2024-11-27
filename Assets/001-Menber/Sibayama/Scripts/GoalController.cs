using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalController : MonoBehaviour
{
    [SerializeField] GameObject _player;
    [SerializeField] private TextMeshProUGUI _distanceText; //  Textをいれる

    void Start()
    {
        //Instantiate(_goal, new Vector3(0, _player.transform.position.y, _goalPosition), Quaternion.identity);
    }
    /// <summary>
    /// ゴール判定
    /// </summary>
    /// <param name="other"></param>
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("ゴール!!");
            //SceneManager.LoadScene("シーン名");
        }
    }

    /// <summary>
    /// ゴールまでの距離
    /// </summary>
    private void Update()
    {
        float dis = Vector3.Distance(this.transform.position, _player.transform.position) - 1f;   //  Cube までとの間の距離算出(Cubeの中心座標から - 0.5引いてる)
        _distanceText.text = dis.ToString("F0");
    }
}
