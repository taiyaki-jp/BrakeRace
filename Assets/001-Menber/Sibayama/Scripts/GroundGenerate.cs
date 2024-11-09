using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerate : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    //  ��������Ground��Prefab���A�^�b�`����
    [SerializeField] private List<GameObject> _grounds = new List<GameObject>();

    int border = 20;
    float playerStartPosZ;  //  Player�̏������W z���W
    float playerNowPosZ;  //  Player�̌��݂� z���W

    [SerializeField] private List<GameObject> _groundsList = new List<GameObject>();

    
    // Start is called before the first frame update
    void Start()
    {
        
        //  Hierarcy�̒����疼�O��"Player"�̂��̂�T���ĕϐ��Ɋi�[
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
        playerNowPosZ = _player.transform.position.z;//Player�̌���x���W��ϐ�playerNowPosX�Ɋi�[
        float playerDistance = playerNowPosZ - playerStartPosZ;//Player�̈ړ�����(playerNowPosX��playerStartPosX�̍���)��ϐ�playerDistance�Ɋi�[
        if (playerDistance > border)
        {
            //�X�e�[�W����
            Debug.Log("�X�e�[�W����");
            var obj = Instantiate(_grounds[Random.Range(0, 3)], new Vector3(0, 0, _player.transform.position.z + 20), Quaternion.identity);//Player�̈�苗��������ɃX�e�[�W����(-5.5f�̓X�e�[�W�����̈ʒu�␳�̈�)
            _groundsList.Add(obj);  //  List�ǉ�
            playerDistance = 0;//playerDistance�̃��Z�b�g
            border = 10;//border�̍Đݒ�
            playerStartPosZ = playerNowPosZ;//playerStartPos�̍Đݒ�

            var Pos = _groundsList[0].transform.position;   // _ground�̍��W�擾
            float dis = Vector3.Distance(Pos, _player.transform.position);  //  player �� Pos �̍��W���r
            if(dis > 15)
            {
                //  �I�u�W�F�N�g�폜
                Destroy(_groundsList[0]);
                //  List�폜
                _groundsList.RemoveAt(0);
            }
            
        }
    }
}
