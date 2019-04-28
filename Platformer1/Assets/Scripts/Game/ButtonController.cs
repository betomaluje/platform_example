using UnityEngine;
using DG.Tweening;

public class ButtonController : MonoBehaviour
{
    public GameObject[] buttonsInput;
    
    void Awake()
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
            if(shouldEnable && !button.name.Equals("AbsorbButton"))
            {
                button.transform.DOScale(1f, 0.25f);
            }
        }
    }
}
