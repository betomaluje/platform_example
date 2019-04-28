using System.Collections;
using UnityEngine;
using Cinemachine;

public class CameraZoom : MonoBehaviour
{
    public float zoomFactor = 0.5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CinemachineVirtualCamera camera = other.transform.parent.Find("Camera").gameObject.GetComponent<CinemachineVirtualCamera>();            
            StartCoroutine(ZoomIn(camera));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CinemachineVirtualCamera camera = other.transform.parent.Find("Camera").gameObject.GetComponent<CinemachineVirtualCamera>();
            StartCoroutine(ZoomOut(camera));
        }
    }

    private IEnumerator ZoomIn(CinemachineVirtualCamera camera)
    {
        float tempZoom = 7f;
        
        while (tempZoom >= 4)
        {
            camera.m_Lens.OrthographicSize = tempZoom;
            yield return null;
            tempZoom -= zoomFactor;
        }        
    }

    private IEnumerator ZoomOut(CinemachineVirtualCamera camera)
    {
        float tempZoom = 4f;       

        while (tempZoom <= 7)
        {
            camera.m_Lens.OrthographicSize = tempZoom;
            yield return null;
            tempZoom += zoomFactor;
        }        
    }
}