using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managed : MonoBehaviour
{
    public Manager manager;
    public static int val = 0;

    void Start()
    {
        print("I'm alive!");
    }

    void OnDestroy()
    {
        print("aww :(");
    }
}
