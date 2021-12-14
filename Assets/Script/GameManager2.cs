using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager2 : MonoBehaviour
{
    public Button[] CellBtn;
    public Button PreviousBtn, NextBtn;
    List<S_RoomList.Room> myList;
    List<string> listString;
    
    int currentPage = 1, maxPage, multiple;

    public void ShowRoomList(S_RoomList packet)
    {
        myList = new List<S_RoomList.Room>();
        listString = new List<string>();
        foreach(S_RoomList.Room r in packet.rooms)
        {
            myList.Add(r);
            listString.Add($"Host : {r.host} Title :  {r.title} {r.nowPlayer} / {r.maxPlayer} ");
        }
        UpdateRoomList();
      

    }
    public void ShowRankList(S_RankList packet)
    {

    }
    public void UpdateRoomList()
    {
        // �ִ� ������
        maxPage = (myList.Count % CellBtn.Length == 0) ? myList.Count / CellBtn.Length : myList.Count / CellBtn.Length + 1;

        // ����, ���� ��ư
        PreviousBtn.interactable = (currentPage <= 1) ? false : true;
        NextBtn.interactable = (currentPage >= maxPage) ? false : true;

        multiple = (currentPage - 1) * CellBtn.Length;

        for (int i = 0; i < CellBtn.Length; i++)
        {
            CellBtn[i].interactable = (multiple + i < myList.Count) ? true : false;
            CellBtn[i].GetComponentInChildren<Text>().text = (multiple + i < myList.Count) ? listString[multiple + i] /*myList[multiple + i] :  ""*/: "";
        }
    }

    void Start()
    {
        
    }
    public void BtnClick(int num)
    {
        if (num == -2)
        {
            --currentPage;
            UpdateRoomList();
        }
        else if (num == -1) 
        {
            ++currentPage;
            UpdateRoomList();
        }
        else if (num == -3)
            NetworkManager.Instance.RoomListRequest();
        else
        {
            NetworkManager.Instance.EnterRoom(myList[multiple + num].roomId);
            print(multiple + num);
            Debug.Log(listString[multiple + num]);
            UpdateRoomList();
        }
        
        //Start();
    }

    [ContextMenu("����Ʈ �߰�")]
    void ListAdd() { /*myList.Add(); Start();*/ }

    [ContextMenu("����Ʈ �߰�")]
    void ListRemove() { myList.RemoveAt(0); Start(); }


    // Update is called once per frame
    void Update()
    {
        
    }
}
