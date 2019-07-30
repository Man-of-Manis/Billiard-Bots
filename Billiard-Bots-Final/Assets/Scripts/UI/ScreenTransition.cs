using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ScreenTransition : MonoBehaviour
{
    public Material TransitionMaterial;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if(TransitionMaterial != null)
        {
            Graphics.Blit(source, destination, TransitionMaterial);
        }
    }
}
