using TMPro;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private CharStats charStats;
    public TMP_Text _text;
    public void Setup(CharStats charStats)
    {
        this.charStats = charStats;

        charStats.onHpChanged += CharStats_onHpChanged;
    }

    private void CharStats_onHpChanged(object sender, System.EventArgs e)
    {
        _text.text = charStats.getHealth().ToString();
    }
}
