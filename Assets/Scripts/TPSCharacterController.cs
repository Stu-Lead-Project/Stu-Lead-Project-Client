using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSCharacterController : MonoBehaviour
{
    [SerializeField] private Transform characterBody; // �ڽ��� ���� ������ ����
    [SerializeField] private Transform cameraArm;     // ī�޶� �� ȸ���� ������ ����

    [SerializeField] private float sensitivity; // ī�޶� ���� �� ����

    void Update()
    {
        LookAround();
        Move();
    }

    /// <summary>
    /// ĳ������ �������� ����ϴ� �Լ�
    /// </summary>
    private void Move()
    {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); // �̵� �Է� ���� ������
        bool isMove = moveInput.magnitude != 0;
        if (isMove) // �����̰� ���� ��
        {
            Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized; // ī�޶��� ������ ���ȭ ���� ����
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized; // ī�޶��� �������� ���ȭ ���� ����
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x; // ī�޶� ���� �̵� ���� ����

            characterBody.forward = moveDir; // ĳ���Ͱ� �̵��� �� �̵� ������ �ٶ󺸰� �ϱ� 
            transform.position += moveDir * Time.deltaTime * 5f; // ��ġ ����
        }
    }

    /// <summary>
    /// ī�޶��� ȸ���� ����ϴ� �Լ�
    /// </summary>
    private void LookAround()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")); // ���α׷��ֿ��� �������� ���簪�� ���� �ַ� "Delta"��� ���� ǥ����
        Vector3 camAngle = cameraArm.rotation.eulerAngles; // ī�޶��� ���� Rotation ���� ���Ϸ� ������ ��ȯ
        float x = camAngle.x - mouseDelta.y;

        if (x < 180f) // ĳ������ ���� ������ ����
        {
            x = Mathf.Clamp(x, -1f, 70f);
        }
        else
        {
            x = Mathf.Clamp(x, 335f, 361f);
        }

        cameraArm.rotation = Quaternion.Euler(camAngle.x - mouseDelta.y * sensitivity, camAngle.y + mouseDelta.x * sensitivity, camAngle.z); // ī�޶��� Rotation �� ��ȯ
    }
}
