using UnityEngine;
using System.Collections;

public class ViewManager : MonoBehaviour
{
    public float mininumZoom, maximumZoom, stepsPerNotch, interpolationRate;
    public Camera linkedCamera;
    public GameplayManager linkedGameplay;

    float currentZoom;
    Vector3 currentPos;

    bool isDragging;
    Vector3 startingCameraPosition;
    Vector3 startingDragPosition;

    // Use this for initialization
    void Start()
    {
        currentZoom = maximumZoom;
        currentPos.Set(0f, 0f, linkedCamera.transform.position.z);
        linkedCamera.orthographicSize = currentZoom;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInput();

        linkedCamera.orthographicSize += (currentZoom - linkedCamera.orthographicSize)
            * Mathf.Min(Time.deltaTime * interpolationRate, 1f);
        linkedCamera.transform.position += (currentPos - linkedCamera.transform.position)
            * Mathf.Min(Time.deltaTime * interpolationRate, 1f);
    }

    void UpdateInput()
    {
        Vector3 mousePosition = Input.mousePosition;
        float scroll = Input.mouseScrollDelta.y * stepsPerNotch;
        bool midButton = Input.GetMouseButton(2);

        float factor = Mathf.Pow(1.2f, -scroll);

        Zoom(factor);

        if (midButton)
        {
            if (!isDragging)
            {
                isDragging = true;
                startingCameraPosition = currentPos;
                startingDragPosition = mousePosition;
            }
            else
            {
                Vector3 delta = (mousePosition - startingDragPosition)
                    * 2f * linkedCamera.orthographicSize / Screen.height;
                currentPos = startingCameraPosition - delta;
            }
        }
        else
        {
            isDragging = false;
        }
    }

    void Zoom(float amount)
    {
        currentZoom *= amount;
        if (currentZoom < mininumZoom) currentZoom = mininumZoom;
        if (currentZoom > maximumZoom) currentZoom = maximumZoom;
    }
}
