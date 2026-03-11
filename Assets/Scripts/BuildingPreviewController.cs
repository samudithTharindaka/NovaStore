using UnityEngine;
using System.Collections;

public class BuildingPreviewController : MonoBehaviour
{
    public static BuildingPreviewController Instance;

    private BuildingSet currentSet;
    private int currentIndex = 0;

    private GameObject currentPreview;
    private Transform previewPoint;

    private void Awake()
    {
        Instance = this;
    }

    public void OpenKiosk(BuildingSet set, Transform point)
    {
        currentSet = set;
        previewPoint = point;
        currentIndex = 0;

        ShowBuilding(); // initial show
    }

    public void CloseKiosk()
    {
        if (currentPreview)
            Destroy(currentPreview);
    }

    public void Next()
    {
        currentIndex++;
        if (currentIndex >= currentSet.buildings.Length)
            currentIndex = 0;

        ShowBuilding();
    }

    public void Back()
    {
        currentIndex--;
        if (currentIndex < 0)
            currentIndex = currentSet.buildings.Length - 1;

        ShowBuilding();
    }

    public BuildingData GetCurrent()
    {
        return currentSet.buildings[currentIndex];
    }

    private void ShowBuilding()
    {
        GameObject prefab = currentSet.buildings[currentIndex].prefab;

        // Instantiate the new building
        GameObject newPreview = Instantiate(prefab);

        // Align using BasePoint if present
        Transform basePoint = newPreview.transform.Find("BasePoint");
        if (basePoint != null)
        {
            Vector3 offset = newPreview.transform.position - basePoint.position;
            newPreview.transform.position = previewPoint.position + offset;
        }
        else
        {
            newPreview.transform.position = previewPoint.position;
        }

        // Add smooth rotation
        var rot = newPreview.AddComponent<SmoothRotation>();
        rot.rotationSpeed = 30f;

        // Set initial scale to 80% of prefab size
        Vector3 targetScale = newPreview.transform.localScale;
        newPreview.transform.localScale = targetScale * 0.8f;

        // If there's a current preview, destroy it immediately
        if (currentPreview)
        {
            Destroy(currentPreview);
        }

        // Scale up from 80% → 100%
        StartCoroutine(ScaleUp(newPreview, targetScale, 0.5f));

        currentPreview = newPreview;
    }

    private IEnumerator ScaleUp(GameObject obj, Vector3 targetScale, float duration)
    {
        float time = 0f;
        Vector3 startScale = obj.transform.localScale;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            obj.transform.localScale = Vector3.Lerp(startScale, targetScale, t);
            yield return null;
        }

        obj.transform.localScale = targetScale;
    }
}