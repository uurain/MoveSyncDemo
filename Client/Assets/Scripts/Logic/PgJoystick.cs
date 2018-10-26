using CustomDataStruct;
using DG.Tweening;
using FairyGUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PgJoystick : SingetonMono<PgJoystick>
{
    //public enum EState
    //{
    //    defaultScene = 0,
    //    lockView = 1,
    //    SoulView = 2,
    //}

    public delegate void OnMoveHandler2(float value);
    public delegate void OnMoveEndHandler2(bool val);

    public static bool DebugMode = false;

    public float offsetLook = 1.76f;

    [SerializeField]
    public OnMoveHandler2 onMove;
    [SerializeField]
    public OnMoveEndHandler2 onMoveEnd;

    //private EState _state = EState.defaultScene;

    public bool isLockView = false;

    public bool enableCamera = false;

    public bool enableMoveDir = true;

    public bool _enable = true;

    private Vector2 OldTmpAxis;

    public int sceneRotPoint = -1;

    public Vector2 RangeRotateY = new Vector2(10,50);

    public Transform directTransform;
    public Transform cameraTransform;
    public Transform cameraLookAt;
    public Vector3 followOffset = new Vector3(0, 5.55f, -7.94f);
    public float minCamDistance = 4;
    public float maxCamDistance = 15;
    public float cameraTargetDist = -1;

    private bool enableKeySimulation = true;
    private bool keySimulationMove = false;

    private float curDegree = -1000;

    private bool isMove;

    private bool _enableAutoCamRotation = false;

    public float autoRotateSpeed = 15;

    private int zoomType = 0;

    public Vector3 defaultFollow = Vector3.zero;
    public Quaternion defaultRotation = Quaternion.identity;

    private Vector3 _sceneLockView = Vector3.zero;

    private bool _enableRotateY = true;

    bool _isMaxView = false;
    float _preViewDist = 0;
    float autoTargetDist = 0;
    Vector3 autoTargetOffset;
    System.Action autoCamZoomInDl = null;

    public void SetLockView(bool val)
    {
        if (isLockView == val)
            return;
        isLockView = val;
        if (_isMaxView)
            return;
        if (isLockView)
        {
            AutoMove(_sceneLockView);
        }
        else
        {
            AutoMove(defaultFollow);
        }
    }

    public void SetSceneLockView(string strVal)
    {
        if(!string.IsNullOrEmpty(strVal))
        {
            string[] strValAry = strVal.Split(',');
            if(strValAry.Length == 3)
            {
                _sceneLockView = new Vector3(float.Parse(strValAry[0]), float.Parse(strValAry[1]), float.Parse(strValAry[2]));
            }
            else
            {
                Debug.LogError("SetSceneLockView is error");
            }
        }            
    }

    public void BeginAutoCamZoomIn(float val)
    {
        autoTargetDist = val;
        autoTargetOffset = followOffset.normalized * autoTargetDist;
    }

    public void StopAutoCamZoomIn()
    {
        _enableRotateY = true;
        autoTargetDist = 0;
        if (autoCamZoomInDl != null)
        {
            autoCamZoomInDl();
            autoCamZoomInDl = null;
        }
    }

    public float MaxCamDistance
    {
        get
        {
            return maxCamDistance;
        }
    }

    public void SetCamLookAt(Transform t)
    {
        directTransform = t;
        cameraLookAt = t;

        CameraFollow();
        InitCamDist();
    }

    public void SetCam(Transform t)
    {
        cameraTransform = t;
        ResetInitRotation();
    }

    void InitCamDist()
    {
        if (cameraTargetDist < 0.01f)
        {
            cameraTargetDist = Vector3.Distance(cameraLookAt.position, cameraLookAt.position + followOffset);
            maxCamDistance = cameraTargetDist;
            ResetInitRotation();

            if (_preViewDist < 0.01f)
            {
                _preViewDist = cameraTargetDist;
            }
        }
    }

    void Awake()
    {
        gameObject.name = "PgJoystick";
    }

    public void SetSceneRotPoint(int val)
    {
        sceneRotPoint = val;
        UpdateFollowOffset(followOffsetList, false);
    }

    public void ResetSceneRotPoint()
    {
        if(sceneRotPoint != -1)
        {
            SetSceneRotPoint(sceneRotPoint);
        }
    }

    List<Vector3> followOffsetList = new List<Vector3>();
    public void UpdateFollowOffset(List<Vector3> posList, bool setList = true)
    {
        if (sceneRotPoint < 0)
            return;
        if(setList)
        {
            followOffsetList = posList;
        }
        if (posList.Count > 0 && posList.Count > sceneRotPoint)
        {
            followOffset = posList[sceneRotPoint];
            defaultFollow = followOffset;

            zoomType = 0;
            EnableAutoCamRotate = false;
            if(cameraTargetDist > 0.01f)
                StopAutoCamZoomIn();
            else
            {

            }
            CameraFollow();
            ResetInitRotation();                

            cameraTargetDist = -1;
            if (cameraLookAt != null)
            {
                InitCamDist();
            }
        }
    }

    private void ResetInitRotation()
    {
        if (cameraTransform != null)
        {
            rotation = Quaternion.Euler(cameraTransform.eulerAngles.x, cameraTransform.eulerAngles.y, 0);
            defaultRotation = cameraTransform.rotation;
        }
    }

    public void Init(GComponent gct)
    {

        gameObject.name = "PgJoystick";

#if !UNITY_EDITOR
	enableKeySimulation = false;
#endif

    }

    public bool Enable
    {
        get {
            return _enable;
        }
        set {
            _enable = value;
        }
    }

    private void OnMove(EventContext context)
    {
        if (directTransform)
        {
            curDegree = ValueObject.Value<float>(context.data);
            curDegree += 90;
            //Debug.Log("curDegree:" + curDegree);
            DoTurn(curDegree);
        }
        isMove = true;
        EnableAutoCamRotate = false;
    }

    private void OnMoveEnd()
    {
        if (!isMove)
            return;
        if (isMove)
            isMove = false;
        curDegree = -1000;
        if (onMoveEnd != null)
            onMoveEnd(false);
    }

    public void Update()
    {
        if (!_enable)
            return;
        if (enableKeySimulation && directTransform)
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");                
            if(x == 0 && y == 0)
            {
                //if (OldTmpAxis != Vector2.zero)
                if(keySimulationMove)
                    OnMoveEnd();
                keySimulationMove = false;
            }
            else
            {
                float angle = Mathf.Atan2(x, y) * Mathf.Rad2Deg;
                curDegree = angle;
                isMove = true;
                EnableAutoCamRotate = false;
                keySimulationMove = true;
            }
            OldTmpAxis.x = x;
            OldTmpAxis.y = y;
        }
        if(curDegree != -1000)
        {
            DoTurn(curDegree);
        }

        if (autoTargetDist > 0.001f)
        {
            AutoCamZoomIn();
        }
        else
        {
            AutoCamRotate();
        }

        CameraFollow();

        if(isDelayAuto)
        {
            delayAutoCamRotateTime -= Time.deltaTime;
            if(delayAutoCamRotateTime <= 0)
            {
                EnableAutoCamRotate = true;
            }
        }
    }

    void DoTurn(float angle)
    {
        if(enableMoveDir)
        {
            Quaternion target = Quaternion.Euler(new Vector3(0, angle + cameraTransform.localEulerAngles.y, 0));
            directTransform.rotation = Quaternion.Lerp(directTransform.rotation, target, Time.deltaTime * 25);
        }

        if (onMove != null)
            onMove(angle);
    }

    void CameraFollow()
    {
        if (!cameraTransform || !cameraLookAt) return;

        Vector3 lookAtPos = cameraLookAt.position;
        lookAtPos.y += offsetLook;

        cameraTransform.position = lookAtPos + followOffset;
        cameraTransform.LookAt(lookAtPos);
    }

    public Vector3 GetCamLookPos()
    {
        Vector3 lookAtPos = cameraLookAt.position;
        lookAtPos.y += offsetLook;
        return lookAtPos;
    }

    public void LookAtPos(Vector3 pos)
    {
        if (cameraTransform != null)
        {
            Vector3 localOffset = followOffset;

            cameraTransform.position = pos + localOffset;
            cameraTransform.LookAt(pos);
            ResetInitRotation();
        }            
    }

    public void AutoCamZoomIn()
    {
        if(autoTargetDist > 0.001f)
        {
            if (!cameraTransform || !cameraLookAt) return;
            float chgVal = 4 * Time.deltaTime;
            if (cameraTargetDist > autoTargetDist)
                chgVal *= -1;

            Vector3 localOffset = followOffset;
            if (Mathf.Abs(cameraTargetDist - autoTargetDist) < 0.1f)
            {
                //localOffset = autoTargetOffset;
                StopAutoCamZoomIn();
            }
            else
            {
                localOffset += followOffset.normalized * chgVal;
            }
            float dist = Vector3.Distance(cameraLookAt.position, cameraLookAt.position + localOffset);
            cameraTargetDist = dist;
            followOffset = localOffset;
        }
    }

    //public void CameraZoomIn(float chgVal, float targetZoomOffset)
    //{
    //    if (!cameraTransform || !cameraLookAt) return;
    //    isZoomIn = false;
    //    //Debug.Log(curZoomOffset + ";targetZoomOffset:" + targetZoomOffset);
    //    if (curZoomOffset < targetZoomOffset)
    //    {
    //        curZoomOffset += chgVal;
    //        if (curZoomOffset > targetZoomOffset)
    //            curZoomOffset = targetZoomOffset;
    //    }
    //    else if (curZoomOffset > targetZoomOffset)
    //    {
    //        chgVal = -chgVal;
    //        curZoomOffset += chgVal;
    //        if (curZoomOffset < targetZoomOffset)
    //        {
    //            chgVal = curZoomOffset - targetZoomOffset;
    //            curZoomOffset = targetZoomOffset;
    //        }
    //    }
    //    else
    //    {
    //        return;
    //    }
    //    isZoomIn = true;
    //    Vector3 localOffset = followOffset;
    //    localOffset += followOffset.normalized * chgVal;
    //    float dist = Vector3.Distance(cameraLookAt.position, cameraLookAt.position + localOffset);
    //    if (dist < minCamDistance || dist > maxCamDistance)
    //    {
    //        return;
    //    }
    //    cameraTargetDist = dist;
    //    followOffset = localOffset;
    //}

    public void CameraZoomTarget(float rotateX)
    {
        if(zoomType == 0)
        {
            if(rotateX < 20)
            {
                zoomType = 1;
                BeginAutoCamZoomIn(minCamDistance);
            }
        }
        else if (zoomType == 1)
        {
            if (rotateX >= 20)
            {
                zoomType = 0;
                BeginAutoCamZoomIn(MaxCamDistance);
            }
        }
    }

    public void CameraZoomIn(float offset)
    {
        if (!DebugMode)
            return;
        if (!cameraTransform || !cameraLookAt) return;
        Vector3 localOffset = followOffset;
        localOffset += followOffset.normalized * offset;
        float dist = Vector3.Distance(cameraLookAt.position, cameraLookAt.position + localOffset);
        if (dist < minCamDistance || dist > MaxCamDistance)
            return;
        cameraTargetDist = dist;
        followOffset = localOffset;
    }

    public float GetCamRotateOffset()
    {
        if(cameraTransform)
        {
            return cameraTransform.eulerAngles.y;
        }
        return 0;
    }

    public bool EnableAutoCamRotate
    {
        get
        {
            return _enableAutoCamRotation;
        }
        set
        {
            _enableAutoCamRotation = value;
            if(!_enableAutoCamRotation)
            {
                if (isDelayAuto)
                    isDelayAuto = false;
            }
        }
    }

    private bool isDelayAuto = false;
    private float delayAutoCamRotateTime;
    private float delayTime = 1.5f;
    public void DelayAutoCamRotate()
    {
        if (isDelayAuto)
            return;
        if(_enableAutoCamRotation)
            EnableAutoCamRotate = false;
        isDelayAuto = true;
        delayAutoCamRotateTime = delayTime;
    }

    public void AutoCamRotate()
    {
        if (!cameraTransform || !cameraLookAt) return;

        if (!_enableAutoCamRotation)
            return;

        if (cameraTargetDist < 0)
            cameraTargetDist = Vector3.Distance(cameraLookAt.position, cameraLookAt.position + followOffset);

        float dist = cameraLookAt.transform.eulerAngles.y - cameraTransform.eulerAngles.y;

        if (dist < 0.001f && dist > -0.001f)
        {
            delayAutoCamRotateTime = delayTime;
            return;
        }
        if(delayAutoCamRotateTime > 0)
        {
            delayAutoCamRotateTime -= Time.deltaTime;
            //Debug.LogError("delayAutoCamRotateTime:" + Time.time);
            return;
        }

        if (dist > 0.5f || dist < -0.5f)
        {
            if(dist < -180)
                dist = dist / dist * Time.deltaTime * autoRotateSpeed;
            else if(dist > 180)
                dist = -dist / dist * Time.deltaTime * autoRotateSpeed;
            else
                dist = dist / Mathf.Abs( dist ) * Time.deltaTime * autoRotateSpeed;
        }

        //Debug.Log("auto:" + Time.time);
        Quaternion rotation = Quaternion.Euler(cameraTransform.eulerAngles.x, cameraTransform.eulerAngles.y + dist, 0.0f);
        Vector3 disVector = new Vector3(0.0f, 0.0f, -cameraTargetDist);
        Vector3 position = rotation * disVector + cameraLookAt.position;
        cameraTransform.rotation = rotation;
        followOffset = position - cameraLookAt.position;
        this.rotation = Quaternion.Euler(cameraTransform.eulerAngles.x, cameraTransform.eulerAngles.y, 0);
    }

    Quaternion rotation;
    public void CameraRotate(float offset, float offsetY)
    {
        if (!cameraTransform || !cameraLookAt) return;

        if(!_enableRotateY)
        {
            offsetY = 0;
        }
        if (isLockView)
        {
            offsetY = 0;
            // 锁屏可能会导致valueX超过RangeRotateY范围，如果策划调了数值超过最大值 这里需要对应的改一下
        }
        if (cameraTargetDist < 0)
        {
            InitCamDist();
        }

        //Debug.Log("CameraRotate offsetY:" + offsetY);

        float valueX = cameraTransform.eulerAngles.x + offsetY;
        //Debug.Log("valueX1:" + valueX);
        valueX = Mathf.Clamp(valueX, RangeRotateY.x, RangeRotateY.y);
        //Debug.Log("valueX2:" + valueX);

        //Debug.Log("x:" + valueX);
        //Debug.Log("y:" + cameraTransform.eulerAngles.y + offset);
        Quaternion target = Quaternion.Euler(valueX, cameraTransform.eulerAngles.y + offset, 0);
        rotation = Quaternion.Lerp(rotation, target, Time.deltaTime * 5);
        Vector3 disVector = new Vector3(0.0f, 0.0f, -cameraTargetDist);
        Vector3 position = rotation * disVector + cameraLookAt.position;
        cameraTransform.rotation = rotation;
        followOffset = position - cameraLookAt.position;

        CameraFollow();

        if(!DebugMode && !_isMaxView && !isLockView)
            CameraZoomTarget(valueX);
    }

    public void AutoMove(Vector3 targetFollowOffset)
    {
        _enableRotateY = false;
        Vector3 lookAtPos = cameraLookAt.position;
        lookAtPos.y += offsetLook;
        Vector3 camPos = lookAtPos + targetFollowOffset;
        Quaternion targetRotation = Quaternion.LookRotation(cameraLookAt.position - camPos);
        float targetDist = targetFollowOffset.magnitude;//Vector3.Distance(cameraLookAt.position, cameraLookAt.position + targetFollowOffset);
        BeginAutoCamZoomIn(targetDist);

        float angle = Quaternion.Angle(cameraTransform.rotation, targetRotation);
        float duration = angle / 60f;
        cameraTransform.DORotateQuaternion(targetRotation, duration).OnUpdate(() =>
        {
            Vector3 disVector = new Vector3(0.0f, 0.0f, -cameraTargetDist);
            Vector3 position = cameraTransform.rotation * disVector + cameraLookAt.position;
            followOffset = position - cameraLookAt.position;
            CameraFollow();
            this.rotation = Quaternion.Euler(cameraTransform.eulerAngles.x, cameraTransform.eulerAngles.y, 0);
        }).SetEase(Ease.Linear);
    }
}

