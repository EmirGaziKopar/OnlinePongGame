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
                InvokeRepeating("playerControl",0,0.5f); //her yar�m saniyede bir �al��mas� gereken bir fonksiyon tan�mlad�k

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
            GameObject.Find("Ball").GetComponent<PhotonView>().RPC("startToGame", RpcTarget.All, null); //top'un i�ersindeki startToGame fonksiyonuna ula�arak topu harekete ge�irdik
            CancelInvoke("playerControl"); //her yar�m saniyede bir �al��maya ayarl� fonksiyonda b�ylelikle durduruldu.
            //player control oyunun ba��nda ancak bir kez �al��abelecek 2 oyuncu �art� sa�land�ktan hemen sonra CancelInvoke edilecek.
        }
    }




    [PunRPC]
    public void clearText()
    {
        InformationText.text = null;
    }


    private void Update()
    {
        if (pw.IsMine) //bunu yapmazsam kendi ekran�mda t�m tahtalar�n hareket etti�ini g�r�r�m
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
