using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class RotateSlider : MonoBehaviour
{
    [AddComponentMenu("Assets/Scripts/RotateSlider.cs")]
    public GameObject legs;
    public Interactable checkBox;
    [SerializeField]
    private GameObject camera;
    [SerializeField]
    private GameObject modelTarget;

    [SerializeField]
    private Interactable tumorCheckbox; 
    [SerializeField]
    private Interactable lowDoseCheckbox; 
    [SerializeField]
    private Interactable mediumDoseCheckbox;

    [SerializeField]
    private GameObject tumor;
    [SerializeField]
    private GameObject lowDose;
    [SerializeField]
    private GameObject mediumDose;

    public void rotateX(SliderEventData eventData)
    {
        // Smoothly tilts a transform towards a target rotation.
        float tiltAroundX = eventData.NewValue * 180.0f;
        tiltAroundX -= 90;

        // Rotate the cube by converting the angles into a quaternion.
        //Quaternion target = Quaternion.Euler(tiltAroundX, legs.transform.eulerAngles.y, legs.transform.eulerAngles.z);

        // Dampen towards the target rotation
        //legs.transform.rotation = target;

        legs.transform.rotation = Quaternion.Euler(tiltAroundX, legs.transform.rotation.y, legs.transform.rotation.z);
    }
    public void rotateY(SliderEventData eventData)
    {
        // Smoothly tilts a transform towards a target rotation.
        float tiltAroundY = eventData.NewValue * 360.0f;
        tiltAroundY -= 180;

        // Rotate the cube by converting the angles into a quaternion.
        Quaternion target = Quaternion.Euler(legs.transform.eulerAngles.x, tiltAroundY, legs.transform.eulerAngles.z);

        // Dampen towards the target rotation
        legs.transform.rotation = Quaternion.Slerp(legs.transform.rotation, target, Time.deltaTime * 5.0f);
    }
    public void rotateZ(SliderEventData eventData)
    {
        // Smoothly tilts a transform towards a target rotation.
        float tiltAroundZ = eventData.NewValue * 360.0f;
        tiltAroundZ -= 180;

        // Rotate the cube by converting the angles into a quaternion.
        Quaternion target = Quaternion.Euler(legs.transform.eulerAngles.x, legs.transform.eulerAngles.y, tiltAroundZ);

        // Dampen towards the target rotation
        legs.transform.rotation = Quaternion.Slerp(legs.transform.rotation, target, Time.deltaTime * 5.0f);
    }
    public void changeOpacity(SliderEventData eventData)
    {
        Renderer[] childrenColors = GetComponentsInChildren<Renderer>();

        foreach(Renderer renderer in childrenColors)
        {
            Color newColor = renderer.material.color;
            newColor.a = eventData.NewValue;

            renderer.material.color = newColor;
        }
    }
    public void buttonPressed ()
    {
        MeshRenderer[] childrenColors = legs.GetComponentsInChildren<MeshRenderer>();
        
        if(checkBox.IsToggled)
        {
            foreach (MeshRenderer renderer in childrenColors)
            {
                //_Mode, 3 is transparent rendering mode
                renderer.material.SetFloat("_Mode", 3);
                renderer.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                renderer.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                renderer.material.EnableKeyword("_ALPHABEND_ON");
                renderer.material.renderQueue = 3000;
            }
        } else {
            foreach (MeshRenderer renderer in childrenColors)
            {
                /*
                Material mat = renderer.material;
                //mode 0 is opaque rendering mode
                mat.SetFloat("_Mode", 0);
                */
                renderer.material.SetFloat("_Mode", 0);
               
            }
        }
        Debug.Log(checkBox.IsToggled);
    }
    
    public void bringLeg()
    {
        legs.transform.position = modelTarget.transform.position;
        legs.transform.rotation = modelTarget.transform.rotation;
        legs.transform.Rotate(-90.0f, 0f, 0f);
    }

    public void tumorCheckboxToggle()
    {
        //checked
        if(tumorCheckbox.IsToggled)
        {
            tumor.SetActive(true);
        } else
        {
            tumor.SetActive(false);
        }
    }

    public void lowDoseCheckboxToggle()
    {
        if(lowDoseCheckbox.IsToggled)
        {
            lowDose.SetActive(true);
        } else
        {
            lowDose.SetActive(false);
        }
    }
    public void mediumDoseCheckboxToggle()
    {
        if(mediumDoseCheckbox.IsToggled)
        {
            mediumDose.SetActive(true);
        } else
        {
            mediumDose.SetActive(false);
        }
    }
}
