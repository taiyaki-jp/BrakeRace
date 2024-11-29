using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerate : MonoBehaviour
{
    [SerializeField,Label("�v���C���[")] private GameObject _player;
    //  ��������Ground��Prefab���A�^�b�`����
    [SerializeField,Label("�n�ʂ̃v���n�u")] private List<GameObject> _grounds = new List<GameObject>();
    [SerializeField, Label("���݂̒n��")] private List<GameObject> _groundsList = new List<GameObject>();

    [SerializeField,Label("�n�ʂ��������ɑ��݂��邩")]private int _groundCount = 10;
    [SerializeField, Label("�n�ʂ̒���")] private float _groundLength;

    float playerBeforePosZ; //  Player�̏����O��z���W
    float playerAfterPosZ;�@//  Player�̍���z���W
    float playerNowPosZ=0;  //  Player���ǂꂾ���ړ�������z���W


    
    // Start is called before the first frame update
    void Start()
    {
        
        //  Hierarcy�̒����疼�O��"Player"�̂��̂�T���ĕϐ��Ɋi�[
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
        //��ԐV���������������H�̐�ɐ���(�}�l�[�W���[�̎q�I�u�W�F�N�g�Ƃ���)
        var obj = Instantiate(_grounds[Random.Range(0, 3)], new Vector3(0, 0, _groundsList[^1].transform.position.z + _groundLength), Quaternion.identity, this.transform);
        _groundsList.Add(obj);  //  List�ǉ�
    }

    private void DestoryGround()
    {
        //  �I�u�W�F�N�g�폜
        Destroy(_groundsList[0]);
        //  List�폜
        _groundsList.RemoveAt(0);
    }
}
