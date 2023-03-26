using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEMP_light : MonoBehaviour
{
    public Color onColor;
    public Color offColor;

    private Renderer objectRenderer;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        ChangeOff();
    }

    public void ChangeOn()
    {
        objectRenderer.material.color = onColor;
    }
    public void ChangeOff()
    {
        objectRenderer.material.color = offColor;
    }
}
