using System.Collections;
using UnityEngine;

namespace PixelArsenal
{
    public class PixelArsenalBeamStatic : MonoBehaviour
    {
        [Header("Prefabs")]
        public GameObject beamLineRendererPrefab; // 라인 렌더러 프리팹
        public GameObject beamStartPrefab; // 레이저 시작 프리팹
        public GameObject beamEndPrefab; // 레이저 끝 프리팹

        private GameObject beamStart;
        private GameObject beamEnd;
        private GameObject beam;
        private LineRenderer line;

        [Header("Beam Options")]
        public bool beamCollides = true; // 충돌 여부
        public float beamLength = 100f; // 레이저 길이
        public float beamEndOffset = 0f; // 끝 오프셋
        public float textureScrollSpeed = 0f; // 텍스처 스크롤 속도
        public float textureLengthScale = 1f; // 텍스처 길이 비율

        [Header("Width Pulse Options")]
        public float widthMultiplier = 1.5f;
        private float customWidth;
        private float originalWidth;
        private float lerpValue = 0.0f;
        public float pulseSpeed = 1.0f;
        private bool pulseExpanding = true;

        private Transform _laserSpiritTransform;

        void Start()
        {
            SpawnBeam(); // 빔 생성
            DisableBeam(); // 생성된 빔을 비활성화
            InitializeBeamProperties(); // 빔 속성 초기화
        }

        // 부모 지정
        public void Initialize(Transform laserSpiritTransform)
        {
            _laserSpiritTransform = laserSpiritTransform;
        }

        // 빔을 생성하고 초기화
        public void SpawnBeam()
        {
            if (beamLineRendererPrefab)
            {
                beam = Instantiate(beamLineRendererPrefab, transform);
                beam.transform.localPosition = Vector3.zero;
                beam.transform.localRotation = Quaternion.identity;

                line = beam.GetComponent<LineRenderer>();
                if (line == null)
                {
                    Debug.LogError("라인 렌더러가 할당되지 않았습니다.");
                }

                beamStart = beamStartPrefab ? Instantiate(beamStartPrefab, beam.transform) : null;
                beamEnd = beamEndPrefab ? Instantiate(beamEndPrefab, beam.transform) : null;
            }
            else
            {
                Debug.LogError("라인 렌더러 프리팹이 할당되지 않았습니다.");
            }
        }

        // 빔 속성을 초기화
        private void InitializeBeamProperties()
        {
            if (line != null)
            {
                originalWidth = line.startWidth;
                customWidth = originalWidth * widthMultiplier;
            }
        }

        void FixedUpdate()
        {
            if (beam && line != null && _laserSpiritTransform != null)
            {
                // 레이저의 시작과 끝 위치를 설정
                line.SetPosition(0, _laserSpiritTransform.position);
                Vector3 end = _laserSpiritTransform.position + (_laserSpiritTransform.forward * beamLength);

                RaycastHit hit;
                if (beamCollides && Physics.Raycast(_laserSpiritTransform.position, _laserSpiritTransform.forward, out hit))
                {
                    end = hit.point - (_laserSpiritTransform.forward * beamEndOffset);
                    if (Vector3.Distance(_laserSpiritTransform.position, end) > beamLength)
                    {
                        end = _laserSpiritTransform.position + (_laserSpiritTransform.forward * beamLength);
                    }
                }

                line.SetPosition(1, end);

                // 빔의 시작과 끝 오브젝트 위치 갱신
                if (beamStart != null)
                {
                    beamStart.transform.position = _laserSpiritTransform.position;
                    beamStart.transform.LookAt(end);
                }

                if (beamEnd != null)
                {
                    beamEnd.transform.position = end;
                    beamEnd.transform.LookAt(_laserSpiritTransform.position);
                }
            }
        }

        void Update()
        {
            if (beam && line != null)
            {
                // 텍스처 스크롤 처리
                float distance = Vector3.Distance(_laserSpiritTransform.position, line.GetPosition(1));
                line.material.mainTextureScale = new Vector2(distance / textureLengthScale, 1);
                line.material.mainTextureOffset -= new Vector2(Time.deltaTime * textureScrollSpeed, 0);

                PulseBeamWidth(); // 폭 조절
            }
        }

        private void PulseBeamWidth()
        {
            // 빔의 폭을 펄스 효과로 조정
            if (pulseExpanding)
            {
                lerpValue += Time.deltaTime * pulseSpeed;
            }
            else
            {
                lerpValue -= Time.deltaTime * pulseSpeed;
            }

            if (lerpValue >= 1.0f)
            {
                pulseExpanding = false;
                lerpValue = 1.0f;
            }
            else if (lerpValue <= 0.0f)
            {
                pulseExpanding = true;
                lerpValue = 0.0f;
            }

            if (line != null)
            {
                float currentWidth = Mathf.Lerp(originalWidth, customWidth, Mathf.Sin(lerpValue * Mathf.PI));
                line.startWidth = currentWidth;
                line.endWidth = currentWidth;
            }
        }

        // 빔 활성화/비활성화
        public void EnableBeam()
        {
            if (beam != null)
            {
                beam.SetActive(true);
            }
        }

        public void DisableBeam()
        {
            if (beam != null)
            {
                beam.SetActive(false);
            }
        }

        // 빔이 활성화되어 있는지 여부 반환
        public bool IsBeamActive => beam != null && beam.activeSelf;

        // 빔 길이를 외부에서 조정 가능하게
        public float BeamLength
        {
            get { return beamLength; }
            set { beamLength = value; }
        }
    }
}




// 수정전.
//public void SpawnBeam()
//{
//    if (beamLineRendererPrefab)
//    {
//        beam = Instantiate(beamLineRendererPrefab);
//        beam.transform.position = transform.position;
//        beam.transform.parent = transform;
//        beam.transform.rotation = transform.rotation;

//        line = beam.GetComponent<LineRenderer>();
//        line.useWorldSpace = true;


//#if UNITY_5_5_OR_NEWER
//        line.positionCount = 2;
//#else
//			line.SetVertexCount(2); 
//#endif

//        beamStart = beamStartPrefab ? Instantiate(beamStartPrefab, beam.transform) : null;
//        beamEnd = beamEndPrefab ? Instantiate(beamEndPrefab, beam.transform) : null;
//    }
//    else
//    {
//        Debug.LogError("A prefab with a line renderer must be assigned to the `beamLineRendererPrefab` field in the PixelArsenalBeamStatic script on " + gameObject.name);
//    }
//}
//void FixedUpdate()
//{
//    if (beam)
//    {
//        line.SetPosition(0, transform.position);
//        Vector3 end = transform.position + (transform.forward * beamLength);
//        RaycastHit hit;

//        if (beamCollides && Physics.Raycast(transform.position, transform.forward, out hit))
//        {
//            end = hit.point - (transform.forward * beamEndOffset);
//            end = Vector3.Distance(transform.position, end) > beamLength
//                ? transform.position + (transform.forward * beamLength)
//                : end;
//        }
//        else
//        {
//            end = transform.position + (transform.forward * beamLength);
//        }

//        line.SetPosition(1, end);
//        beamStart.transform.position = transform.position;
//        beamStart.transform.LookAt(end);
//        beamEnd.transform.position = end;
//        beamEnd.transform.LookAt(beamStart.transform.position);
//        float distance = Vector3.Distance(transform.position, end);
//        line.material.mainTextureScale = new Vector2(distance / textureLengthScale, 1); //This sets the scale of the texture so it doesn't look stretched
//        line.material.mainTextureOffset -= new Vector2(Time.deltaTime * textureScrollSpeed, 0); //This scrolls the texture along the beam if not set to 0
//    }

//    // Pulse the width of the beam
//    if (pulseExpanding)
//    {
//        lerpValue += Time.deltaTime * pulseSpeed;
//    }
//    else
//    {
//        lerpValue -= Time.deltaTime * pulseSpeed;
//    }

//    if (lerpValue >= 1.0f)
//    {
//        pulseExpanding = false;
//        lerpValue = 1.0f;
//    }
//    else if (lerpValue <= 0.0f)
//    {
//        pulseExpanding = true;
//        lerpValue = 0.0f;
//    }

//    float currentWidth = Mathf.Lerp(originalWidth, customWidth, Mathf.Sin(lerpValue * Mathf.PI));

//    line.startWidth = currentWidth;
//    line.endWidth = currentWidth;
//}