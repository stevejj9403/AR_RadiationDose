using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMover : MonoBehaviour
{
    public GameObject menu;
    public GameObject camera;

    public void bringMenu()
    {
        menu.SetActive(true);
        menu.transform.position = camera.transform.position + 2*camera.transform.TransformDirection(Vector3.forward);
        menu.transform.rotation = camera.transform.rotation;
    }
}
