using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEX : MonoBehaviour
{
    public float speed;
    public GameObject[] weapons;    //����迭
    public bool[] hasWeapons;       //���� �������ִ� ����迭
    public GameObject[] grenades;
    public GameObject[] itemObj;
    public Transform[] itemPos;
    Transform trans;

    public int ammo;
    public int coin;
    public int health;
    public int hasGrenades;
    public int score;

    public int cntitem = 0;
    public int locitem = 0;

    public int maxAmmo;
    public int maxCoin;
    public int maxHealth;
    public int maxHasGrenades;

    float hAxis;
    float vAxis;

    bool sDown; //�׽�Ʈ��
    bool save;
    bool wDown;
    bool jDown;
    bool iDown;
    bool sDown1;
    bool sDown2;
    bool sDown3;

    bool isJump;
    bool isSwap;
    bool isBorder;  //��輱�� ��Ҵ��� Ȯ���ϴ� ����
                    //  bool isShop;    //�������϶�

    Vector3 moveVec;
    Rigidbody rigid;    //����ȿ���� ����ϱ� ���� ����
    Animator anim;

    GameObject nearObject;
    public GameObject equipWeapon;

    int equipWeaponIndex = -1;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();  //�ʱ�ȭ
        anim = GetComponentInChildren<Animator>();  //�ʱ�ȭ

        Debug.Log(PlayerPrefs.GetInt("MaxScore"));    //����� ������ Ȯ��
        PlayerPrefs.SetInt("MaxScore", 112500); //PlayerPrefs = ����Ƽ���� �����ϴ� ������ ���� ���
    }

    // GetAxisRaw() = Axis ���� ������ ��ȯ�ϴ� �Լ�
    void Update()
    {
        GetInput(); //�ʱ�ȭ�� �������� �Ʒ��Լ����� ���Ǿ� �ϹǷ� ù��°�� ��ġ
        Move();
        Turn();
        Jump();
        Swap();
        Interation();
        Putitem();
    }

    void GetInput()
    {   //Ű�Է� �̺�Ʈ
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        //���ۼ�wDown = Input.GetButton("Walk");
        jDown = Input.GetButtonDown("Jump");    //�����̽��ٸ� ������ ����
        //���ۼ�        iDown = Input.GetButtonDown("Interation");
        //���ۼ�        sDown1 = Input.GetButtonDown("Swap1");
        //���ۼ�        sDown2 = Input.GetButtonDown("Swap2");
        //���ۼ�        sDown3 = Input.GetButtonDown("Swap3");
        //���ۼ�        sDown = Input.GetButtonDown("Putitem");
    }

    void Move() //�̵�
    {
        // x��, y��, z��                   //��� ������ �������� ����(���Ⱚ�� 1�� ������ ����)
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        if (isSwap) //���ⱳü���϶�   
            moveVec = Vector3.zero; //�������� ����      
        if (!isBorder)  //���� �������   > 176�ٿ� �ش��ڵ�
        {/*
            if (wDown)  //�ȱ� Ű�� ������ �� (shift)          
                transform.position += moveVec * speed * 0.3f * Time.deltaTime;
            else*/
                transform.position += moveVec * speed * Time.deltaTime;
        }
        //���ۼ�        anim.SetBool("isRun", moveVec != Vector3.zero); //�Ķ���Ͱ� ����
        //���ۼ�        anim.SetBool("isWalk", wDown); //�Ķ���Ͱ� ����
    }

    void Turn() //ȸ��
    {
        //LookAt() = ������ ���͸� ���ؼ� ȸ�������ִ� �Լ�
        transform.LookAt(transform.position + moveVec);//���ư��� �������� �ٶ� 
    }
    void Jump() //����
    {
        if (jDown && moveVec == Vector3.zero && !isJump && !isSwap)    //����Ű�� ������ isJump�� false�϶�, isSwap�� false�϶�
        {
            //AddForce() = �������� ���� ���ϴ� �Լ�
            rigid.AddForce(Vector3.up * 15, ForceMode.Impulse); //����
//���ۼ�           anim.SetBool("isJump", true);
//���ۼ�           anim.SetTrigger("doJump");  //�ִϸ��̼� Ʈ����
            isJump = true;
        }
    }
    void Swap() //���ⱳü
    {
        if (sDown1 && (!hasWeapons[0] || equipWeaponIndex == 0)) //1��Ű�� ������ �� 1�����Ⱑ �������� ��������Ⱑ ���� �����϶�
            return;
        if (sDown2 && (!hasWeapons[1] || equipWeaponIndex == 1))
            return;
        if (sDown3 && (!hasWeapons[2] || equipWeaponIndex == 2))
            return;
        int weaponIndex = -1;
        if (sDown1) weaponIndex = 0;    //1��Ű�� �������� 
        if (sDown2) weaponIndex = 1;    //2��Ű�� �������� 
        if (sDown3) weaponIndex = 2;    //3��Ű�� �������� 

        if ((sDown1 || sDown2 || sDown3) && !isJump)    //1, 2, 3��Ű�� �ϳ��� ������ isJump�� �ƴҶ�
        {
            if (equipWeapon != null)
                equipWeapon.SetActive(false);   //���� �������� ���⸦ ������

            equipWeaponIndex = weaponIndex; //����Ű�� �ش��ϴ� ���� ������
            equipWeapon = weapons[weaponIndex]; //����迭���� �ش��ϴ� ���� ������
            equipWeapon.SetActive(true);    //�ش� ���⸦ ������

            anim.SetTrigger("doSwap");  //�ִϸ��̼� Ʈ����

            isSwap = true;

            Invoke("SwapOut", 0.4f);    //�Լ����۽ð� ���� 0.4��
        }
    }
    void SwapOut()  //���ⱳü ��
    {
        isSwap = false;
    }
    void Interation()   //EŰ�� ������ �� ����Ǵ� ��ȣ�ۿ�
    {
        if (iDown && nearObject != null && !isJump)  //EŰ�� ���Ȱ�, ����ü�� �����ϰ�, isJump�� �ƴҶ�
        {
            if (nearObject.tag == "Weapon")      //������ ��ü�� �����̸�
            {
                save = nearObject;
//���ۼ�      Item item = nearObject.GetComponent<Item>();   //Item ������Ʈ�� �޾ƿ� 
//���ۼ�      int weaponIndex = item.value;   //item�� value���� ������
//���ۼ�               hasWeapons[weaponIndex] = true;     //���� �������� ����迭�� ���� Ȱ��ȭ ��Ŵ 

                Destroy(nearObject);    //��ȣ�ۿ��� ��ü�� ������
            }
            else if (nearObject.tag == "Shop")  //������ ��ü�� �����̸�
            {
//���ۼ�      Shop shop = nearObject.GetComponent<Shop>();
//���ۼ�      shop.Enter(this);   //�÷��̾� ������ ���� �־��� //this = Player
                // isShop = true;
            }

        }
    }
    void Putitem()    //������ ����ϱ�
    {
        if (sDown && moveVec == Vector3.zero && !isJump && cntitem != 0)
        {
            Instantiate(itemObj[locitem], itemPos[0].position, Quaternion.identity);
            cntitem -= 1;

        }
    }
    void StopToWall()   //���� ����� �� �̵�����
    {
        //Scene ������ Ray�� �����ִ� �Լ� / ������ġ / ��¹��� / Ray�� ���� / ����
        Debug.DrawRay(transform.position, transform.forward * 2, Color.green);
        // Wall�̶�� LayerMask�� ���� ��ü�� �浹�ϸ� bool���� true�� �ٲ�
        isBorder = Physics.Raycast(transform.position, transform.forward, 2, LayerMask.GetMask("Wall"));
    }
    void FreezeRotation() //ȸ������(ĳ���Ͱ� ��ü�� ����� �� �ǵ�ġ �ʰ� ȸ���Ǵ� �������� ����)
    {
        rigid.angularVelocity = Vector3.zero;   //ȸ���ӵ��� 0���� �ٲ� > ������ �������� X 
    }
    void FixedUpdate()  //�Լ�����
    {
        FreezeRotation();
        StopToWall();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")     //�ٴڰ� ����� ��
        {
//���ۼ�            anim.SetBool("isJump", false);
            isJump = false;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        /*
        if (other.tag == "Item")     //�����۰� ����� ��
        {
        Item item = other.GetComponent<Item>();     //Item ��ũ��Ʈ ��������
            if (cntitem == 0)
            {
                switch (item.type)
                {
                    case Item.Type.Ammo:
                        cntitem += 1;
                        locitem = 0;
                        ammo += item.value;     //�������� value �� ��ŭ ammo���� ����
                        if (ammo > maxAmmo)     //���� ammo�� ���� �ִ�ġ���� ũ��
                            ammo = maxAmmo;     //���� �ִ�ġ�� ����
                        break;
                    case Item.Type.Coin:
                        coin += item.value;
                        if (coin > maxCoin)
                            coin = maxCoin;
                        break;
                    case Item.Type.Heart:
                        cntitem += 1;
                        locitem = 2;
                        health += item.value;
                        if (health > maxHealth)
                            health = maxHealth;
                        break;
                    case Item.Type.Grenade:
                        cntitem += 1;
                        locitem = 1;
                        grenades[hasGrenades].SetActive(true);      //ĳ���� �������� �����ϴ� ����ź Ȱ��ȭ
                        hasGrenades += item.value;
                        if (hasGrenades > maxHasGrenades)
                            hasGrenades = maxHasGrenades;
                        break;
                }
                Destroy(other.gameObject);  //������ ��ü ����
            }
        }*/
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Weapon" || other.tag == "Shop")   //���⳪ ������ ����� ��
            nearObject = other.gameObject;  //nearObject�� �� ����
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Weapon")  // ���Ⱑ ��� �������� ����� ��
            nearObject = null;  //���� �����
        else if (other.tag == "Shop")    //������������ ����� ��
        {
//���ۼ�           Shop shop = nearObject.GetComponent<Shop>();
//���ۼ�            shop.Exit();
            // isShop = false;
            nearObject = null;
        }
    }
}
