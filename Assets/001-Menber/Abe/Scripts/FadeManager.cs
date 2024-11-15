using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    [SerializeField, Label("フェード速度")] float FadeSpeed=100;
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
    /// フェードを呼び出す関数
    /// </summary>
    /// <typeparam name="T">FillOriginEnum</typeparam>
    /// <param name="Startmethod">フェードインする時の動き</param>
    /// <param name="StartOrigin">フェードインの動きの向き</param>
    /// <param name="Endmethod">フェードアウトする時の動き</param>
    /// <param name="EndOrigin">フェードアウトの動きの向き</param>
    /// <param name="SceneName">遷移先のシーンの名前</param>
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
