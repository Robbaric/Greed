using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customize : MonoBehaviour {

    public MeshRenderer renderer;
    public Material mat;

    CustomizeType ct;
    
    public void ChangeColor()
    {
        renderer.materials[ct.type] = mat;
    }
}
