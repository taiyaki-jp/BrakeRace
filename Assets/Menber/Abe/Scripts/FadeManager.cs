using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    [SerializeField, Label("フェード速度")] private float _fadeSpeed=1;
    private GameObject _fadeCanvas;
    private FadeAndLoad load;

    //System.Action BeforeAction=null;
    //System.Action AfterAction=null;
    //System.Action FinishAction = null;

    private void Start()
    {
        _fadeCanvas = Fade_Singleton.canvas;

        load = new FadeAndLoad
        {
            image = Fade_Singleton.FadeImage,
            speed = _fadeSpeed
        };

        if (!Fade_Singleton.IsFirst) return;
        _ = FirstFade();
        Fade_Singleton.IsFirst = false;
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
        //BeforeAction.Invoke();

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
        //BeforeAction.Invoke();

        await SceneManager.LoadSceneAsync(SceneName);
        //AfterAction.Invoke();

        await load.FadeOutWhite();
        //FinishAction.Invoke();

        FadeCanvas.SetActive(false);
    }*/

    /// <summary>
    /// 最初のフェード
    /// </summary>
    private async UniTask FirstFade()
    {
        //AfterAction.Invoke();
        load.SetColor(Color.black);
        await load.FadeOut();
        //FinishAction.Invoke();

        _fadeCanvas.SetActive(false);
    }



    /// <summary>
    /// FillAmountフェードを呼び出す関数
    /// </summary>
    /// <param name="sceneName">遷移先のシーンの名前</param>
    /// <param name="startOrigin">FillOriginEnum.csのEnum</param>
    /// <param name="endOrigin">FillOriginEnum.csのEnum</param>
    /// <param name="color">[省略可能]フェードの色 省略すると黒</param>
    public async UniTask Fade<TOriginEnum>(string sceneName,TOriginEnum startOrigin,TOriginEnum endOrigin,Color color=default)where TOriginEnum : Enum
    {
        _fadeCanvas = Fade_Singleton.canvas;
        _fadeCanvas.SetActive(true);

        if(color==default)load.SetColor(Color.black);
            else load.SetColor(color);

        await load.FadeIn(startOrigin);
        //BeforeAction.Invoke();

        await SceneManager.LoadSceneAsync(sceneName);
        //AfterAction.Invoke();

        await load.FadeOut(endOrigin);
        //FinishAction.Invoke();

        _fadeCanvas.SetActive(false);
    }
    /// <summary>
    /// 透明度フェードを呼び出す関数
    /// </summary>
    /// <param name="sceneName">遷移先のシーンの名前</param>
    /// <param name="color">どんな色でフェードするか</param>
    public async UniTask Fade(string sceneName,Color color)
    {
        _fadeCanvas = Fade_Singleton.canvas;
        _fadeCanvas.SetActive(true);

        if (color == default) load.SetColor(Color.black);
        else load.SetColor(color);

        await load.FadeIn();
        //BeforeAction.Invoke();

        await SceneManager.LoadSceneAsync(sceneName);
        //AfterAction.Invoke();

        await load.FadeOut();
        //FinishAction.Invoke();

        _fadeCanvas.SetActive(false);
    }
}
