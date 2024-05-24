using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : BaseController
{
    [SerializeField]
    Transform groundCheck;

    [SerializeField]
    Transform raycastOrigin;

    [SerializeField]
    float maxSlopeAngle;

    //int _mask = (1 << (int)Define.Layer.Ground) | (1 << (int)Define.Layer.Monster);

    private const float RAY_DISTANCE = 2f;
    private RaycastHit slopeHit;
    private int groundLayer = 1 << (int)Define.Layer.Ground;

    PlayerStat _stat;
    Rigidbody _rb;
    CapsuleCollider _cc;
	bool _stopSkill = false;
    private bool _canDash = true;
    private bool _isDashing = true;
    private bool _canMove = true;

    public override void Init()
    {
        WorldObjectType = Define.WorldObject.Player;
        _stat = gameObject.GetComponent<PlayerStat>();
        _rb = gameObject.GetComponent<Rigidbody>();
        _cc = gameObject.GetComponent<CapsuleCollider>();
        _rb.useGravity = true;
        _rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        _cc.isTrigger = false;

        // 플레이어 OnMouseEvent의 중복을 피하기 위해서 (-)로 함수를 제거해주고 (+)로 다시 실행
        Managers.Input.Key -= OnKeyEvent;
        Managers.Input.Key += OnKeyEvent;

        if (gameObject.GetComponentInChildren<UI_HPBar>() == null)
            Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);
    }

    protected void Update()
    {
        UpdateCamera();

        if (IsOnSlope() || _canDash)
            _rb.useGravity = true;

        if (_canMove)
            UpdateMoving();

        if (Input.GetKeyDown(KeyCode.Space) && _canDash)
            StartCoroutine(Dash());

        if (Input.GetKeyUp(KeyCode.I))
            Inventory();

        NpcScript("UI_NPC_Text");
        BoxScript("UI_Box_Text");
    }

    public bool IsOnSlope()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out slopeHit, RAY_DISTANCE, groundLayer))
        {
            var angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle != 0f && angle < maxSlopeAngle;
        }
        return false;
    }

    protected Vector3 AdjustDirectionToSlope(Vector3 direction)
    {
        return Vector3.ProjectOnPlane(direction, slopeHit.normal).normalized;
    }

    public bool IsGrounded()
    {
        Vector3 boxSize = new Vector3(transform.lossyScale.x, 0.4f, transform.lossyScale.z);
        return Physics.CheckBox(groundCheck.position, boxSize, Quaternion.identity, groundLayer);
    }
    // Quaternion.identity는 회전값이 없다는 의미입니다.

    private float CalculateNextFrameGroundAngle(float moveSpeed)
    {
        float hAxis = Input.GetAxisRaw("Horizontal");
        float vAxis = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(hAxis, 0, vAxis);

        // 다음 프레임 캐릭터 앞 부분 위치
        var nextFramePlayerPosition = raycastOrigin.position + dir * moveSpeed * Time.fixedDeltaTime;

        if (Physics.Raycast(nextFramePlayerPosition, Vector3.down, out RaycastHit hitInfo,
                            RAY_DISTANCE, groundLayer))
            return Vector3.Angle(Vector3.up, hitInfo.normal);
        return 0f;
    }

    private void Inventory()
    {
        GameObject root = Managers.UI.Root.gameObject;

        if (Util.FindChild(root, "Inventory", true) == null)
        {
            Managers.UI.ShowPopupUI<UI_Popup>("Inventory");
        }
    }

    private void NpcScript(string prefab = null)
    {
        GameObject root = Managers.UI.Root.gameObject;

        if (Util.FindChild(gameObject, prefab, true) == null)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.F) && Util.FindChild(root, "Dialogue", true) == null)
        {
            Managers.UI.ShowPopupUI<UI_Popup>("Script/Dialogue");
            GameObject go = Util.FindChild(gameObject, "UI_NPC_Text", true);
            Destroy(go);
        }
    }

    private void BoxScript(string prefab = null)
    {
        GameObject root = Managers.UI.Root.gameObject;

        if (Util.FindChild(gameObject, prefab, true) == null)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.F) && Util.FindChild(root, "CoBox", true) == null)
        {
            Managers.UI.ShowPopupUI<UI_Popup>("Box/CoBox");
            GameObject go = Util.FindChild(gameObject, "UI_Box_Text", true);
            Destroy(go);
        }
    }

    protected void UpdateCamera()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane GroupPlane = new Plane(Vector3.up, transform.position);

        float rayLength;

        if (GroupPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointTolook = cameraRay.GetPoint(rayLength);
            transform.LookAt(new Vector3(pointTolook.x, transform.position.y, pointTolook.z));
        }
    }

    protected override void UpdateMoving()
    {
        bool isOnSlope = IsOnSlope();
        bool isGrounded = IsGrounded();
        float hAxis = Input.GetAxisRaw("Horizontal");
        float vAxis = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(hAxis, 0, vAxis);
        Vector3 velocity = CalculateNextFrameGroundAngle(_stat.MoveSpeed) < maxSlopeAngle ? dir : Vector3.zero;
        Vector3 gravity = Vector3.down * Mathf.Abs(_rb.velocity.y);

        if (isGrounded && isOnSlope)         // 경사로에 있을 때
        {
            velocity = AdjustDirectionToSlope(dir);
            gravity = Vector3.zero;
            _rb.useGravity = false;
        }
        else
        {
            _rb.useGravity = true;
        }

        if (dir.magnitude < 0.1f)
        {
            _rb.velocity = Vector3.zero;
            State = Define.State.Idle;
        }
        else
        {
            if (hAxis == 0 && vAxis == 0)
            {
                _rb.velocity = Vector3.zero;
                State = Define.State.Idle;
            }
            _rb.velocity = velocity * _stat.MoveSpeed + gravity;
            //State = Define.State.Moving;
            //float moveDist = Mathf.Clamp(_stat.MoveSpeed * Time.deltaTime, 0, dir.magnitude);
            //transform.position += dir.normalized * moveDist;
        }
    }

    protected override void UpdateSkill()
    {
        if (_lockTarget != null)
        {
            Vector3 dir = _lockTarget.transform.position - transform.position;
            Quaternion quat = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime);
        }
    }

    private IEnumerator Dash()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane GroupPlane = new Plane(Vector3.up, transform.position);

        float rayLength;

        if (GroupPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 _mouse = cameraRay.GetPoint(rayLength);
            Vector3 dashDirection = (_mouse - transform.position).normalized;

            _canDash = false;
            _canMove = false;
            _isDashing = true;
            bool originalGravity = true;
            _rb.useGravity = false;

            _rb.velocity = dashDirection.normalized * _stat.DashingSpeed;

            yield return new WaitForSeconds(_stat.DashingTime);
            _rb.useGravity = originalGravity;
            _isDashing = false;
            yield return new WaitForSeconds(_stat.DashingCooldown);
            _canDash = true;
            _canMove = true;
            _rb.velocity = Vector3.zero;
        }

    }

    void OnHitEvent()
    {
        if (_lockTarget != null)
        {
            Stat targetStat = _lockTarget.GetComponent<Stat>();
            targetStat.OnAttacked(_stat);
        }

        if (_stopSkill)
        {
            State = Define.State.Idle;
        }
        else
        {
            State = Define.State.Skill;
        }
    }

    void OnKeyEvent(Define.KeyEvent evt)
    {
        switch (State)
        {
            case Define.State.Idle:
                OnMouseEvent_IdleRun(evt);
                break;
            case Define.State.Moving:
                OnMouseEvent_IdleRun(evt);
                break;
            case Define.State.Skill:
                {
                    if (evt == Define.KeyEvent.MoveUp)
                        _stopSkill = true;
                }
                break;
        }
    }

    void OnMouseEvent_IdleRun(Define.KeyEvent evt)
    {
        /*RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool raycastHit = Physics.Raycast(ray, out hit, 100.0f, _mask);*/
        //Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

        switch (evt)
        {
            case Define.KeyEvent.MoveDown:
                {
                    State = Define.State.Moving;
                    /*if (raycastHit)
                    {
                        _destPos = hit.point;
                        _stopSkill = false;

                        if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
                            _lockTarget = hit.collider.gameObject;
                        else
                            _lockTarget = null;
                    }*/
                }
                break;
            case Define.KeyEvent.MovePress:
                {
                    /*if (_lockTarget == null && raycastHit)
                        _destPos = hit.point;*/
                }
                break;
            case Define.KeyEvent.MoveUp:
                _stopSkill = true;
                break;
        }
    }
}
