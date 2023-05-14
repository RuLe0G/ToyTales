using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private CharStats charStats;
    public Slider _slider;
    public void Setup(CharStats charStats)
    {
        this.charStats = charStats;

        charStats.onHpChanged += CharStats_onHpChanged;
    }

    private void CharStats_onHpChanged(object sender, System.EventArgs e)
    {
        _slider.value = charStats.getHealthPercent();
    }
}
