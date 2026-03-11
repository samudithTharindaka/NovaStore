using UnityEngine;
using Cinemachine;

public class KioskController : MonoBehaviour
{
    public BuildingSet buildingSet;

    public Transform previewPoint;
    public Transform cameraPosition;

    public GameObject kioskUI;

    public CinemachineVirtualCamera kioskCamera;

    public GameObject playerModel;

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterController>())
        {
            kioskUI.SetActive(true);

            // Hide player
            // Hide all renderers under Geometry
            Renderer[] renderers = playerModel.transform.Find("Geometry").GetComponentsInChildren<Renderer>();
            foreach (Renderer r in renderers)
            {
                r.enabled = false;
            }

            kioskCamera.transform.position = cameraPosition.position;
            kioskCamera.transform.rotation = cameraPosition.rotation;

            // Activate kiosk camera
            kioskCamera.Priority = 20;

            BuildingPreviewController.Instance.OpenKiosk(buildingSet, previewPoint);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CharacterController>())
        {
            kioskUI.SetActive(false);

            Renderer[] renderers = playerModel.transform.Find("Geometry").GetComponentsInChildren<Renderer>();
            foreach (Renderer r in renderers)
            {
                r.enabled = true;
            }

            kioskCamera.Priority = 5;

            BuildingPreviewController.Instance.CloseKiosk();
        }
    }
}