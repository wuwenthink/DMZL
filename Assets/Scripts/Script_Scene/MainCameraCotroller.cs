// ======================================================================================
// 文件名         ：    MainCameraCotroller.cs
// 版本号         ：    v2.0.0.0
// 作者           ：    wuwenthink
// 创建日期       ：    2017-7-19
// 最后修改日期   ：    2017-8-4
// ======================================================================================
// 功能描述       ：    摄像机控制
// ======================================================================================

using System.Collections;
using UnityEngine;

/// <summary>
/// 摄像机控制
/// </summary>
public class MainCameraCotroller : MonoBehaviour
{
    /// <summary>
    /// 摄像机要跟随的人物
    /// </summary>
    public Transform character;

    /// <summary>
    /// 摄像机平滑移动的时间
    /// </summary>
    public float smoothTime = 0.01f;

    private Vector3 cameraVelocity = Vector3.zero;

    /// <summary>
    /// 当前的摄像机
    /// </summary>
    private Camera currCamera;

    /// <summary>
    /// 是否取消震动
    /// </summary>
    public bool cancelShake = false;

    /// <summary>
    /// 摄像机移动的目标位置
    /// </summary>
    private Vector3 targetPosition;

    /// <summary>
    /// 是否允许摄像机自由跟随
    /// </summary>
    private static bool cameraFree = true;

    // TOUPDATE 测试用开关
    public bool zoomOut = false;

    private void Awake ()
    {
        // 获取主摄像机
        currCamera = GetComponent<Camera> ();
    }

    private void Start ()
    {
        targetPosition = new Vector3 (0, 0, -1);
    }

    private void LateUpdate ()
    {
        // 默认情形：摄像机随角色移动
        if (cameraFree)
            transform.position = Vector3.SmoothDamp (transform.position, character.position + targetPosition, ref cameraVelocity, smoothTime);
    }

    private void Update ()
    {
        if (zoomOut)
        {
            ZoomOut ();
            zoomOut = false;
        }
    }

    /// <summary>
    /// 摄像机震动
    /// </summary>
    /// <param name="shakeStrength">震动幅度</param>
    /// <param name="rate">震动频率</param>
    /// <param name="shakeTime">震动时长</param>
    /// <returns></returns>
    private IEnumerator ShakeCamera (float shakeStrength = 0.2f, float rate = 14, float shakeTime = 0.4f)
    {
        float t = 0;
        float speed = 1 / shakeTime;
        Vector3 orgPosition = transform.localPosition;
        while (t < 1 && !cancelShake)
        {
            t += Time.deltaTime * speed;
            transform.position = orgPosition + new Vector3 (Mathf.Sin (rate * t), Mathf.Cos (rate * t), 0) * Mathf.Lerp (shakeStrength, 0, t);
            yield return null;
        }
        cancelShake = false;
        transform.position = orgPosition;
    }

    /// <summary>
    /// 摄像机拉近
    /// <param name="distance">拉近的距离</param>
    /// <param name="time">拉近的时间</param>
    /// </summary>
    private void ZoomIn (float distance = 3f, float time = 1f)
    {
        cameraFree = false;
        targetPosition = new Vector3 (0, 0, targetPosition.z + distance);
        smoothTime = time;
        //transform.position = Vector3.SmoothDamp(transform.position, character.position + targetPosition, ref cameraVelocity, time);
        cameraFree = true;
    }

    /// <summary>
    /// 摄像机拉远
    /// <param name="distance">拉远的距离</param>
    /// <param name="time">拉远的时间</param>
    /// </summary>
    private void ZoomOut (float distance = 5f, float time = 3f)
    {
        cameraFree = false;
        targetPosition = new Vector3 (0, 0, targetPosition.z - distance);
        for (float i = 0; i < time * 10000 / Time.deltaTime; i += Time.deltaTime)
            transform.position = Vector3.SmoothDamp (transform.position, character.position + targetPosition, ref cameraVelocity, time);
        //cameraFree = true;
    }

    /// <summary>
    /// 摄像机移动
    /// </summary>
    /// <param name="position">要移动到的位置</param>
    /// <param name="time">移动的时间</param>
    private void Zoom (Vector3 position, float time = 2f)
    {
        cameraFree = false;
        transform.position = Vector3.SmoothDamp (transform.position, position, ref cameraVelocity, time);
        cameraFree = true;
    }
}