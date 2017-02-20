using UnityEngine;
using System.Collections;

public class ViewManager : MonoBehaviour
{
    public float mininumZoom, maximumZoom, stepsPerNotch, interpolationRate;
    public Camera linkedCamera;
    public Player_Script linkedPlayer;
    public MapGeneration linkedMap;
    public bool isFollowingPlayer;
    
    float currentZoom;
    Vector3 currentPos;

    bool isDragging;
    Vector3 startingCameraPosition;
    Vector3 startingDragPosition;

    void UpdateInput()
    {
        Vector3 mousePosition = Input.mousePosition;
        float scroll = Input.mouseScrollDelta.y * stepsPerNotch;
        bool midButton = Input.GetMouseButton(2);

        float factor = Mathf.Pow(1.2f, -scroll);

        Zoom(factor);

        if (midButton && !isFollowingPlayer)
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
    void UpdateFollowingPlayer()
    {
        isFollowingPlayer = linkedPlayer.GetVelocity().sqrMagnitude != 0f;

        if (isFollowingPlayer)
        {
            Vector3 position = linkedPlayer.transform.position;
            position.z = currentPos.z;
            currentPos = position;
        }
    }
    void UpdateCameraBounding()
    {
        float mapHalfWidth = linkedMap.mapSizeX * linkedMap.tileSize.x * 0.5f;
        float mapHalfHeight = linkedMap.mapSizeY * linkedMap.tileSize.y * 0.5f;

        float aspectRatio = (float)Screen.width / Screen.height;

        Vector2 topLeft = new Vector2(
            currentPos.x - linkedCamera.orthographicSize * aspectRatio,
            currentPos.y - linkedCamera.orthographicSize
            );

        if (topLeft.x < -mapHalfWidth)
            currentPos.x = -mapHalfWidth + linkedCamera.orthographicSize * aspectRatio;
        if (topLeft.y < -mapHalfHeight)
            currentPos.y = -mapHalfHeight + linkedCamera.orthographicSize;

        Vector2 bottomRight = new Vector2(
            currentPos.x + linkedCamera.orthographicSize * aspectRatio,
            currentPos.y + linkedCamera.orthographicSize
            );

        if (bottomRight.x > mapHalfWidth)
            currentPos.x = mapHalfWidth - linkedCamera.orthographicSize * aspectRatio;
        if (bottomRight.y > mapHalfHeight)
            currentPos.y = mapHalfHeight - linkedCamera.orthographicSize;
    }

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
        UpdateFollowingPlayer();
        UpdateCameraBounding();

        linkedCamera.orthographicSize += (currentZoom - linkedCamera.orthographicSize)
            * Mathf.Min(Time.deltaTime * interpolationRate, 1f);
        linkedCamera.transform.position += (currentPos - linkedCamera.transform.position)
            * Mathf.Min(Time.deltaTime * interpolationRate, 1f);
    }
}
