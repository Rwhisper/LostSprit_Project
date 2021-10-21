using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public GameObject MenuCam;
  //  public GameObject FireCam;
    public PlayerController WaterPlayer;
    public PlayerController FirePlayer;
    public GameObject StartMenu;
    public GameObject UIPanel;
    public Image Img1;
    public Image Img2;
    public Transform[] PuzzlePos;
<<<<<<< HEAD
    public GameObject Puzzleitem;

    public ObjectNum objNum;
=======
    public GameObject[] Puzzleitem;
>>>>>>> 95a9e4e787eb69c760448250289d0a60164ce28c

    bool isInstantiate = false;


    public void GameStartFire()
    {

        StartMenu.SetActive(false);
        UIPanel.SetActive(true);
        MenuCam.SetActive(false);        
        FirePlayer.gameObject.SetActive(true);
        WaterPlayer.gameObject.SetActive(false);
        Img1.color = new Color(1, 1, 1, 0);
        Img2.color = new Color(1, 1, 1, 0);
<<<<<<< HEAD
        //if (!isInstantiate)
        //{
        //    for (int i = 0; i < 4; i++)
        //    {
        //        Instantiate(Puzzleitem, PuzzlePos[i].position, Quaternion.identity);
        //    }
        //    isInstantiate = true;
        //}
       
=======
        isInstantiate = true;
        for (int i = 0; i < 4; i++)
        {
            Instantiate(Puzzleitem[0], PuzzlePos[i].position, Quaternion.identity);
        }
>>>>>>> 95a9e4e787eb69c760448250289d0a60164ce28c

    }
    public void GameStartWater()
    {

        StartMenu.SetActive(false);
        UIPanel.SetActive(true);
        MenuCam.SetActive(false);
        FirePlayer.gameObject.SetActive(false);
        WaterPlayer.gameObject.SetActive(true);
        Img1.color = new Color(1, 1, 1, 0);
        Img2.color = new Color(1, 1, 1, 0);
<<<<<<< HEAD
        //if (!isInstantiate)
        //{
        //    for (int i = 0; i < 4; i++)
        //    {
        //        Instantiate(Puzzleitem, PuzzlePos[i].position, Quaternion.identity);
        //    }
        //    isInstantiate = true;
        //}
=======
        if (!isInstantiate)
        {
            for (int i = 0; i < 4; i++)
            {
                Instantiate(Puzzleitem[0], PuzzlePos[i].position, Quaternion.identity);
            }
        }
>>>>>>> 95a9e4e787eb69c760448250289d0a60164ce28c
    }
    void LateUpdate()   //Update() �� ���� �� ȣ���
    {

        Img1.color = new Color(1, 1, 1, WaterPlayer.cntitem[0] != 0 ? 1 : 0); //RGB�� �ǵ����� �ʰ� alpha���� ����
        Img2.color = new Color(1, 1, 1, WaterPlayer.cntitem[1] != 0 ? 1 : 0); //RGB�� �ǵ����� �ʰ� alpha���� ����
       
    }

}
