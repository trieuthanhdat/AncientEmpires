

#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class EasyAdManagerUltimate : MonoBehaviour
{
    [MenuItem("Unity源码/淘宝店")]
    public static void AdMenuTaobao()
    {
        string url = "https://shop35938299.taobao.com/";
        Application.OpenURL(url);
    }

    [MenuItem("Unity源码/闲鱼店")]
    public static void AdMenuGoofish()
    {
        string url = "https://www.goofish.com/personal?userId=Qt9wn22aKj%2FnV82TP9hAbA%3D%3D";
        Application.OpenURL(url);
    }
}
#endif