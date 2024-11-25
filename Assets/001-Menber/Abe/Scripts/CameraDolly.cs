using Cinemachine;
using Cysharp.Threading.Tasks;
using System.Linq;
using UnityEngine;

public class CameraDolly : MonoBehaviour
{
    [SerializeField]private CinemachineSmoothPath path;
    [SerializeField]private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private Transform test;

    private CinemachineTrackedDolly dolly; 
    // Start is called before the first frame update
    async void Start()
    {
        dolly=virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
        SetWayPoint(test);
        await DoDolly();
        _ = DoDollyBack();
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
