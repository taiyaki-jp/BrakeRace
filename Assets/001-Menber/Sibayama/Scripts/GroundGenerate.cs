using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerate : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    //  生成するGroundのPrefabをアタッチする
    [SerializeField] private List<GameObject> _grounds = new List<GameObject>();

    int border = 20;
    float playerStartPosZ;  //  Playerの初期座標 z座標
    float playerNowPosZ;  //  Playerの現在の z座標

    [SerializeField] private List<GameObject> _groundsList = new List<GameObject>();

    
    // Start is called before the first frame update
    void Start()
    {
        
        //  Hierarcyの中から名前が"Player"のものを探して変数に格納
        _player = GameObject.Find("Player");    

        playerStartPosZ = _player.transform.position.z; 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GenerateGround();
    }

    void GenerateGround()
    {
        playerNowPosZ = _player.transform.position.z;//Playerの現在x座標を変数playerNowPosXに格納
        float playerDistance = playerNowPosZ - playerStartPosZ;//Playerの移動距離(playerNowPosXとplayerStartPosXの差分)を変数playerDistanceに格納
        if (playerDistance > border)
        {
            //ステージ生成
            Debug.Log("ステージ生成");
            var obj = Instantiate(_grounds[Random.Range(0, 3)], new Vector3(0, 0, _player.transform.position.z + 20), Quaternion.identity);//Playerの一定距離だけ先にステージ生成(-5.5fはステージ生成の位置補正の為)
            _groundsList.Add(obj);  //  List追加
            playerDistance = 0;//playerDistanceのリセット
            border = 10;//borderの再設定
            playerStartPosZ = playerNowPosZ;//playerStartPosの再設定

            var Pos = _groundsList[0].transform.position;   // _groundの座標取得
            float dis = Vector3.Distance(Pos, _player.transform.position);  //  player と Pos の座標を比較
            if(dis > 15)
            {
                //  オブジェクト削除
                Destroy(_groundsList[0]);
                //  List削除
                _groundsList.RemoveAt(0);
            }
            
        }
    }
}
