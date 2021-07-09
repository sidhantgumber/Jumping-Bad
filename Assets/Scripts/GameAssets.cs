using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    // yeh class use kari game objects ko code ke through refer karne ke liye
    // pipe etc ke multiple instances banenge to unhe directly access karne ke liye saare instances ko yahan se refer karva diya
    private static GameAssets instance;

    public static GameAssets GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
    }

    public Transform pipe;
   // public Transform ground;
    
}
