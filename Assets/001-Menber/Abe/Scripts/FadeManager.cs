using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    [SerializeField, Label("フェード速度")] float FadeSpeed=100;
    private GameObject FadeCanvas;
    private FadeAndLoad2 load;

    System.Action BeforAction;
    System.Action AfterAction;
    System.Action FinishAction;

    private void Start()
    {
        FadeCanvas = Fade_Singleton.canvas;

        load = new FadeAndLoad2();
        load.image=Fade_Singleton.FadeImage;
        load.speed=FadeSpeed;

        if (Fade_Singleton.IsFirst)
        {
            FirstFade();
            Fade_Singleton.IsFirst = false;
        }
    }


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
    }

    private async void FirstFade()
    {
        //AfterAction.Invoke();

        await load.FadeOutBrack();
        //FinishAction.Invoke();

        FadeCanvas.SetActive(false);
    }
}
