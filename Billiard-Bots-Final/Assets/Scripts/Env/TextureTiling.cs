using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureTiling : MonoBehaviour
{
    private MeshRenderer rend;

    void Start()
    {
        TextureTile();
    }

    private void TextureTile()
    {
        rend = GetComponent<MeshRenderer>();
        rend.material.mainTextureScale = new Vector2(transform.lossyScale.x / 2, transform.lossyScale.y / 2);
    }
}
