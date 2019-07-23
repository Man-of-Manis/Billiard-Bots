using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBasedSpawning : MonoBehaviour {

    //public GameObject manager;
    private PlayerTurn playerTurn;
    public GameObject[] receivers;

    public bool isActive;
    private bool activated;
    public int totalRoundsRequired;
    private int totalTurnsRequired;

    // Start is called before the first frame update
    void Start() {
        playerTurn = GetComponent<PlayerTurn>();
        updateReceivers();

        totalTurnsRequired = (totalRoundsRequired * playerTurn.playerAmount) - 1; // * num players, 4 is placeholder //-1 is due to GetComponent<PlayerTurn>() starting at 0 rather than 1
    }

    // Update is called once per frame
    void Update() {

        if (playerTurn.totalTurns >= totalTurnsRequired && !activated) {
            Debug.Log("toggle");
            isActive = !isActive;
            updateReceivers();
            activated = true;
        }
    }

    private void updateReceivers () {
        foreach (GameObject g in receivers) {
            g.SetActive(isActive);
        }
    }


}
