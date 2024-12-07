using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    [SerializeField, Label("フェード速度")] float FadeSpeed=1;
    private GameObject FadeCanvas;
    private FadeAndLoad load;

    System.Action BeforAction=null;
    System.Action AfterAction=null;
    System.Action FinishAction = null;

    private void Start()
    {
        FadeCanvas = Fade_Singleton.canvas;

        load = new FadeAndLoad();
        load.image=Fade_Singleton.FadeImage;
        load.speed=FadeSpeed;

        if (Fade_Singleton.IsFirst)
        {
            FirstFade();
            Fade_Singleton.IsFirst = false;
        }
    }

    /*
    /// <summary>
    /// フェードを呼び出す関数1
    /// </summary>
    /// <param name="SceneName">遷移先のシーンの名前</param>
    public async void Fade(string SceneName)
    {
        FadeCanvas = Fade_Singleton.canvas;
        FadeCanvas.SetActive(true);
        
        await load.FadeIn();
        //BeforAction.Invoke();

        await SceneManager.LoadSceneAsync(SceneName);
        //AfterAction.Invoke();

        await load.FadeOut();
        //FinishAction.Invoke();

        FadeCanvas.SetActive(false);
    }
    /// <summary>
    /// フェードを呼び出す関数2
    /// </summary>
    /// <param name="SceneName">遷移先のシーンの名前</param>
    public async void FadeWhite(string SceneName)
    {
        FadeCanvas.SetActive(true);

        await load.FadeInWhite();
        //BeforAction.Invoke();

        await SceneManager.LoadSceneAsync(SceneName);
        //AfterAction.Invoke();

        await load.FadeOutWhite();
        //FinishAction.Invoke();

        FadeCanvas.SetActive(false);
    }*/

    /// <summary>
    /// 最初のフェード
    /// </summary>
    private async void FirstFade()
    {
        //AfterAction.Invoke();

        await load.FadeOut(Color.black);
        //FinishAction.Invoke();

        FadeCanvas.SetActive(false);
    }




    /// <summary>
    /// FillAmountフェードを呼び出す関数
    /// </summary>
    /// <param name="SceneName">遷移先のシーンの名前</param>
    /// <param name="StartMethod">Image.FillMethod</param>
    /// <param name="StartOrigin">StartMethodに対応するFillOriginEnum.csのEnum</param>
    /// <param name="EndMethod">Image.FillMethod</param>
    /// <param name="EndOrigin">EndMethodに対応するFillOriginEnum.csのEnum</param>
    public async void Fade<OriginEnum>(string SceneName,Image.FillMethod StartMethod, OriginEnum StartOrigin,Image.FillMethod EndMethod, OriginEnum EndOrigin)where OriginEnum : Enum
    {
        FadeCanvas = Fade_Singleton.canvas;
        FadeCanvas.SetActive(true);

        await load.FadeIn(StartMethod,StartOrigin);
        //BeforAction.Invoke();

        await SceneManager.LoadSceneAsync(SceneName);
        //AfterAction.Invoke();

        await load.FadeOut(EndMethod,EndOrigin);
        //FinishAction.Invoke();

        FadeCanvas.SetActive(false);
    }
    /// <summary>
    /// 透明度フェードを呼び出す関数
    /// </summary>
    /// <param name="SceneName">遷移先のシーンの名前</param>
    /// <param name="StartColor">どんな色でフェードを開始するか</param>
    /// <param name="EndColor">どんな色でフェードを終了するか()</param>
    public async void Fade(string SceneName,Color StartColor,Color EndColor)
    {
        FadeCanvas = Fade_Singleton.canvas;
        FadeCanvas.SetActive(true);

        await load.FadeIn(StartColor);
        //BeforAction.Invoke();

        await SceneManager.LoadSceneAsync(SceneName);
        //AfterAction.Invoke();

        await load.FadeOut(EndColor);
        //FinishAction.Invoke();

        FadeCanvas.SetActive(false);
    }
}
