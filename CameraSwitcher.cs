using System.Collections;
using UnityEngine;

//By Lane Gingras
//File name: CameraSwitcher.cs
//Created on: November 1st, 2023

public class CameraSwitcher : MonoBehaviour
{
   [TextArea]
   public string camerasToolTip = "Set the array size to the amount of Cameras in the scene. Then, drag all of the camera gameobjects from the scene hierarchy to the Cameras array entries.";
   public GameObject[] cameras;//Each entry contains a gameobject reference of a camera

   [TextArea]
   public string secondsBetweenToolTip = "Set the array size to the amount of Cameras in the scene. Then, enter a float/decimal value in the Seconds Between array entries. These float values determine how long each camera is active before switching to the next one.";
   public float[] secondsBetween;//Each entry determines how long a camera is active
   
   public string activeCamera;//Public variable showing what camera is currently active
   
   private int arrayPointer = 0;//arrayPointer used to point to secondsBetween and cameras current array positions 
   private int disableEnableLength;//Used in the disable/enable on start/end features, determines the length of an array before checking it
   private bool cameraActive;//Since coroutine is triggered in update, this is the switch that prevents the coroutine from being triggered again until the camera duration is over
   private bool error = false;
   private string errorMessage = "";
   private string warningMessage= "";

    [TextArea]
   public string disableEnableToolTip = "The disable/enable on start/end arrays hold gameobjects that you would like to be enabled or disabled when the script is first run or when it finishes the final camera duration.";
   public GameObject[] disableOnStart;//Array holding gameobjects that will be turned off when this script initially runs
   public GameObject[] enableOnStart;//Array holding gameobjects that will be turned on when this script initially runs
   public GameObject[] disableOnEnd;//Array holding gameobjects that will be turned off when this script ends
   public GameObject[] enableOnEnd;//Array holding gameobjects that will be turned on when this script ends



    // Start is called before the first frame update
    void Start()
    {
        if (cameras.Length != secondsBetween.Length)//error check, camera and secondsBetween array have to be equal
        {
            error = true;
            errorMessage = "The Cameras array is not equal to the Seconds Between array.";
        }

        for (int i = 0; i < secondsBetween.Length; i++)//error check, there should be no negative numbers in the secondsBetween array
        {
            if (secondsBetween[i] < 0)
            {
                error = true;
                errorMessage = "The Seconds Between array contains a negative number.";
            }
        }

        if (cameras.Length == 0)//error check, there has to be at least 1 camera
        {
            error = true;
            errorMessage = "The Cameras array is empty.";
        }

        if (error == true)//if there has been a red level error, it will be displayed here
        {
            Debug.Log("<color=red>Error: </color>" + errorMessage + "\n\n");
        }
        else//disable on start and enable on start + yellow error checking, yellow meaning the program will still run but will warn the user
        {
            disableEnableLength = enableOnStart.Length;

            if (disableEnableLength != 0)//enable on start section
            {
                for (int i = 0; i < disableEnableLength; i++)
                {
                    if (enableOnStart[i] != null && error == false)
                    {
                        enableOnStart[i].SetActive(true);
                    }   
                    else
                    {
                        warningMessage = "Disable/Enable on Start/End contains a null entry in the array.";//yellow error, should be no null values in the array, program will still run
                    }     

                }
            }


            disableEnableLength = disableOnStart.Length;

            if (disableEnableLength != 0)//disable on start section
            {
                for (int i = 0; i < disableEnableLength; i++)
                {
                    if (disableOnStart[i] != null && error == false)
                    {
                        disableOnStart[i].SetActive(false);
                    }   
                    else
                    {
                        warningMessage = "Disable/Enable on Start/End contains a null entry in the array.";//yellow error, should be no null values in the array, program will still run
                    }     

                }
            }


            disableEnableLength = enableOnEnd.Length;

            if (disableEnableLength != 0)//enable on end section ***ONLY ERROR CHECKS, ACTUAL ENABLE ON END OCCURS IN COROUTINE***
            {
                for (int i = 0; i < disableEnableLength; i++)
                {
                    if (enableOnEnd[i] == null)
                    {
                        warningMessage = "Disable/Enable on Start/End contains a null entry in the array.";//yellow error, should be no null values in the array, program will still run
                    }       
                }
            }


            disableEnableLength = disableOnEnd.Length;

            if (disableEnableLength != 0)//disable on end section ***ONLY ERROR CHECKS, ACTUAL ENABLE ON END OCCURS IN COROUTINE***
            {
                for (int i = 0; i < disableEnableLength; i++)
                {
                    if (disableOnEnd[i] == null)
                    {
                        warningMessage = "Disable/Enable on Start/End contains a null entry in the array.";//yellow error, should be no null values in the array, program will still run
                    }       
                }
            }
        }

        if (warningMessage != "")//if there has been a yellow error, warn the user, but the program will proceed
        {
            Debug.Log("<color=yellow>Warning: </color>" + warningMessage + "\n\n");
        }
    }



    // Update is called once per frame
    void Update()
    {
        if ((arrayPointer < cameras.Length) && (cameraActive == false) && error == false)//if the current camera counter doesn't exceed the number of camera, and there isn't currently a camera duration happening, and there wasn't a red error
        {
            cameraActive = true;
            activeCamera = cameras[arrayPointer].name;
            StartCoroutine(MyCoroutine());
        }
      
    }



    //Spawns the camera, disables the previous one, waits for the user inputted amount of time, then at the end it enables the Update to go to the next camera if there is one
    IEnumerator MyCoroutine()
    {
        cameras[arrayPointer].SetActive(true);//enable the camera
        
        if(arrayPointer != 0)//disable the previous camera, except for if it is the inital camera
            cameras[arrayPointer-1].SetActive(false);
        
        yield return new WaitForSeconds(secondsBetween[arrayPointer]);
        
        
        if(arrayPointer < cameras.Length)//if there are still more cameras, increase the counter and enable the script to move to the next camera
        {
            arrayPointer +=1;
            cameraActive = false;
        }
        


        //enable and disable on end section
        if(arrayPointer >= cameras.Length)//if all of the cameras are done
        {
            disableEnableLength = enableOnEnd.Length;

            if (disableEnableLength != 0)//enable on end
            {
                for (int i = 0; i < disableEnableLength; i++)
                {
                    if (enableOnEnd[i] != null)
                    {
                        enableOnEnd[i].SetActive(true);
                    }    
                }
            }


            disableEnableLength = disableOnEnd.Length;

            if (disableEnableLength != 0)//disable on end
            {
                for (int i = 0; i < disableEnableLength; i++)
                {
                    if (disableOnEnd[i] != null)
                    {
                        disableOnEnd[i].SetActive(false);
                    }    
                }
            }


            cameras[arrayPointer-1].SetActive(false);//turn off the last camera
            Destroy(this.gameObject);//destroy this gameobject
        }
    }
}