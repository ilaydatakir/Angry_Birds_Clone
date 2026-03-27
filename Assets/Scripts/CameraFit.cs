using UnityEngine;

public class CameraFit : MonoBehaviour
{
    [Tooltip("Camera")]
    public SpriteRenderer backgroundSprite;

    void Start()
    {
        FitCameraToBackground();
    }

    void FitCameraToBackground()
    {
        if (backgroundSprite == null)
        {
            return;
        }

        Camera cam = GetComponent<Camera>();
        if (cam == null || !cam.orthographic)
        {
            return;
        }

        float bgWidth = backgroundSprite.bounds.size.x;

        float screenAspect = (float)Screen.width / Screen.height;

        cam.orthographicSize = bgWidth / (2f * screenAspect);

        Vector3 bgCenter = backgroundSprite.bounds.center;
        transform.position = new Vector3(bgCenter.x, bgCenter.y, transform.position.z);
    }
}
