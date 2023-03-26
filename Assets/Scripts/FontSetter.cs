using UnityEngine;
using TMPro;

public class FontSetter : MonoBehaviour
{
    void Start()
    {
        TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
        text.font = Font.fontStatic.newFont;
        text.color = Font.fontStatic.color;
    }
}