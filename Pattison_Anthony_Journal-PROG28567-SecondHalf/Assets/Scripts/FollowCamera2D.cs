using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Camera))]

public class FollowCamera2D : MonoBehaviour
{
    [SerializeField] private Transform Target;
    [SerializeField] private float Speed;
    [SerializeField] private Tilemap Tilemap;

    private Vector3 offset;

    private float LeftCameraBoundary;
    private float RightCameraBoundary;
    private float BottomCameraBoundary;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        offset = transform.position - Target.position;
        CalculateBounds();
    }

    private void CalculateBounds()
    {
        Tilemap.CompressBounds();
        Camera Cam = GetComponent<Camera>();
        float orthoSize = Cam.orthographicSize;
        Vector3 viewprotHalfsize = new (orthoSize * Cam.aspect, orthoSize);

        Vector3Int TileMapMin = Tilemap.cellBounds.min;
        Vector3Int TileMapMax = Tilemap.cellBounds.max;
        
        LeftCameraBoundary = TileMapMin.x + viewprotHalfsize.x;
        RightCameraBoundary = TileMapMax.x - viewprotHalfsize.x;
        BottomCameraBoundary = TileMapMin.y + viewprotHalfsize.y;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 DesiredPos = Target.position + offset;
        Vector3 StepPos = Vector3.Lerp(transform.position, DesiredPos, Speed * Time.deltaTime);

        StepPos.x = Mathf.Clamp(StepPos.x, LeftCameraBoundary, RightCameraBoundary);
        StepPos.y = Mathf.Clamp(StepPos.y, BottomCameraBoundary, DesiredPos.y);
        transform.position = StepPos;

    }
}
