using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    public Transform target;    //������ǥ
    public float orbitSpeed;    //�����ӵ�
    Vector3 offset;             //��ǥ���� �Ÿ�

    void Start()
    {
        offset = transform.position - target.position;  //���� ����ź ��ġ���� Ÿ�� ��ġ�� �� ��
    }

    void Update()
    {
        transform.position = target.position + offset;
        //Ÿ�� ������ ȸ���ϴ� �Լ� // ��ġ, ȸ�� ��, ȸ���ϴ� ��ġ
        transform.RotateAround(target.position, Vector3.up, orbitSpeed * Time.deltaTime);
        offset = transform.position - target.position;
    }
}
