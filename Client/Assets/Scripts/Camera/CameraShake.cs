//
// CameraShake.cs
//
// Author(s):
//       Josh Montoute <josh@thinksquirrel.com>
//
// Copyright (c) 2012-2014 Thinksquirrel Software, LLC
//
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[AddComponentMenu("Thinksquirrel Utilities/Camera Shake")]
public class CameraShake : MonoBehaviour
{
    [SerializeField]
    List<Camera> m_Cameras = new List<Camera>();
    [SerializeField]
    ShakeType m_ShakeType = ShakeType.CameraMatrix;
    [SerializeField]
    int m_NumberOfShakes = 2;
    [SerializeField]
    Vector3 m_ShakeAmount = Vector3.one;
    [SerializeField]
    Vector3 m_RotationAmount = Vector3.one;
    [SerializeField]
    float m_Distance = 00.10f;
    [SerializeField]
    float m_Speed = 50.00f;
    [SerializeField]
    float m_Decay = 00.20f;
    [SerializeField]
    float m_GuiShakeModifier = 01.00f;
    [SerializeField]
    bool m_MultiplyByTimeScale = true;

    // Analysis disable ConvertToAutoProperty
    /// <summary>
    /// The cameras to shake.
    /// </summary>
    public List<Camera> cameras { get { return m_Cameras; } set { m_Cameras = value; } }
    /// <summary>
    /// The type of shake to perform (camera matrix or local position).
    /// </summary>
    public ShakeType shakeType { get { return m_ShakeType; } set { m_ShakeType = value; } }
    /// <summary>
    /// The maximum number of shakes to perform.
    /// </summary>
    public int numberOfShakes { get { return m_NumberOfShakes; } set { m_NumberOfShakes = value; } }
    /// <summary>
    /// The amount to shake in each direction.
    /// </summary>
    public Vector3 shakeAmount { get { return m_ShakeAmount; } set { m_ShakeAmount = value; } }
    /// <summary>
    /// The amount to rotate in each direction.
    /// </summary>
    public Vector3 rotationAmount { get { return m_RotationAmount; } set { m_RotationAmount = value; } }
    /// <summary>
    /// The initial distance for the first shake.
    /// </summary>
    public float distance { get { return m_Distance; } set { m_Distance = value; } }
    /// <summary>
    /// The speed multiplier for the shake.
    /// </summary>
    public float speed { get { return m_Speed; } set { m_Speed = value; } }
    /// <summary>
    /// The decay speed (between 0 and 1). Higher values will stop shaking sooner.
    /// </summary>
    public float decay { get { return m_Decay; } set { m_Decay = value; } }
    /// <summary>
    /// The modifier applied to speed in order to shake the GUI.
    /// </summary>
    public float guiShakeModifier { get { return m_GuiShakeModifier; } set { m_GuiShakeModifier = value; } }
    /// <summary>
    /// If true, multiplies the final shake speed by the time scale.
    /// </summary>
    public bool multiplyByTimeScale { get { return m_MultiplyByTimeScale; } set { m_MultiplyByTimeScale = value; } }
    // Analysis restore ConvertToAutoProperty

    // Shake type
    public enum ShakeType
    {
        CameraMatrix,
        LocalPosition,
    }
    // Shake rect (for GUI)
    // Analysis disable FieldCanBeMadeReadOnly.Local
    Rect m_ShakeRect;
    // Analysis restore FieldCanBeMadeReadOnly.Local

    // States
    bool m_Shaking;
    bool m_Cancelling;
    readonly List<Vector3> m_MatrixOffsetCache = new List<Vector3>(10);
    readonly List<Quaternion> m_MatrixRotationCache = new List<Quaternion>(10);
    readonly List<Vector3> m_OffsetCache = new List<Vector3>(10);
    readonly List<Quaternion> m_RotationCache = new List<Quaternion>(10);
    readonly List<bool> m_IgnoreMatrixCache = new List<bool>(10);
    readonly List<bool> m_IgnorePositionCache = new List<bool>(10);

    internal class ShakeState
    {
        internal readonly ShakeType _shakeType;
        internal readonly Vector3 _startPosition;
        internal readonly Quaternion _startRotation;
        internal readonly Vector2 _guiStartPosition;
        internal Vector3 _shakePosition;
        internal Quaternion _shakeRotation;
        internal Vector2 _guiShakePosition;

        internal ShakeState(ShakeType shakeType, Vector3 position, Quaternion rotation, Vector2 guiPosition)
        {
            _shakeType = shakeType;
            _startPosition = position;
            _startRotation = rotation;
            _guiStartPosition = guiPosition;
            _shakePosition = position;
            _shakeRotation = rotation;
            _guiShakePosition = guiPosition;
        }
    }

    readonly Dictionary<Camera, List<ShakeState>> m_States = new Dictionary<Camera, List<ShakeState>>();
    readonly Dictionary<Camera, int> m_ShakeCount = new Dictionary<Camera, int>();

    // Minimum shake values
    const float minShakeValue = 0.001f;
    const float minRotationValue = 0.001f;

    #region Components
    readonly static List<CameraShake> m_Components = new List<CameraShake>();

    [System.Obsolete("Use CameraShake.GetComponents() to get all Camera Shake components")]
    public static CameraShake instance { get { return m_Components.Count > 0 ? m_Components[0] : null; } }

    void OnEnable()
    {
        if (cameras.Count < 1)
        {
            if (GetComponent<Camera>())
                cameras.Add(GetComponent<Camera>());
        }

        if (cameras.Count < 1)
        {
            if (Camera.main)
                cameras.Add(Camera.main);
        }

        if (cameras.Count < 1)
        {
            Debug.LogError("Camera Shake: No cameras assigned in the inspector!");
        }

        m_Components.Add(this);
    }

    void OnDisable()
    {
        m_Components.Remove(this);
    }

    public static CameraShake[] GetComponents()
    {
        return m_Components.ToArray();
    }

    #endregion

    #region Static properties
    public static bool isShaking
    {
        get
        {
            for (int i = 0, m_ComponentsCount = m_Components.Count; i < m_ComponentsCount; i++)
            {
                var component = m_Components[i];
                if (component.IsShaking())
                    return true;
            }

            return false;
        }
    }

    [System.Obsolete("Use IsCancelling method on individual Camera Shake components")]
    public static bool isCancelling
    {
        get
        {
            var inst = m_Components.Count > 0 ? m_Components[0] : null;
            return inst != null && inst.IsCancelling();
        }
    }

    #endregion

    #region Static methods

    public static void ShakeAll()
    {
        for (int i = 0, m_ComponentsCount = m_Components.Count; i < m_ComponentsCount; i++)
        {
            var component = m_Components[i];
            component.Shake();
        }
    }

    public static void ShakeAll(ShakeType shakeType, int numberOfShakes, Vector3 shakeAmount, Vector3 rotationAmount, float distance, float speed, float decay, float guiShakeModifier, bool multiplyByTimeScale)
    {
        for (int i = 0, m_ComponentsCount = m_Components.Count; i < m_ComponentsCount; i++)
        {
            var component = m_Components[i];
            component.Shake(shakeType, numberOfShakes, shakeAmount, rotationAmount, distance, speed, decay, guiShakeModifier, multiplyByTimeScale);
        }
    }

    public static void ShakeAll(System.Action callback)
    {
        for (int i = 0, m_ComponentsCount = m_Components.Count; i < m_ComponentsCount; i++)
        {
            var component = m_Components[i];
            component.Shake(callback);
        }
    }

    public static void ShakeAll(ShakeType shakeType, int numberOfShakes, Vector3 shakeAmount, Vector3 rotationAmount, float distance, float speed, float decay, float guiShakeModifier, bool multiplyByTimeScale, System.Action callback)
    {
        for (int i = 0, m_ComponentsCount = m_Components.Count; i < m_ComponentsCount; i++)
        {
            var component = m_Components[i];
            component.Shake(shakeType, numberOfShakes, shakeAmount, rotationAmount, distance, speed, decay, guiShakeModifier, multiplyByTimeScale, callback);
        }
    }

    public static void CancelAllShakes()
    {
        for (int i = 0, m_ComponentsCount = m_Components.Count; i < m_ComponentsCount; i++)
        {
            var component = m_Components[i];
            component.CancelShake();
        }
    }

    public static void CancelAllShakes(float time)
    {
        for (int i = 0, m_ComponentsCount = m_Components.Count; i < m_ComponentsCount; i++)
        {
            var component = m_Components[i];
            component.CancelShake(time);
        }
    }

    #endregion

    #region Events

    // Analysis disable InconsistentNaming
    // Analysis disable DelegateSubtraction
    [System.Obsolete("Use CameraShake.onStartShaking instead")]
    public event System.Action cameraShakeStarted { add { onStartShaking += value; } remove { onStartShaking -= value; } }
    [System.Obsolete("Use CameraShake.onEndShaking")]
    public event System.Action allCameraShakesCompleted { add { onEndShaking += value; } remove { onEndShaking -= value; } }
    // Analysis restore DelegateSubtraction
    // Analysis restore InconsistentNaming

    /// <summary>
    /// Occurs when a camera starts shaking.
    /// </summary>
    public event System.Action onStartShaking;
    /// <summary>
    /// Occurs when a camera has completely stopped shaking and has been reset to its original position.
    /// </summary>
    public event System.Action onEndShaking;
    /// <summary>
    /// Occurs before every individual camera shake.
    /// </summary>	
    public event System.Action onPreShake;
    /// <summary>
    /// Occurs after every individual camera shake.
    /// </summary>
    public event System.Action onPostShake;

    #endregion

    #region Public methods

    public bool IsShaking()
    {
        return m_Shaking;
    }

    public bool IsCancelling()
    {
        return m_Cancelling;
    }

    public void Shake()
    {
        var seed = Random.insideUnitSphere;

        for (int i = 0, camerasCount = cameras.Count; i < camerasCount; i++)
        {
            Camera cam = cameras[i];
            StartCoroutine(DoShake_Internal(cam, seed, shakeType, numberOfShakes, shakeAmount, rotationAmount, distance, speed, decay, guiShakeModifier, multiplyByTimeScale, null));
        }
    }

    public void Shake(ShakeType shakeType, int numberOfShakes, Vector3 shakeAmount, Vector3 rotationAmount, float distance, float speed, float decay, float guiShakeModifier, bool multiplyByTimeScale)
    {
        var seed = Random.insideUnitSphere;

        for (int i = 0, camerasCount = cameras.Count; i < camerasCount; i++)
        {
            Camera cam = cameras[i];
            StartCoroutine(DoShake_Internal(cam, seed, shakeType, numberOfShakes, shakeAmount, rotationAmount, distance, speed, decay, guiShakeModifier, multiplyByTimeScale, null));
        }
    }

    public void Shake(System.Action callback)
    {
        var seed = Random.insideUnitSphere;

        for (int i = 0, camerasCount = cameras.Count; i < camerasCount; i++)
        {
            Camera cam = cameras[i];
            StartCoroutine(DoShake_Internal(cam, seed, shakeType, numberOfShakes, shakeAmount, rotationAmount, distance, speed, decay, guiShakeModifier, multiplyByTimeScale, callback));
        }
    }

    public void Shake(ShakeType shakeType, int numberOfShakes, Vector3 shakeAmount, Vector3 rotationAmount, float distance, float speed, float decay, float guiShakeModifier, bool multiplyByTimeScale, System.Action callback)
    {
        var seed = Random.insideUnitSphere;

        for (int i = 0, camerasCount = cameras.Count; i < camerasCount; i++)
        {
            Camera cam = cameras[i];
            StartCoroutine(DoShake_Internal(cam, seed, shakeType, numberOfShakes, shakeAmount, rotationAmount, distance, speed, decay, guiShakeModifier, multiplyByTimeScale, callback));
        }
    }

    public void CancelShake()
    {
        if (m_Shaking && !m_Cancelling)
        {
            m_Shaking = false;
            StopAllCoroutines();
            for (int i = 0, camerasCount = cameras.Count; i < camerasCount; i++)
            {
                Camera cam = cameras[i];
                if (m_ShakeCount.ContainsKey(cam))
                {
                    m_ShakeCount[cam] = 0;
                }
                ResetState(cam);
            }
        }
    }

    public void CancelShake(float time)
    {
        if (m_Shaking && !m_Cancelling)
        {
            StopAllCoroutines();
            StartCoroutine(DoResetState(cameras, m_ShakeCount, time));
        }
    }

    public void BeginShakeGUI()
    {
        CheckShakeRect();
        GUI.BeginGroup(m_ShakeRect);
    }

    public void EndShakeGUI()
    {
        GUI.EndGroup();
    }

    public void BeginShakeGUILayout()
    {
        CheckShakeRect();
        GUILayout.BeginArea(m_ShakeRect);
    }

    public void EndShakeGUILayout()
    {
        GUILayout.EndArea();
    }

    #endregion

    #region Private methods

    void OnDrawGizmosSelected()
    {
        for (int i = 0, camerasCount = cameras.Count; i < camerasCount; i++)
        {
            Camera cam = cameras[i];

            if (!cam)
                continue;

            if (IsShaking())
            {
                var offset = cam.worldToCameraMatrix.GetColumn(3);
                offset.z *= -1;
                offset = cam.transform.position + cam.transform.TransformPoint(offset);
                var rot = QuaternionFromMatrix(cam.worldToCameraMatrix.inverse * Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(1, 1, -1)));
                var matrix = Matrix4x4.TRS(offset, rot, cam.transform.lossyScale);
                Gizmos.matrix = matrix;
            }
            else
            {
                var matrix = Matrix4x4.TRS(cam.transform.position, cam.transform.rotation, cam.transform.lossyScale);
                Gizmos.matrix = matrix;
            }
            Gizmos.DrawWireCube(Vector3.zero, shakeAmount);
            Gizmos.color = Color.cyan;
            if (cam.orthographic)
            {
                var pos = new Vector3(0, 0, (cam.nearClipPlane + cam.farClipPlane) / 2f);
                var size = new Vector3(cam.orthographicSize / cam.aspect, cam.orthographicSize * 2f, cam.farClipPlane - cam.nearClipPlane);
                Gizmos.DrawWireCube(pos, size);
            }
            else
            {
                Gizmos.DrawFrustum(Vector3.zero, cam.fieldOfView, cam.farClipPlane, cam.nearClipPlane, (.7f / cam.aspect));
            }
        }
    }

    IEnumerator DoShake_Internal(Camera cam, Vector3 seed, ShakeType shakeType, int numberOfShakes, Vector3 shakeAmount, Vector3 rotationAmount, float distance, float speed, float decay, float guiShakeModifier, bool multiplyByTimeScale, System.Action callback)
    {
        // Wait for async cancel operations to complete
        if (m_Cancelling)
            yield return null;

        // Set random values
        var mod1 = seed.x > .5f ? 1 : -1;
        var mod2 = seed.y > .5f ? 1 : -1;
        var mod3 = seed.z > .5f ? 1 : -1;

        // First shake
        if (!m_Shaking)
        {
            m_Shaking = true;

            if (onStartShaking != null)
                onStartShaking();
        }

        if (m_ShakeCount.ContainsKey(cam))
            m_ShakeCount[cam]++;
        else
            m_ShakeCount.Add(cam, 1);

        if (onPreShake != null)
            onPreShake();

        // Pixel width is always based on the first camera
        float pixelWidth = GetPixelWidth(cameras[0].transform, cameras[0]);

        // Set other values
        var cachedTransform = cam.transform;
        Vector3 camOffset;
        Quaternion camRot;

        int currentShakes = numberOfShakes;
        float shakeDistance = distance;
        float rotationStrength = 1;

        float startTime = Time.time;
        float scale = multiplyByTimeScale ? Time.timeScale : 1;
        float pixelScale = pixelWidth * guiShakeModifier * scale;
        Vector3 start1 = Vector2.zero;
        Quaternion startR = Quaternion.identity;
        Vector2 start2 = Vector2.zero;

        var state = new ShakeState(shakeType, cachedTransform.position, cachedTransform.rotation, new Vector2(m_ShakeRect.x, m_ShakeRect.y));
        List<ShakeState> stateList;

        if (m_States.TryGetValue(cam, out stateList))
        {
            stateList.Add(state);
        }
        else
        {
            stateList = new List<ShakeState>();
            stateList.Add(state);
            m_States.Add(cam, stateList);
        }

        // Main loop
        while (currentShakes > 0)
        {
            // Early break when rotation is less than the minimum value.
            if (Mathf.Abs(rotationAmount.sqrMagnitude) > Vector3.kEpsilon && rotationStrength <= minRotationValue)
                break;

            // Early break when shake amount is less than the minimum value.
            if (Mathf.Abs(shakeAmount.sqrMagnitude) > Vector3.kEpsilon && Mathf.Abs(distance) > Vector3.kEpsilon && shakeDistance <= minShakeValue)
                break;

            var timer = (Time.time - startTime) * speed;

            state._shakePosition = start1 + new Vector3(
                mod1 * Mathf.Sin(timer) * (shakeAmount.x * shakeDistance * scale),
                mod2 * Mathf.Cos(timer) * (shakeAmount.y * shakeDistance * scale),
                mod3 * Mathf.Sin(timer) * (shakeAmount.z * shakeDistance * scale));

            state._shakeRotation = startR * Quaternion.Euler(
                mod1 * Mathf.Cos(timer) * (rotationAmount.x * rotationStrength * scale),
                mod2 * Mathf.Sin(timer) * (rotationAmount.y * rotationStrength * scale),
                mod3 * Mathf.Cos(timer) * (rotationAmount.z * rotationStrength * scale));

            state._guiShakePosition = new Vector2(
                start2.x - (mod1 * Mathf.Sin(timer) * (shakeAmount.x * shakeDistance * pixelScale)),
                start2.y - (mod2 * Mathf.Cos(timer) * (shakeAmount.y * shakeDistance * pixelScale)));

            camOffset = GetGeometricAvg(stateList, true);
            camRot = GetAvgRotation(stateList);
            NormalizeQuaternion(ref camRot);

            switch (state._shakeType)
            {
                case ShakeType.CameraMatrix:
                    var m = Matrix4x4.TRS(camOffset, camRot, new Vector3(1, 1, -1));
                    cam.worldToCameraMatrix = m * cachedTransform.worldToLocalMatrix;
                    break;
                case ShakeType.LocalPosition:
                    cachedTransform.localPosition = camOffset;
                    cachedTransform.localRotation = camRot;
                    break;
            }

            var avg = GetGeometricAvg(stateList, false);

            m_ShakeRect.x = avg.x;
            m_ShakeRect.y = avg.y;

            if (timer > Mathf.PI * 2)
            {
                startTime = Time.time;
                shakeDistance *= (1 - Mathf.Clamp01(decay));
                rotationStrength *= (1 - Mathf.Clamp01(decay));
                currentShakes--;
            }
            yield return null;
        }

        // End conditions
        m_ShakeCount[cam]--;

        if (onPostShake != null)
            onPostShake();

        // Last shake
        if (m_ShakeCount[cam] == 0)
        {
            m_Shaking = false;
            ResetState(cam);

            if (onEndShaking != null)
            {
                onEndShaking();
            }
        }
        else
        {
            stateList.Remove(state);
        }

        if (callback != null)
            callback();
    }

    void CheckShakeRect()
    {
        if (Mathf.Abs(Screen.width - m_ShakeRect.width) > Vector3.kEpsilon || Mathf.Abs(Screen.height - m_ShakeRect.height) > Vector3.kEpsilon)
        {
            m_ShakeRect.width = Screen.width;
            m_ShakeRect.height = Screen.height;
        }
    }

    void ResetState(Camera cam)
    {
        cam.ResetWorldToCameraMatrix();

        m_ShakeRect.x = 0;
        m_ShakeRect.y = 0;

        m_States[cam].Clear();
    }

    IEnumerator DoResetState(IList<Camera> cameras, IDictionary<Camera, int> shakeCount, float time)
    {
        m_MatrixOffsetCache.Clear();
        m_MatrixRotationCache.Clear();
        m_OffsetCache.Clear();
        m_RotationCache.Clear();
        m_IgnoreMatrixCache.Clear();
        m_IgnorePositionCache.Clear();

        for (int i = 0, camerasCount = cameras.Count; i < camerasCount; i++)
        {
            var cam = cameras[i];
            var cachedTransform = cam.transform;
            m_MatrixOffsetCache.Add((Vector3)((cam.worldToCameraMatrix * cachedTransform.worldToLocalMatrix.inverse).GetColumn(3)));
            m_MatrixRotationCache.Add(QuaternionFromMatrix((cam.worldToCameraMatrix * cachedTransform.worldToLocalMatrix.inverse).inverse * Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(1, 1, -1))));
            m_OffsetCache.Add(cachedTransform.localPosition);
            m_RotationCache.Add(cachedTransform.localRotation);

            if (shakeCount.ContainsKey(cam))
            {
                shakeCount[cam] = 0;
            }

            var ignoreMatrix = true;
            var ignorePosition = true;
            var states = m_States[cam];

            for (int j = 0, statesCount = states.Count; j < statesCount; j++)
            {
                var state = states[j];

                if (state._shakeType == ShakeType.CameraMatrix)
                {
                    ignoreMatrix = false;
                }
                else
                {
                    ignorePosition = false;
                }

                if (!(ignoreMatrix || ignorePosition))
                    break;
            }

            m_IgnoreMatrixCache.Add(ignoreMatrix);
            m_IgnorePositionCache.Add(ignorePosition);

            m_States[cam].Clear();
        }

        float t = 0;
        float x = m_ShakeRect.x, y = m_ShakeRect.y;
        m_Cancelling = true;
        while (t < time)
        {
            int i = 0;
            for (int j = 0, camerasCount = cameras.Count; j < camerasCount; j++)
            {
                var cam = cameras[j];
                var cachedTransform = cam.transform;
                m_ShakeRect.x = Mathf.Lerp(x, 0, t / time);
                m_ShakeRect.y = Mathf.Lerp(y, 0, t / time);

                if (!m_IgnoreMatrixCache[j])
                {
                    var matrixPos = Vector3.Lerp(m_MatrixOffsetCache[i], Vector3.zero, t / time);
                    var matrixRot = Quaternion.Slerp(m_MatrixRotationCache[i], cachedTransform.rotation, t / time);
                    var m = Matrix4x4.TRS(matrixPos, matrixRot, new Vector3(1, 1, -1));
                    cam.worldToCameraMatrix = m * cachedTransform.worldToLocalMatrix;
                }
                if (!m_IgnorePositionCache[j])
                {
                    var pos = Vector3.Lerp(m_OffsetCache[i], Vector3.zero, t / time);
                    var rot = Quaternion.Slerp(m_RotationCache[i], Quaternion.identity, t / time);
                    cachedTransform.localPosition = pos;
                    cachedTransform.localRotation = rot;
                }
                i++;
            }
            t += Time.deltaTime;
            yield return null;
        }

        for (int i = 0, camerasCount = cameras.Count; i < camerasCount; i++)
        {
            var cam = cameras[i];

            if (!m_IgnoreMatrixCache[i])
                cam.ResetWorldToCameraMatrix();

            if (!m_IgnorePositionCache[i])
            {
                cam.transform.localPosition = Vector3.zero;
                cam.transform.localRotation = Quaternion.identity;
            }

            m_ShakeRect.x = 0;
            m_ShakeRect.y = 0;
        }
        m_Shaking = false;
        m_Cancelling = false;
    }

    #endregion

    #region Math helpers
    static Vector3 GetGeometricAvg(IList<ShakeState> states, bool position)
    {
        float x = 0, y = 0, z = 0, l = states.Count;

        for (int i = 0, statesCount = states.Count; i < statesCount; i++)
        {
            ShakeState state = states[i];
            if (position)
            {
                x -= state._shakePosition.x;
                y -= state._shakePosition.y;
                z -= state._shakePosition.z;
            }
            else
            {
                x += state._guiShakePosition.x;
                y += state._guiShakePosition.y;
            }
        }

        return new Vector3(x / l, y / l, z / l);
    }

    static Quaternion GetAvgRotation(IList<ShakeState> states)
    {
        var avg = new Quaternion(0, 0, 0, 0);

        for (int i = 0, statesCount = states.Count; i < statesCount; i++)
        {
            ShakeState state = states[i];
            if (Quaternion.Dot(state._shakeRotation, avg) > 0)
            {
                avg.x += state._shakeRotation.x;
                avg.y += state._shakeRotation.y;
                avg.z += state._shakeRotation.z;
                avg.w += state._shakeRotation.w;
            }
            else
            {
                avg.x += -state._shakeRotation.x;
                avg.y += -state._shakeRotation.y;
                avg.z += -state._shakeRotation.z;
                avg.w += -state._shakeRotation.w;
            }
        }

        var mag = Mathf.Sqrt(avg.x * avg.x + avg.y * avg.y + avg.z * avg.z + avg.w * avg.w);

        if (mag > 0.0001f)
        {
            avg.x /= mag;
            avg.y /= mag;
            avg.z /= mag;
            avg.w /= mag;
        }
        else
        {
            avg = states[0]._shakeRotation;
        }

        return avg;
    }
    static float GetPixelWidth(Transform cachedTransform, Camera cachedCamera)
    {
        var position = cachedTransform.position;
        var screenPos = cachedCamera.WorldToScreenPoint(position - cachedTransform.forward * .01f);
        Vector3 offset = screenPos;

        if (screenPos.x > 0)
            offset -= Vector3.right;
        else
            offset += Vector3.right;

        if (screenPos.y > 0)
            offset -= Vector3.up;
        else
            offset += Vector3.up;

        offset = cachedCamera.ScreenToWorldPoint(offset);

        return 1f / (cachedTransform.InverseTransformPoint(position) - cachedTransform.InverseTransformPoint(offset)).magnitude;
    }

    static Quaternion QuaternionFromMatrix(Matrix4x4 m)
    {
        return Quaternion.LookRotation(m.GetColumn(2), m.GetColumn(1));
    }

    static void NormalizeQuaternion(ref Quaternion q)
    {
        float sum = 0;

        for (int i = 0; i < 4; ++i)
            sum += q[i] * q[i];

        float magnitudeInverse = 1 / Mathf.Sqrt(sum);

        for (int i = 0; i < 4; ++i)
            q[i] *= magnitudeInverse;
    }

    #endregion

}