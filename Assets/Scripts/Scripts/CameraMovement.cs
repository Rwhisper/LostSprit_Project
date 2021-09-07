using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Transform objectTofollow; //���� ������Ʈ ����
    public float followSpeed = 10f; //���󰡴� �ӵ�
    public float sensitivity = 100f; //���콺 ����
    public float clampAngle = 70f; // ī�޶� ���� ����

    private float rotX; //���콺 ��
    private float rotY;

    public Transform realCamera; //ī�޶��� ����
    public Vector3 dirNormalized; //����
    public Vector3 finalDir; //��������
    public float minDistance; //�ּҰŸ�
    public float maxDistance; //�ִ�Ÿ�
    public float finalDistance; //�����Ÿ�
    public float smoothness = 10f;

    // Start is called before the first frame update
    void Start()
    {
        rotX = transform.localRotation.eulerAngles.x; //�ʱ�ȭ
        rotY = transform.localRotation.eulerAngles.y; 

        dirNormalized = realCamera.localPosition.normalized; //�ʱ�ȭ normalized = ũ�� 0����(���⸸ ����)
        finalDistance = realCamera.localPosition.magnitude; //magnitude = ũ��
    }

    // Update is called once per frame
    void Update()
    {
        rotX += -(Input.GetAxis("Mouse Y")) * sensitivity * Time.deltaTime; //���� * ��������� �ð�
        rotY += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime; //y���� ���콺�� x���� �׷��� ����

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);
        Quaternion rot = Quaternion.Euler(rotX, rotY, 0);
        transform.rotation = rot;

        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate() //ī�޶� ������
    {
        transform.position = Vector3.MoveTowards(transform.position, objectTofollow.position, followSpeed * Time.deltaTime);

        finalDir = transform.TransformPoint(dirNormalized * maxDistance);

        RaycastHit hit; //��ֹ�

        
        if(Physics.Linecast(transform.position, finalDir, out hit))
        {
            finalDistance = Mathf.Clamp(hit.distance, minDistance, minDistance);
        }
        else
        {
            finalDistance = maxDistance;
        }
        realCamera.localPosition = Vector3.Lerp(realCamera.localPosition, dirNormalized * finalDistance, Time.deltaTime * smoothness);

    }
}
