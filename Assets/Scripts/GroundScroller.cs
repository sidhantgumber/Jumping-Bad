using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScroller : MonoBehaviour
{

    public float groundScrollSpeed = 0.2f;
    Material groundMaterial;
    Vector2 offSet;
    void Start()
    {
        groundMaterial = GetComponent<Renderer>().material;
        offSet = new Vector2(groundScrollSpeed, 0f);
        
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided");
    }


    void Update()
    {
        if (Bird.GetInstance().HasGameStarted())
        {
            groundMaterial.mainTextureOffset += offSet * Time.deltaTime;
        }
    }
}
