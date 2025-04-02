using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    private Transform cameraPoint;
    private Camera mainCamera;
    private float verticalRotation = 0f;
    private float verticalClamp = 85f;
    [SerializeField] GameObject flashLight;

    private Player player;

    private void Awake()
    {
        player = GetComponent<Player>();

        cameraPoint = transform.Find("CameraPoint");
        if (cameraPoint == null)
        {
            Debug.LogError("cameraPoint 자식 오브젝트를 찾을 수 없습니다.");
            return;
        }
    }
    private void Start()
    {
        // cameraPoint 하위에 카메라가 있는지 확인
        mainCamera = cameraPoint.GetComponentInChildren<Camera>();
        // 없으면 씬의 메인 카메라를 찾아 재배치
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
            if (mainCamera != null)
            {
                mainCamera.transform.SetParent(cameraPoint);
                mainCamera.transform.localPosition = Vector3.zero;
                mainCamera.transform.localRotation = Quaternion.identity;
            }
            else
            {
                Debug.LogError("씬에서 메인 카메라를 찾을 수 없습니다.");
            }
        }
        else
        {
            // 기존 카메라의 로컬 회전값 보정 (-180 ~ 180 범위)
            verticalRotation = mainCamera.transform.localEulerAngles.x;
            if (verticalRotation > 180f)
                verticalRotation -= 360f;
        }

    }


    private void LateUpdate()
    {
        // Look 액션 입력값 읽기
        Vector2 lookInput = player.Input.playerActions.Look.ReadValue<Vector2>();
        float sensitivity = player.Data.cameraData.mouseSensitivity;
        // 수평 회전: 플레이어 오브젝트 회전 (좌우)
        float mouseX = lookInput.x * sensitivity;
        transform.Rotate(0f, mouseX, 0f);

        // 수직 회전: 카메라의 로컬 회전 (상하)
        float mouseY = lookInput.y * sensitivity;
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalClamp, verticalClamp);

        if (mainCamera != null)
        {
            mainCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
            flashLight.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        }
    }

    public void turnOn()
    {
        flashLight.SetActive(true);
    }

    public void turnOff()
    {
        flashLight.SetActive(false);
    }
}
