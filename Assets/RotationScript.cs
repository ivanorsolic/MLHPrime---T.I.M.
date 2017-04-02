using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        //for (double i = 0; i < 5; i += 0.01)
        //{
        //    Transform(0.01f);
        //}
    }

    //void Transform(float alpha)
    ////{
    ////    int x = (int)(0 + (10 * Math.Cos(alpha)));
    ////    int y = (int)(0 + (30 * Math.Sin(alpha)));

    ////    this.transform.position = new Vector3(x, y, 0);

    ////}

    //float timeCounter = 0;
    //bool Direction = false;

    //void Update()
    //{

    //    if (Input.GetKeyDown(KeyCode.LeftArrow))
    //    {
    //        Direction = false;
    //    }

    //    if (Input.GetKeyDown(KeyCode.RightArrow))
    //    {
    //        Direction = true;
    //    }

    //    if (Direction)
    //        timeCounter += Time.deltaTime;

    //    else
    //        timeCounter -= Time.deltaTime;

    //    float x = Mathf.Cos(timeCounter);

    //    float y = Mathf.Sin(timeCounter);

    //    float z = 0;

    //    transform.position = new Vector3(x, y, z);
    //}

    int a = 50;
    int b = 75;
    int x = 1;

    int y = 1;
    int alpha = 0;
    int X;
    int Y;


    void Update()
    {
        //transform.Translate(0, 0, Time.deltaTime * 7);

        //this.transform.Rotate(0, Time.deltaTime * 60, 0, Space.World);

        alpha += 10;
        X = (int)(x + ((a * Mathf.Cos((float)(alpha * .005d)))));
        Y = (int)(y + ((b * Mathf.Sin((float)(alpha * .005d)))));


        this.gameObject.transform.position = new Vector3(26,34,1) + new Vector3(X, 0, Y);
    }
}