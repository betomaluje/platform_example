using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public GameObject[] buttonsInput;
    
    void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            // we need to show the buttons
            ToggleInputButtons(true);
        } else
        {
            // we need to hide buttons
            ToggleInputButtons(false);
        }
    }

    private void ToggleInputButtons(bool shouldEnable)
    {
        foreach(GameObject button in buttonsInput)
        {
            button.SetActive(shouldEnable);
        }
    }
}
