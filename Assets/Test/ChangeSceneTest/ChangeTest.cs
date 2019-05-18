using UnityEngine;
using System.Collections;
using MainSpace;

public class ChangeTest :MonoBehaviour
{
    private void Awake ()
    {
    }
    // Use this for initialization
    void Start ()
    {
        MainSystem.I.Start();
        MainSystem.I.SendMessage(MainMessages.LoadScene,"Change2");
    }

 
}
