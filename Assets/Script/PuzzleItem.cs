using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleItem : MonoBehaviour
{
    public enum Type { rockitem };
    public Type type;
    public bool redstatus = false;
    public bool bluestatus = false;


    Rigidbody rigid;
    //SphereCollider sphereCollider;
    Material mat;
    PuzzleEvent puzzleEvent;
    GameObject nearObject;

    void Awake()    //�ʱ�ȭ
    {
        puzzleEvent = GetComponent<PuzzleEvent>();

        rigid = GetComponent<Rigidbody>();
        mat = GetComponent<MeshRenderer>().material;
        //������Ʈ�� �ݶ��̴��� ù��°�͸� �������Ƿ� is Trigger�� ���Ե��� ���� �ݶ��̴��� ���� �ö󰡾���
        //sphereCollider = GetComponent<SphereCollider>();
    }

    void Update()
    {

    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "rockItem")
        {
            rigid.isKinematic = true;   //���̻� �ܺ� ����ȿ���� ���ؼ� �������� ����
                                        // sphereCollider.enabled = false;
        }
        /*
        if (collision.gameObject.tag == "fireplayer")
        {
            mat.color = Color.red;
        }
        if (collision.gameObject.tag == "waterplayer")
        {
            mat.color = Color.blue;
        }
        */
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "fireplayer")
        {
            redstatus = true;
            mat.color = Color.red;
            bluestatus = false;
        }
        if (other.tag == "waterplayer")
        {
            bluestatus = true;
            mat.color = Color.blue;
            redstatus = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "fireplayer")
            nearObject = other.gameObject;

        if (other.tag == "waterplayer")
            nearObject = other.gameObject;
    }

    void OnTriggerExit(Collider other)
    {
        nearObject = null;
    }


}
