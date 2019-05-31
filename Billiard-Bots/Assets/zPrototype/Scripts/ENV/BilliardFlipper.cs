using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BilliardFlipper : MonoBehaviour
{
    public enum PlayerInputNum { Player_1, Player_2, Player_3, Player_4};

    public PlayerInputNum num;

    public bool clockwise = true;

    public float rotMax = 170f;

    private string playerNum;

    public float flipperRotation;

    private HingeJoint hinge;

    [SerializeField] private float hitStrength = 100000f;

    [SerializeField] private float flipperDamper = 25f;

    // Start is called before the first frame update
    void Start()
    {
        hinge = GetComponentInChildren<HingeJoint>();

        hinge.useSpring = true;

        JointLimits limits = hinge.limits;

        limits.min = -rotMax;

        limits.max = rotMax;

        hinge.limits = limits;

        switch (num)
        {
            case PlayerInputNum.Player_1: playerNum = "P1";
                break;
            case PlayerInputNum.Player_2: playerNum = "P2";
                break;
            case PlayerInputNum.Player_3: playerNum = "P3";
                break;
            case PlayerInputNum.Player_4: playerNum = "P4";
                break;
            default: playerNum = "P1";
                break;
        }

        FlipperColor();
    }

    // Update is called once per frame
    void Update()
    {
        FlipperInput();

        FlipperRotation();
    }

    void FlipperInput()
    {
        flipperRotation = Input.GetAxis(playerNum + "_L_Trigger") * -rotMax + Input.GetAxis(playerNum + "_R_Trigger") * rotMax;
    }

    void FlipperRotation()
    {
        //transform.localRotation = Quaternion.Euler(0f, flipperRotation, 0f);

        JointSpring spring = new JointSpring();

        spring.targetPosition = flipperRotation;

        spring.spring = hitStrength;

        spring.damper = flipperDamper;

        hinge.spring = spring;

        hinge.useLimits = true;
    }

    void FlipperColor()
    {
        PlayerIdentifier[] ids = FindObjectsOfType<PlayerIdentifier>();

        foreach(PlayerIdentifier d in ids)
        {
            if(((int)num).Equals((int)d.player))
            {
                Color col = d.GetComponent<MeshRenderer>().material.color;
                transform.GetChild(0).GetComponent<MeshRenderer>().material.color = col;
                transform.GetChild(0).GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", col * 0.25f);
            }
        }
    }
}
