using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotation : MonoBehaviour
{

    private Vector3 vector3;

    public EchoHub EchoHub
    {
        get { return EchoHub.Instance; }
    }

    // Use this for initialization
    void Start () {
        vector3 = GameObject.Find("Sun").transform.position;
        
        
        
    }

    // Update is called once per frame
    void Update ()
	{

        if (SlusamKomandu.IsRotating)
	    {
	        Rotate();
	    }
	    else
	    {
            StopRotate();
	    }
    }

    void Rotate()
    {
        
        GameObject.Find("Mercury").transform.RotateAround(vector3,Vector3.up, 5*Time.deltaTime);
        GameObject.Find("Venera").transform.RotateAround(vector3, Vector3.up, 5 * Time.deltaTime);
        GameObject.Find("Earth").transform.RotateAround(vector3, Vector3.up, 4 * Time.deltaTime);
        GameObject.Find("Moon").transform.RotateAround(vector3, Vector3.up, 4 * Time.deltaTime);
        GameObject.Find("Mars").transform.RotateAround(vector3, Vector3.up, 3 * Time.deltaTime);
        GameObject.Find("Jupiter").transform.RotateAround(vector3, Vector3.up, 3 * Time.deltaTime);
        GameObject.Find("Saturn").transform.RotateAround(vector3, Vector3.up, 2 * Time.deltaTime);
        GameObject.Find("Uranus").transform.RotateAround(vector3, Vector3.up, 2 * Time.deltaTime);
        GameObject.Find("Pluton").transform.RotateAround(vector3, Vector3.up, 1* Time.deltaTime);




    }

    void StopRotate()
    {
        transform.RotateAround(vector3, Vector3.up, 0 * Time.deltaTime);
    }
}
