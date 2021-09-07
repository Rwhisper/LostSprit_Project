using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum Type { Ammo, Coin, Grenade, Heart, Weapon };
    public Type type;
    public int value;

    Rigidbody rigid;
    SphereCollider sphereCollider;
    
    void Awake()    //�ʱ�ȭ
    {
        rigid = GetComponent<Rigidbody>();
        //������Ʈ�� �ݶ��̴��� ù��°�͸� �������Ƿ� is Trigger�� ���Ե��� ���� �ݶ��̴��� ���� �ö󰡾���
        sphereCollider = GetComponent<SphereCollider>(); 
    }

    void Update()
    {
        transform.Rotate(Vector3.up * 20 * Time.deltaTime); //ȸ��
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            rigid.isKinematic = true;   //���̻� �ܺ� ����ȿ���� ���ؼ� �������� ����
            sphereCollider.enabled = false;
        }
    }
}
