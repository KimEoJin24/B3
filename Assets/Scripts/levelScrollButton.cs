using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class levelScrollButton : MonoBehaviour
{
    public ScrollRect scrollrect;
    private float scrollAmount = 360f;
    private int levelAmount = 2;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ScrollLeft()
    {
        if (scrollrect.content.localPosition.x < 0f)
        {
            scrollrect.content.localPosition += new Vector3(scrollAmount, 0, 0);
        }
    }

    public void ScrollRight()
    {
        if (scrollrect.content.localPosition.x > -(scrollAmount * (levelAmount - 1)))
        {
            scrollrect.content.localPosition -= new Vector3(scrollAmount, 0, 0);
        }
    }
}
