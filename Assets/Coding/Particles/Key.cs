using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    bool Active;

    public GameObject Key1;

    public Renderer YiYang;
    public Material YiYangMaterial;
    public Renderer YaYin;
    public Material YaYinMaterial;
    public Renderer YiYin;
    public Material YiYinMaterial;
    public Renderer YaYang;
    public Material YaYangMaterial;

    public GameObject Key0;

    void PullEnergy(double capacity)
    {

    }

    void FixedUpdate()
    {
        if (Active)
        {
            YiYang.material = YiYinMaterial;
            YaYin.material = YaYangMaterial;
            YiYin.material = YiYangMaterial;
            YaYang.material = YaYinMaterial;
            Active = !Active;
            return;
        }

        YiYang.material = YiYangMaterial;
        YaYin.material = YaYinMaterial;
        YiYin.material = YiYinMaterial;
        YaYang.material = YaYangMaterial;
        Active = !Active;
    }

}
