using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraZoom : MonoBehaviour
{
    private float originalSize;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CinemachineVirtualCamera camera = other.transform.parent.Find("Camera").gameObject.GetComponent<CinemachineVirtualCamera>();
            originalSize = camera.m_Lens.OrthographicSize;
            camera.m_Lens.OrthographicSize = 6;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CinemachineVirtualCamera camera = other.transform.parent.Find("Camera").gameObject.GetComponent<CinemachineVirtualCamera>();
            camera.m_Lens.OrthographicSize = 10;
        }
    }
}