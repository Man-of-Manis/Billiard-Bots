﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Screenshot : MonoBehaviour
{
    private string folderName = "Screenshots";

    private string folderDirect;

    private void Start()
    {
        string direct = Directory.GetCurrentDirectory();

        DirectoryInfo di = new DirectoryInfo(direct);

        DirCount(di);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.JoystickButton8))
        {
            DirectoryInfo fd = new DirectoryInfo(folderDirect + folderName);

            ScreenCapture.CaptureScreenshot(folderDirect + "Screenshots/BB_Screenshot" + FileCount(fd) + ".png", 1);
        }
    }

    public static long FileCount(DirectoryInfo d)
    {
        long i = 0;

        FileInfo[] fis = d.GetFiles();

        if(fis != null)
        {
            foreach (FileInfo fi in fis)
            {

                if (fi.Extension.Contains("png"))
                {
                    i++;
                }
            }
        }
        
        return i;
    }

    public void DirCount(DirectoryInfo d)
    {
        DirectoryInfo[] dir = d.GetDirectories();

        foreach (DirectoryInfo dire in dir)
        {
            
            if (dire.Name.Equals(folderName))
            {
                //Debug.Log("Directory contains a folder for screenshots.");
                folderDirect = d.FullName + "\\" + folderName;
                return;
            }
        }

        Debug.Log("Directory does not contain a folder for screenshots. \n Creating one now...");
        folderDirect = d.FullName + "\\" + folderName;
        Directory.CreateDirectory(folderDirect);
        folderDirect = Directory.GetCurrentDirectory() + "\\";
    }
}