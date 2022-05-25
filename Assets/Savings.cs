using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Savings : MonoBehaviour
{

    public float Health = 0f;

    public void Save(float health1) 
    {
        Health = health1;
    }















    ////this code lets me call my gamemanager script from other scripts
    //private static Savings instance;
    //public static Savings Instance { get { return instance; } }

    ////these are my variables that I have i want saved throughout the game
    //public float Health = 0f;

    //void Awake()
    //{
    //    instance = this;

    //    var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    //    if (currentSceneIndex != 0)
    //    {
    //        Health = PlayerPrefs.GetFloat("Health");
    //    }
    //    else
    //    {
    //        Save();
    //    }

    //}

    //public void Save()
    //{
    //    PlayerPrefs.SetFloat("Health", Health);
    //}
}
