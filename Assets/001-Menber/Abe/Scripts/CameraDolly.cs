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
    [SerializeField, Label("テスト用-白線の位置")] private Transform test;
    [SerializeField, Label("テスト用-ドリー開始ボタン")] private Button _button;

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
    /// ウェイポイントの設定(とlookatも設定)
    /// </summary>
    /// <param name="wptransform">白線の位置</param>
    public void SetWayPoint(Transform wptransform)
    {
        CinemachineSmoothPath.Waypoint newwp = new CinemachineSmoothPath.Waypoint();
        newwp.position = new Vector3(wptransform.position.x-4,wptransform.position.y+3,wptransform.position.z);//信号の白線をいれる
        newwp.roll = 0;

        virtualCamera.LookAt = wptransform;//ついでにlookatも設定


        var PointList = path.m_Waypoints.ToList();//リスト形式に変換してウェイポイントを取る
        PointList.RemoveAt(2);//2番の要素を削除
        PointList.Add(newwp);//上で定義したウェイポイントを追加
        path.m_Waypoints = PointList.ToArray();//ウェイポイント配列に戻す

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
        await UniTask.DelayFrame(60);//カメラの動きにラグがあるから少し待機
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
        await UniTask.DelayFrame(60);//カメラの動きにラグがあるから少し待機
        virtualCamera.LookAt = this.transform;
    }
}
