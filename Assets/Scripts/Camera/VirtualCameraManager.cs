using Cinemachine;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class VirtualCameraManager : Singleton<VirtualCameraManager>
{
    //protected override void Awake()
    //{
    //    base.Awake();
    //    virtualCamera = GetComponent<CinemachineVirtualCamera>();
    //    TryUpdateCameraTarget();
    //}
    private void Start()
    {
        TryUpdateCameraTarget();
    }
    private CinemachineVirtualCamera virtualCamera;
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        TryUpdateCameraTarget();
    }
    private void TryUpdateCameraTarget()
    {
        if (virtualCamera == null)
            virtualCamera = GetComponent<CinemachineVirtualCamera>();

        if (Player.Instance != null)
        {
            var target = Player.Instance.transform;

            virtualCamera.Follow = target;
            virtualCamera.LookAt = target;

            // ∆естко устанавливаем позицию камеры в стартовую точку
            transform.position = target.position;
        }
        else
        {
            Debug.LogWarning("[CameraManager] »грок не найден при обновлении цели камеры.");
        }
    }
}
