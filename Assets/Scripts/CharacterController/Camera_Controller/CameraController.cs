using NaughtyAttributes;
using UnityEngine;

namespace VHS
{
    public class CameraController : MonoBehaviour
    {
        [Space, Header("Data")]
        [SerializeField] private CameraInputData camInputData = null;

        [Space, Header("Custom Classes")]
        [SerializeField] private CameraZoom cameraZoom = null;
        [SerializeField] private CameraSwaying cameraSway = null;

        [Space, Header("Look Settings")]
        [SerializeField] private Vector2 sensitivity = Vector2.zero;
        [SerializeField] [MinMaxSlider(-90f, 90f)] private Vector2 lookAngleMinMax = Vector2.zero;

        private float m_yaw;
        private float m_pitch;

        private Transform m_pitchTranform;
        private Camera m_cam;


        void Awake()
        {
            GetComponents();
            InitValues();
            InitComponents();
            ChangeCursorState();
        }

        void LateUpdate()
        {
            CalculateRotation();
            ApplyRotation();
            HandleZoom();
        }

        void GetComponents()
        {
            m_pitchTranform = transform.GetChild(0).transform;
            m_cam = GetComponentInChildren<Camera>();
        }

        void InitValues()
        {
            m_yaw = transform.eulerAngles.y;
        }

        void InitComponents()
        {
            cameraZoom.Init(m_cam, camInputData);
            cameraSway.Init(m_cam.transform);
        }

        void CalculateRotation()
        {
            m_yaw += camInputData.InputVector.x * sensitivity.x;
            m_pitch -= camInputData.InputVector.y * sensitivity.y;

            m_pitch = Mathf.Clamp(m_pitch, lookAngleMinMax.x, lookAngleMinMax.y);
        }

        void ApplyRotation()
        {
            transform.eulerAngles = new Vector3(0f, m_yaw, 0f);
            m_pitchTranform.localEulerAngles = new Vector3(m_pitch, 0f, 0f);
        }

        public void HandleSway(Vector3 _inputVector, float _rawXInput)
        {
            cameraSway.SwayPlayer(_inputVector, _rawXInput);
        }

        void HandleZoom()
        {
            if (camInputData.ZoomClicked || camInputData.ZoomReleased)
                cameraZoom.ChangeFOV(this);

        }

        public void ChangeRunFOV(bool _returning)
        {
            cameraZoom.ChangeRunFOV(_returning, this);
        }

        void ChangeCursorState()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
