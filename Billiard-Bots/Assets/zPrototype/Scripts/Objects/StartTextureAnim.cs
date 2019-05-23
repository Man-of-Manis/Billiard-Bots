using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class StartTextureAnim : MonoBehaviour {

    public AnimatedTiledTexture _animatedTileTexture;    // A reference to AnimatedTileTexture object

    private void OnMouseDown()
    {
        _animatedTileTexture.Play();
    }

    // Use this for initialization
    void Start () {
        if (_animatedTileTexture == null)
        {
            Debug.LogWarning("No animated tile texture script assigned!");
        }
        else
            _animatedTileTexture.RegisterCallback(AnimationFinished);
    }
    // This function will get called by the AnimatedTiledTexture script when the animation is completed if the EnableEvents option is set to true
    void AnimationFinished()
    {
        // The animation is finished
    }
    // Update is called once per frame
    void Update () {


    }
}
