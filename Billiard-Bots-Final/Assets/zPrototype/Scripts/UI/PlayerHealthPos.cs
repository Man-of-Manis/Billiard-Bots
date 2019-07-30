using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthPos : MonoBehaviour
{
    [System.Serializable]
    public class HealthPos
    {
        public Vector3 cornerPos;
        public Vector3 bottomPos;
    }

    [SerializeField] private RectTransform[] healthbars = new RectTransform[4];

    public HealthPos[] positions;

    [SerializeField] private RectTransform[] playerItemBar = new RectTransform[4];

    public bool healthPos = true;

    private void OnValidate()
    {
        Changing();
    }


    private void Changing()
    {
        for (int i = 0; i < healthbars.Length; i++)
        {
            healthbars[i].localPosition = healthPos ? positions[i].cornerPos : positions[i].bottomPos;
        }

        for(int i = 0; i < playerItemBar.Length; i++)
        {
            float y = playerItemBar[i].parent.parent.localPosition.y;
            playerItemBar[i].localPosition = new Vector3(0f, y < 0f ? 100f : -100f, 0f);
        }
    }
}
