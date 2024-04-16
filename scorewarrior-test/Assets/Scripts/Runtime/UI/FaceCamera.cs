using UnityEngine;

namespace Scorewarrior.Runtime.UI
{
    public class FaceCamera : MonoBehaviour
    {
        private Transform _cachedTransform;
        private Transform _mainCameraTransform;
        private Camera _mainCamera;

        private void Awake()
        {
            _cachedTransform = transform;

            TryGetCameraTransform();
        }

        private void LateUpdate()
        {
            if (!TryGetCameraTransform())
            {
                return;
            }

            _cachedTransform.forward = _mainCameraTransform.forward;
        }

        private bool TryGetCameraTransform()
        {
            if (_mainCameraTransform)
            {
                return true;
            }

            _mainCamera = Camera.main;
            if (!_mainCamera)
            {
                return false;
            }

            _mainCameraTransform = _mainCamera.transform;

            return true;
        }
    }
}