using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomizeType : MonoBehaviour {

    public int type = 0;
    public Renderer mr;

    public GameObject glow;
    public GameObject flat;

    public void ChangeType(int i)
    {
        type = i;
        if(i == 2)
        {
            flat.SetActive(false);
            glow.SetActive(true);
        }
        else
        {
            flat.SetActive(true);
            glow.SetActive(false);
        }

    }

    public void ChangeColor(Material mat)
    {
        if(type != 2)
        {
            var materials = mr.sharedMaterials;
            materials[type] = Resources.Load("Materials/EndesgaPallette/" + mat.name.ToString(), typeof(Material)) as Material;
            mr.sharedMaterials = materials;
        }

        else
        {
            var materials = mr.sharedMaterials;
            materials[type] = Resources.Load("Materials/GlowPallette/" + mat.name.ToString(), typeof(Material)) as Material;
            mr.sharedMaterials = materials;
        }
    }

    public void Start()
    {
        flat.SetActive(true);
        glow.SetActive(false);
    }
}
