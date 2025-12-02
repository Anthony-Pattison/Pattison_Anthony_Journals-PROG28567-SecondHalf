using UnityEngine;

public class ParalaxControler : MonoBehaviour
{
    [SerializeField] private Camera viewCamera;
    [SerializeField] private float cameraDeltaScalar = 1f;

    Vector3 cameraStartPos;
    Vector3 layerStartPos;

    void Start()
    {
        cameraStartPos = viewCamera.transform.position;
        layerStartPos = transform.position;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        Vector3 cameraDelta = viewCamera.transform.position - cameraStartPos;
        float deltaX = cameraDelta.x * cameraDeltaScalar;
        float deltaY = cameraDelta.y * cameraDeltaScalar;

        transform.position = new (layerStartPos.x + deltaX, layerStartPos.y + deltaY);
    }
}
