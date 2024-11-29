using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerate : MonoBehaviour
{
    [SerializeField,Label("プレイヤー")] private GameObject _player;
    //  生成するGroundのPrefabをアタッチする
    [SerializeField,Label("地面のプレハブ")] private List<GameObject> _grounds = new List<GameObject>();
    [SerializeField, Label("現在の地面")] private List<GameObject> _groundsList = new List<GameObject>();

    [SerializeField,Label("地面が何個同時に存在するか")]private int _groundCount = 10;
    [SerializeField, Label("地面の長さ")] private float _groundLength;

    float playerBeforePosZ; //  Playerの少し前のz座標
    float playerAfterPosZ;　//  Playerの今のz座標
    float playerNowPosZ=0;  //  Playerがどれだけ移動したかz座標


    
    // Start is called before the first frame update
    void Start()
    {
        
        //  Hierarcyの中から名前が"Player"のものを探して変数に格納
        _player = GameObject.Find("Player");    

        playerBeforePosZ = _player.transform.position.z;

        for (int i = 0; i < _groundCount-1; i++)
        {
            Generate();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerAfterPosZ = _player.transform.position.z;
        playerNowPosZ += (playerAfterPosZ-playerBeforePosZ);
        GenerateGround();
        playerBeforePosZ = playerAfterPosZ;

    }

    void GenerateGround()
    {
        if (playerNowPosZ >= _groundLength)
        {
            playerNowPosZ -= _groundLength;
            Generate();
            DestoryGround();
        }
    }

    private void Generate()
    {
        //一番新しく生成した道路の先に生成(マネージャーの子オブジェクトとして)
        var obj = Instantiate(_grounds[Random.Range(0, 3)], new Vector3(0, 0, _groundsList[^1].transform.position.z + _groundLength), Quaternion.identity, this.transform);
        _groundsList.Add(obj);  //  List追加
    }

    private void DestoryGround()
    {
        //  オブジェクト削除
        Destroy(_groundsList[0]);
        //  List削除
        _groundsList.RemoveAt(0);
    }
}
