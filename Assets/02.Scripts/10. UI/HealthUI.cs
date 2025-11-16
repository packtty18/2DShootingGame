using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public PlayerStat Stat;

    public Slider sliderUI;

    private void Start()
    {
        sliderUI = GetComponent<Slider>();
    }

    private void Update()
    {
        sliderUI.value = Stat != null ? Stat.CurrentHealth / Stat.MaxHealth : 0f;
    }
}
