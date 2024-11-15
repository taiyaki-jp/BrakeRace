using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    [SerializeField, Label("�t�F�[�h���x")] float FadeSpeed=100;
    private TestSceneLoad load;
    System.Action BeforAction;
    System.Action AfterAction;
    System.Action FinishAction;

    private void Start()
    {
        load = new TestSceneLoad();
        load.image=GetComponent<Image>();
        load.speed=FadeSpeed;
    }


    /// <summary>
    /// �t�F�[�h���Ăяo���֐�
    /// </summary>
    /// <typeparam name="T">FillOriginEnum</typeparam>
    /// <param name="Startmethod">�t�F�[�h�C�����鎞�̓���</param>
    /// <param name="StartOrigin">�t�F�[�h�C���̓����̌���</param>
    /// <param name="Endmethod">�t�F�[�h�A�E�g���鎞�̓���</param>
    /// <param name="EndOrigin">�t�F�[�h�A�E�g�̓����̌���</param>
    /// <param name="SceneName">�J�ڐ�̃V�[���̖��O</param>
    public async void Fade<T>(Image.FillMethod Startmethod,T StartOrigin,Image.FillMethod Endmethod,T EndOrigin,string SceneName)where T :  Enum
    {
        await load.FadeIn(Startmethod, StartOrigin);
        BeforAction.Invoke();
        await SceneManager.LoadSceneAsync(SceneName);
        AfterAction.Invoke();
        await load.FadeOut(Endmethod, EndOrigin);
        FinishAction.Invoke();
    }

}
