using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI altitudeText = null;
    [SerializeField] private TextMeshProUGUI fuelText = null;
    [SerializeField] private TextMeshProUGUI gravityText = null;
    [SerializeField] private TextMeshProUGUI warpDrivePiecesText = null;
    [SerializeField] private TextMeshProUGUI panelText = null;
    [SerializeField] private TextMeshProUGUI panelConfirmText = null;

    [SerializeField] private TextMeshProUGUI speakerText = null;
    [SerializeField] private TextMeshProUGUI dialogueText = null;
    [SerializeField] private GameObject dialoguePanel = null;

    [SerializeField] private GameObject planetPanel = null;

    [SerializeField] private GameObject endPanel = null;

    private UnityAction<bool> panelCallback = null;
    private UnityAction dialogueCallback = null;
    private Queue<Dialogue> dialogueQueue = null;

    private bool showingDialogue = false;
    private bool showingEndScreen = false;

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
    public void UpdateFuelLevel(float currentFuel, float maxFuel, bool show)
    {
        if (show)
        {
            fuelText.gameObject.SetActive(true);
            currentFuel = currentFuel < 0 ? 0 : currentFuel;
            fuelText.text = "Fuel level: " + currentFuel.ToString("0") + "/" + maxFuel.ToString("0") + " (" + ((currentFuel / maxFuel) * 100).ToString("0") + "%)";
        }
        else
        {
            fuelText.gameObject.SetActive(false);
        }
    }

    public void UpdateGravityLevel(float gravityMultiplier)
    {
        gravityText.text = "Gravity: " + (gravityMultiplier * 100).ToString("0") + "%";
    }

    public void UpdateWarpDriveProgress(int piecesFound, int piecesTotal)
    {
        warpDrivePiecesText.text = piecesFound + "/" + piecesTotal;
    }

    public void ShowPlanetPanel(UnityAction<bool> callback, string message, string confirmMessage)
    {
        panelText.text = message;
        panelConfirmText.text = confirmMessage;
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

    public void SetDialogue(Dialogue[] dialogues, UnityAction onDialogueFinished)
    {
        if(dialogues == null || dialogues.Length == 0)
        {
            onDialogueFinished?.Invoke();
            return;
        }
        dialogueQueue = new Queue<Dialogue>();
        for(int i = 0; i < dialogues.Length; ++i)
        {
            dialogueQueue.Enqueue(dialogues[i]);
        }
        dialogueCallback = onDialogueFinished;
        showingDialogue = true;
        ShowDialogue(dialogueQueue.Dequeue());
    }

    public void ShowDialogue(Dialogue dialogue)
    {
        dialogueText.text = dialogue.dialogue;
        speakerText.text = dialogue.speaker;
        dialoguePanel.SetActive(true);
    }

    public void ShowEnd()
    {
        endPanel.SetActive(true);
        showingEndScreen = true;
    }

    private void Update()
    {
        if(showingDialogue && Input.GetButtonUp("Jump"))
        {
            if(dialogueQueue.Count == 0)
            {
                dialoguePanel.SetActive(false);
                showingDialogue = false;
                dialogueCallback?.Invoke();
            }
            else
            {
                ShowDialogue(dialogueQueue.Dequeue());
            }
        }

        if(showingEndScreen && Input.GetButtonUp("Jump"))
        {
            Application.Quit();
        }
    }
}

[System.Serializable]
public struct Dialogue
{
    public string speaker;
    [TextArea(2, 10)]
    public string dialogue;
}
