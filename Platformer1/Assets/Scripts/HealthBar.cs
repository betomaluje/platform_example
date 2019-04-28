using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Transform bar;
    
    void Start()
    {
        bar = transform.Find("Bar");
    }

    public void setHealth(float normalisedSize)
    {
        if (bar != null)
        {
            bar.localScale = new Vector3(normalisedSize, 1f);
        }        
    }
}
