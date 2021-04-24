using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI altitudeText = null;
    [SerializeField] private TextMeshProUGUI fuelText = null;

    public void UpdateAltitude(float altitude)
    {
        altitudeText.text = "Altitude: " + altitude.ToString("0.00") + "km";
    }

    public void UpdateFuelLevel(float currentFuel, float maxFuel)
    {
        fuelText.text = "Fuel level: " + ((currentFuel / maxFuel) * 100).ToString("0") + "%";
    }
}
