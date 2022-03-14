using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SpawnerTerminal : MonoBehaviour
{
    public List<GameObject> Buttons;
    public List<Text> ButtonTexts;
    public List<GameObject> Prefabs;
    public GameObject SpawnLocation;

    Dictionary<int, EventTrigger.Entry> actions = new Dictionary<int, EventTrigger.Entry>();
    Dictionary<int, EventTrigger> triggers = new Dictionary<int, EventTrigger>();

    int pages;
    int page;

    void Start()
    {
        pages = Mathf.CeilToInt(Prefabs.Count / Buttons.Count);

        for (int i = 0; i < Prefabs.Count; i++)
        {
            actions.Add(i, new EventTrigger.Entry());
            actions[i].eventID = EventTriggerType.PointerClick;
            int x = i;
            actions[i].callback.AddListener((data) =>
            {
                GameObject go = Instantiate(Prefabs[x]);
                go.transform.position = SpawnLocation.transform.position;
                go.transform.rotation = SpawnLocation.transform.rotation;
            });
        }

        for (int i = 0; i < Buttons.Count; i++)
        {
            triggers[i] = Buttons[i].GetComponent<EventTrigger>();
        }

        UpdatePage();
    }

    // Update is called once per frame
    void UpdatePage()
    {
        for (int i = 0; i < Buttons.Count; i++)
        {
            if (i + Buttons.Count * page == Prefabs.Count)
            {
                for (int x = i; x < Buttons.Count; x++)
                {
                    Buttons[x].SetActive(false);
                }
                return;
            }

            Buttons[i].SetActive(true);

            if (triggers[i].triggers.Count > 0) triggers[i].triggers.RemoveAt(0);
            triggers[i].triggers.Add(actions[i + Buttons.Count * page]);

            ButtonTexts[i].text = Prefabs[i + Buttons.Count * page].name.Replace(' ', '\n');
        }
    }

    public void PreviousPage()
    {
        page--;
        if (page < 0)
        {
            page = pages;
        }
        UpdatePage();
    }

    public void NextPage()
    {
        page++;
        if (page > pages)
        {
            page = 0;
        }
        UpdatePage();
    }
}
