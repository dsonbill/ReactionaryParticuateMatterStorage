using UnityEngine;
using UnityEngine.UI;

public class UIScaleToElements : MonoBehaviour {
    private float spacing;
    private float padding;
    private float childPreferredHeight;
    private RectTransform rt;
    private VerticalLayoutGroup vlg;

    void Start ()
    {
        rt = GetComponent<RectTransform>();
        vlg =  GetComponent<VerticalLayoutGroup>();
	}
	
	void Update ()
    {
        if (transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (!transform.GetChild(i).gameObject.activeSelf) continue;
                childPreferredHeight = transform.GetChild(i).GetComponent<LayoutElement>().preferredHeight;
                padding = vlg.padding.top + vlg.padding.bottom;
                spacing = vlg.spacing;
                rt.sizeDelta = new Vector2(rt.rect.width, padding + (spacing * (transform.childCount - 1)) + (childPreferredHeight) * transform.childCount);
                break;
            }
        }
    }
}
