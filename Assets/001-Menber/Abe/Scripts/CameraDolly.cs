using Cinemachine;
using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CameraDolly : MonoBehaviour
{
    [SerializeField]private CinemachineSmoothPath path;
    [SerializeField]private CinemachineVirtualCamera virtualCamera;
    [SerializeField, Label("�e�X�g�p-�����̈ʒu")] private Transform test;
    [SerializeField, Label("�e�X�g�p-�h���[�J�n�{�^��")] private Button _button;

    private bool _doing=false;
    private CinemachineTrackedDolly dolly;
    // Start is called before the first frame update
    void Start()
    {
        dolly = virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
        _button.onClick.AddListener(Button);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }
    private async void Button()
    {
        if(_doing) return;
        _doing = true;
        SetWayPoint(test);
        await DoDolly();
        _ = DoDollyBack();
        _doing = false;
    }
    /// <summary>
    /// �E�F�C�|�C���g�̐ݒ�(��lookat���ݒ�)
    /// </summary>
    /// <param name="wptransform">�����̈ʒu</param>
    public void SetWayPoint(Transform wptransform)
    {
        CinemachineSmoothPath.Waypoint newwp = new CinemachineSmoothPath.Waypoint();
        newwp.position = new Vector3(wptransform.position.x-4,wptransform.position.y+3,wptransform.position.z);//�M���̔����������
        newwp.roll = 0;

        virtualCamera.LookAt = wptransform;//���ł�lookat���ݒ�


        var PointList = path.m_Waypoints.ToList();//���X�g�`���ɕϊ����ăE�F�C�|�C���g�����
        PointList.RemoveAt(2);//2�Ԃ̗v�f���폜
        PointList.Add(newwp);//��Œ�`�����E�F�C�|�C���g��ǉ�
        path.m_Waypoints = PointList.ToArray();//�E�F�C�|�C���g�z��ɖ߂�

    }

    public async UniTask DoDolly()
    {
        float pathpos = 0;
        while (dolly.m_PathPosition < 2)
        {
            pathpos += Time.deltaTime;
            dolly.m_PathPosition = Mathf.Min(pathpos,2);
            await UniTask.Yield();
        }
        await UniTask.DelayFrame(60);//�J�����̓����Ƀ��O�����邩�班���ҋ@
    }
    public async UniTask DoDollyBack()
    {
        float pathpos = dolly.m_PathPosition;
        while (dolly.m_PathPosition > 0)
        {
            pathpos -= Time.deltaTime;
            dolly.m_PathPosition = Mathf.Max(pathpos, 0);
            await UniTask.Yield();
        }
        await UniTask.DelayFrame(60);//�J�����̓����Ƀ��O�����邩�班���ҋ@
        virtualCamera.LookAt = this.transform;
    }
}
