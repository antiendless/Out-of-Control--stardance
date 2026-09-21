using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class UIControlsDisplay : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject displayPanel;
    public TextMeshProUGUI controlsText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (displayPanel != null)
        {
            displayPanel.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Tab))
        {
            //showing UI text panel
            displayPanel.SetActive(true);

            //update text with current bindings
            UpdateDisplayText();
        }
        else
        {
            //hide ui after releasing tab
            displayPanel.SetActive(false);
        }
    }

    void UpdateDisplayText()
    {
        if (ControlManager.Instance != null && controlsText != null)
        {
            controlsText.text = "<b>Current Controls<\b>\n\n" +
                                "Move Left: <color = #FFEA00" + ControlManager.Instance.moveLeftKey.ToString() + "</color>\n>" + 
                                "Move Right: <color=#FFEA00>" + ControlManager.Instance.moveRightKey.ToString() + "</color>\n" +
                                "Jump: <color = #FFEA00" + ControlManager.Instance.jumpKey.ToString() + "</color>";
        }
    }
}
