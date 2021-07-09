using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float backgroundScrollSpeed = 0.2f;
    Material backgroundMaterial;
    Vector2 offSet;
    void Start()
    {
        backgroundMaterial = GetComponent<Renderer>().material;
        offSet = new Vector2(backgroundScrollSpeed, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Bird.GetInstance().HasGameStarted())
        {
            backgroundMaterial.mainTextureOffset += offSet * Time.deltaTime;
        }
    }
}
