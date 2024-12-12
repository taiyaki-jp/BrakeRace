using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        load.SetColor(Color.black);
        await load.FadeOut();
        //FinishAction.Invoke();

        FadeCanvas.SetActive(false);
    }



    /// <summary>
    /// FillAmountフェードを呼び出す関数
    /// </summary>
    /// <param name="SceneName">遷移先のシーンの名前</param>
    /// <param name="StartOrigin">FillOriginEnum.csのEnum</param>
    /// <param name="EndOrigin">FillOriginEnum.csのEnum</param>
    /// <param name="color">[省略可能]フェードの色 省略すると黒</param>
    public async void Fade<OriginEnum>(string SceneName,OriginEnum StartOrigin,OriginEnum EndOrigin,Color color=default)where OriginEnum : Enum
    {
        FadeCanvas = Fade_Singleton.canvas;
        FadeCanvas.SetActive(true);

        if(color==default)load.SetColor(Color.black); 
            else load.SetColor(color);

        await load.FadeIn(StartOrigin);
        //BeforAction.Invoke();

        await SceneManager.LoadSceneAsync(SceneName);
        //AfterAction.Invoke();

        await load.FadeOut(EndOrigin);
        //FinishAction.Invoke();

        FadeCanvas.SetActive(false);
    }
    /// <summary>
    /// 透明度フェードを呼び出す関数
    /// </summary>
    /// <param name="SceneName">遷移先のシーンの名前</param>
    /// <param name="Color">どんな色でフェードするか</param>
    public async void Fade(string SceneName,Color color)
    {
        FadeCanvas = Fade_Singleton.canvas;
        FadeCanvas.SetActive(true);

        if (color == default) load.SetColor(Color.black);
        else load.SetColor(color);

        await load.FadeIn();
        //BeforAction.Invoke();

        await SceneManager.LoadSceneAsync(SceneName);
        //AfterAction.Invoke();

        await load.FadeOut();
        //FinishAction.Invoke();

        FadeCanvas.SetActive(false);
    }
}
