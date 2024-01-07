using UnityEngine;

public class BackgroundScaler : MonoBehaviour
{
    public Camera mainCamera;

    void Start()
    {
        // Make sure the main camera is assigned, if not, try to find it by the tag "MainCamera"
        if (!mainCamera)
        {
            mainCamera = Camera.main; // This finds a camera with the tag "MainCamera"
            if (!mainCamera)
            {
                Debug.LogError("Main Camera not found. Please assign a camera to the BackgroundScaler script.");
                return;
            }
        }

        // Make sure the camera is orthographic for this script to work correctly
        if (!mainCamera.orthographic)
        {
            Debug.LogError("The camera is not set to orthographic mode. This script requires an orthographic camera.");
            return;
        }

        ResizeQuad();
    }

    void ResizeQuad()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer == null)
        {
            Debug.LogError("Renderer not found on the GameObject to which this script is attached.");
            return;
        }

        if (renderer.material == null || renderer.material.mainTexture == null)
        {
            Debug.LogError("Material or texture not found on the renderer.");
            return;
        }

        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = mainCamera.orthographicSize * 2;
        Vector2 cameraSize = new Vector2(cameraHeight * screenAspect, cameraHeight);
        Vector2 textureRatio = new Vector2(renderer.material.mainTexture.width, renderer.material.mainTexture.height);
        textureRatio /= textureRatio.y; // Normalize by height to get the aspect ratio

        Vector3 scale = new Vector3(cameraSize.x / textureRatio.x, cameraSize.y, 1);
        transform.localScale = scale;
    }
}