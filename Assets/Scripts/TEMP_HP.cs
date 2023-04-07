using UnityEngine;

public class TEMP_HP : MonoBehaviour
{
    public CharStats player;
    public HealthBar bar;


    private void Start()
    {
        bar.Setup(player);
    }
}
