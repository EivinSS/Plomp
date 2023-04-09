using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class AspectRatioFitter : MonoBehaviour
{
    [SerializeField] RectTransform parent;
    [SerializeField] Sprite bgImage;

    void Start()
    {
        RectTransform goRectTransform = GetComponent<RectTransform>();

        float pWidth = parent.rect.width;
        float pHeight = parent.rect.height;

        float arParent = pWidth / pHeight;

        float bgImageWidth = bgImage.rect.width;
        float bgImageHeight = bgImage.rect.height;

        float arImage = bgImageWidth / bgImageHeight;

        //put width equal each other
        if(arParent >= arImage)
        {
            goRectTransform.sizeDelta = new Vector2(pWidth+2, (pWidth / arImage)+2);
        }
        //put height equal each other
        else if(arParent < arImage)
        {
            goRectTransform.sizeDelta = new Vector2((pHeight * arImage)+2, pHeight+2);
        }
    }
}