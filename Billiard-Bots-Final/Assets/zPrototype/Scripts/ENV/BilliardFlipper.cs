using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BilliardFlipper : MonoBehaviour
{
    public enum PlayerInputNum { Player_1, Player_2, Player_3, Player_4};

    public PlayerInputNum num;

    public bool clockwise = true;

    public float rotMax = 170f;

    private int playerNum;

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
            case PlayerInputNum.Player_1: playerNum = 1;
                break;
            case PlayerInputNum.Player_2: playerNum = 2;
                break;
            case PlayerInputNum.Player_3: playerNum = 3;
                break;
            case PlayerInputNum.Player_4: playerNum = 4;
                break;
            default: playerNum = 1;
                break;
        }

        GetComponentInChildren<FlipperHit>().playerNum = (int)num;

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
        //flipperRotation = Input.GetAxis(playerNum + "_L_Trigger") * -rotMax + Input.GetAxis(playerNum + "_R_Trigger") * rotMax;

        flipperRotation = PlayerInput.Instance.players[playerNum].lTrigger * -rotMax + PlayerInput.Instance.players[playerNum].rTrigger * rotMax;
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
                Color col = d.transform.Find("P_BilliardBot").Find("BilliardBot_Mesh").GetComponent<SkinnedMeshRenderer>().materials[0].color;
                transform.GetChild(0).GetComponent<MeshRenderer>().material.color = col;
                transform.GetChild(0).GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", col * 0.25f);
            }
        }
    }
}
