using UnityEngine;
using TMPro;

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
            controlsText.text = "Current Controls\n\n" +
                                "Move Left: " + ControlManager.Instance.moveLeftKey.ToString() + "\n" + 
                                "Move Right: " + ControlManager.Instance.moveRightKey.ToString() + "\n" +
                                "Jump: " + ControlManager.Instance.jumpKey.ToString();
        }
        else if (controlsText != null)
        {
            controlsText.text = "Current Controls\n\n" +
                                "Move Left: A\n" + 
                                "Move Right: D\n" + 
                                "Jump: Spacebar";
        }
    }
}
//hi, if your're actually reading this, thanks! I spent alot of time writing all of this. \
// also, please consider following me and my project on stardance!!
