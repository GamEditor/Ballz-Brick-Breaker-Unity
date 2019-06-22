using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    public static Loader Instance;

    private void Awake()
    {
        Instance = this;
    }


}