using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGameMenu : MonoBehaviour
{
    public string[] Tabs = { "Winner", "Stats", "Best Stats" };

    public int page = 0;

    public TextMeshProUGUI[] tabNames = new TextMeshProUGUI[2];

    public Transform pagesParent;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Tabs.Length; i++)
        {
            pagesParent.GetChild(i).gameObject.SetActive(i == page);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("P1_L_Bumper"))
        {
            page = page == 0 ? 2 : page - 1;
            SetTabs(Tabs[page == 0 ? 2 : page - 1], Tabs[page == 2 ? 0 : page + 1]);

            for(int i = 0; i < Tabs.Length; i++)
            {
                pagesParent.GetChild(i).gameObject.SetActive(i == page);
            }
        }

        else if(Input.GetButtonDown("P1_R_Bumper"))
        {
            page = page == 2 ? 0 : page + 1;
            SetTabs(Tabs[page == 0 ? 2 : page - 1], Tabs[page == 2 ? 0 : page + 1]);

            for (int i = 0; i < Tabs.Length; i++)
            {
                pagesParent.GetChild(i).gameObject.SetActive(i == page);
            }
        }

        else if(Input.GetButtonDown("P1_B_Button"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }

    void SetTabs(string left, string right)
    {
        tabNames[0].text = left;
        tabNames[1].text = right;
    }
}
