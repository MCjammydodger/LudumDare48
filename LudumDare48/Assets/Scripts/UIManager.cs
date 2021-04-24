using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI altitudeText = null;
    [SerializeField] private TextMeshProUGUI fuelText = null;
    [SerializeField] private TextMeshProUGUI gravityText = null;

    [SerializeField] private GameObject planetPanel = null;

    private UnityAction<bool> panelCallback = null;

    private void Awake()
    {
        planetPanel.SetActive(false);
    }

    public void UpdateAltitude(float altitude)
    {
        altitudeText.text = "Altitude: " + altitude.ToString("0.00") + "km";
    }

    public void UpdateVelocity(float velocity)
    {
        altitudeText.text = "Velocity: " + velocity.ToString("0") + "km/s";
    }
    public void UpdateFuelLevel(float currentFuel, float maxFuel)
    {
        fuelText.text = "Fuel level: " + currentFuel.ToString("0") + "/" + maxFuel.ToString("0") + " (" + ((currentFuel / maxFuel) * 100).ToString("0") + "%)" ;
    }

    public void UpdateGravityLevel(float gravityMultiplier)
    {
        gravityText.text = "Gravity: " + (gravityMultiplier * 100).ToString("0") + "%";
    }

    public void ShowPlanetPanel(UnityAction<bool> callback)
    {
        panelCallback = callback;
        planetPanel.SetActive(true);
    }

    public void PanelConfirm()
    {
        planetPanel.SetActive(false);
        panelCallback?.Invoke(true);
        panelCallback = null;
    }

    public void PanelCancel()
    {
        planetPanel.SetActive(false);
        panelCallback?.Invoke(false);
        panelCallback = null;
    }
}
