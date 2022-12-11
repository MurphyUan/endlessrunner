using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phantom : MonoBehaviour
{
    private static Phantom instance = new Phantom();
    private Phantom(){}
    static Phantom(){}

    public static Phantom Instance 
    {
        get{ return instance;}
    }

    public static void MovePhantom(Vector3 relPos)
    {
        instance.transform.position += relPos;
    }
}
