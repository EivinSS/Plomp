using TMPro;
using UnityEngine;

public class Font : MonoBehaviour
{
    public TMP_FontAsset newFont;
    public Color color;
    public static Font fontStatic;
    void Awake()
    {
        if (fontStatic != null)
        {
            Destroy(gameObject);
            return;
        }
        fontStatic = this;
        DontDestroyOnLoad(gameObject);
    }
}
