using UnityEngine;
using UnityEngine.UI;

public class Fade_Singleton : MonoBehaviour
{
    private static Fade_Singleton Instance_closed;

    private static Image _image;
    private static GameObject _canvas;

    private void Awake()
    {
        //もしすでに生成されていれば
        if (Instance_closed != null && Instance_closed != this)
        {
            Destroy(this.gameObject);//自身を削除
        }

        //staticに自身を入れる
        Instance_closed = this;
        DontDestroyOnLoad(this.gameObject);//それをシーンを跨ぐ様にする

        //↓初期化処理
        _image = GetComponentInChildren<Image>();
        _canvas = this.gameObject;
    }
    private Fade_Singleton() { }//外部からの生成をブロック

    public static Image FadeImage
    {
        get { return _image; }
    }

    public static GameObject canvas
    {
        get { return _canvas; }
    }
}
