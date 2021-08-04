using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class BallScript : MonoBehaviour
{
    new Rigidbody rigidbody;

    PhotonView pw;

    int player1_score=0;
    int player2_score=0;

    public TMPro.TextMeshProUGUI player1_txt;
    public TMPro.TextMeshProUGUI player2_txt;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        pw = GetComponent<PhotonView>();
    }



    [PunRPC]
    public void startToGame()
    {
        rigidbody.velocity = new Vector3(5, 5, 0);
        showScore();
    }

    public void showScore()
    {
        player1_txt.text = PhotonNetwork.PlayerList[0].NickName + ": " + player1_score.ToString();
        player2_txt.text = PhotonNetwork.PlayerList[1].NickName + ": " + player2_score.ToString();
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (pw.IsMine)
        {
            if (collision.gameObject.name == "wallRight")
            {
                pw.RPC("gol", RpcTarget.All, 0, 1); //ilk parametre oyuncu 1'e ait ikinci oyuncu 2'ye 
            }
            else if (collision.gameObject.name == "wallLeft")
            {
                pw.RPC("gol", RpcTarget.All, 1, 0); //Buradaki 1 ve 0'lar score'daki artýþ miktarýný temsil ediyor gol fonksiyonunda oyuncunun skoruna buradakimiktar kadar ekleme yapýcaz
            }
        }
    }

    [PunRPC]
    public void gol(int player1, int player2)
    {
        player1_score += player1;
        player2_score += player2;

        showScore();
        
    }


    public void service()
    {
        rigidbody.velocity = Vector3.zero;
        transform.position = Vector3.zero;

        rigidbody.velocity = new Vector3(5, 5, 0);
    }


}
