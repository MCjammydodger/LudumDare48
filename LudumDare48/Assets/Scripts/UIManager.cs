using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI altitudeText;

    public void UpdateAltitude(float altitude)
    {
        altitudeText.text = "Altitude: " + altitude.ToString("0.00") + "km";
    }
}
