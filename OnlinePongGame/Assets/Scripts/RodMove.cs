using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class RodMove : MonoBehaviour
{
    PhotonView pw;

    TMPro.TextMeshProUGUI InformationText;

    private void Awake()
    {
        InformationText = GameObject.Find("Canvas/InformationText").GetComponent<TMPro.TextMeshProUGUI>();
        pw = GetComponent<PhotonView>();

        if (pw.IsMine)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                transform.position = new Vector3(7, 0, -6.5f);
                InvokeRepeating("playerControl",0,0.5f); //her yarým saniyede bir çalýþmasý gereken bir fonksiyon tanýmladýk

            }
            else if (!PhotonNetwork.IsMasterClient)
            {
                transform.position = new Vector3(-7, 0, -6.5f);
            }
        }
    }


    void playerControl()
    {
        if(PhotonNetwork.PlayerList.Length == 2)
        {
            pw.RPC("clearText", RpcTarget.All, null);
            GameObject.Find("Ball").GetComponent<PhotonView>().RPC("startToGame", RpcTarget.All, null); //top'un içersindeki startToGame fonksiyonuna ulaþarak topu harekete geçirdik
            CancelInvoke("playerControl"); //her yarým saniyede bir çalýþmaya ayarlý fonksiyonda böylelikle durduruldu.
            //player control oyunun baþýnda ancak bir kez çalýþabelecek 2 oyuncu þartý saðlandýktan hemen sonra CancelInvoke edilecek.
        }
    }




    [PunRPC]
    public void clearText()
    {
        InformationText.text = null;
    }


    private void Update()
    {
        if (pw.IsMine) //bunu yapmazsam kendi ekranýmda tüm tahtalarýn hareket ettiðini görürüm
        {
            rodMove();
        }
    }



    void rodMove()
    {
        float vertical = Input.GetAxis("Mouse Y") * Time.deltaTime * 20;
        transform.Translate(0, vertical, 0);
    }


}
