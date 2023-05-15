using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dashUI : MonoBehaviour
{
    public Sprite sp1;
    public Sprite sp2;

    public Image mainIcon;

    public ThridPersonDash dash;

    private void Start()
    {
        dash.dashUse += playerDasedUse;
        dash.dashReset += playerDasedReset;
    }

    private void playerDasedReset(ThridPersonDash obj)
    {
        mainIcon.sprite= sp2;
    }

    private void playerDasedUse(ThridPersonDash obj)
    {
        mainIcon.sprite = sp1;
    }
}
