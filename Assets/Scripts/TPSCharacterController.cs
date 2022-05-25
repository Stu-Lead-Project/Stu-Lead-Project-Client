using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSCharacterController : MonoBehaviour
{
    [SerializeField] private Transform characterBody; // 박스맨 모델을 관리할 변수
    [SerializeField] private Transform cameraArm;     // 카메라 암 회전을 관리할 변수

    [SerializeField] private float sensitivity; // 카메라 감도 값 변수

    void Update()
    {
        LookAround();
        Move();
    }

    /// <summary>
    /// 캐릭터의 움직임을 담당하는 함수
    /// </summary>
    private void Move()
    {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); // 이동 입력 값을 가져옴
        bool isMove = moveInput.magnitude != 0;
        if (isMove) // 움직이고 있을 때
        {
            Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized; // 카메라의 정면을 평면화 시켜 저장
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized; // 카메라의 오른쪽을 평면화 시켜 저장
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x; // 카메라를 통해 이동 방향 저장

            characterBody.forward = moveDir; // 캐릭터가 이동할 때 이동 방향을 바라보게 하기 
            transform.position += moveDir * Time.deltaTime * 5f; // 위치 변경
        }
    }

    /// <summary>
    /// 카메라의 회전을 담당하는 함수
    /// </summary>
    private void LookAround()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")); // 프로그래밍에서 이전값과 현재값의 차를 주로 "Delta"라는 용어로 표현함
        Vector3 camAngle = cameraArm.rotation.eulerAngles; // 카메라의 암의 Rotation 값을 오일러 각으로 변환
        float x = camAngle.x - mouseDelta.y;

        if (x < 180f) // 캐릭터의 상하 움직임 제한
        {
            x = Mathf.Clamp(x, -1f, 70f);
        }
        else
        {
            x = Mathf.Clamp(x, 335f, 361f);
        }

        cameraArm.rotation = Quaternion.Euler(camAngle.x - mouseDelta.y * sensitivity, camAngle.y + mouseDelta.x * sensitivity, camAngle.z); // 카메라의 Rotation 값 변환
    }
}
