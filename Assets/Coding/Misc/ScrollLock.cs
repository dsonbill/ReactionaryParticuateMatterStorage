using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ScrollLock : MonoBehaviour {

    private Scrollbar bar;
    public GameObject scrollBar;

    public float speed = 0.1f;

    bool Lock = true;

	void Start ()
    {
        bar = scrollBar.GetComponent<Scrollbar>();
	}

    void Update()
    {
        if (Lock && bar.value > 0)
        {
            if (bar.value < speed)
            {
                bar.value -= bar.value;
            }

            bar.value -= speed;
        }
        else if (Lock)
        {
            Lock = false;
        }
    }

    public void SetPosition()
    {
        Lock = true;
    }
}
