using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
public class MoveSystem : MonoBehaviour
{

    public Animator anim;

    PhotonView pw;

    public void Awake()
    {
        pw = GetComponent<PhotonView>();


        if(pw.IsMine == true) //Biz isek yani aslýnda her iki oyuncuda birbirini beyaz görecek ama kendilerini kýrmýzý görecekler 
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
    }

}
