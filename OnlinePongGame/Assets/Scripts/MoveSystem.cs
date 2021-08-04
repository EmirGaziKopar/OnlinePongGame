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


        if(pw.IsMine == true) //Biz isek yani asl�nda her iki oyuncuda birbirini beyaz g�recek ama kendilerini k�rm�z� g�recekler 
        {
            
        }
    }

    private void Update()
    {
        if (pw.IsMine == true)
        {

            move();

        }
    }


    private void move()
    {
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * 30.0f; //her saniye 30 birim hareket g�c� sa�lar
        float y = Input.GetAxis("Vertical") * Time.deltaTime * 30.0f; //her saniye 30 birim hareket g�c� sa�lar

        transform.Translate(x, 0, y); //Belirlenen pozisyona ge�i�i sa�lar 
        
    }



    void ball()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if(Physics.Raycast(transform.position , transform.forward , out hit , 100.0f)) //ate� 
            {
                hit.collider.gameObject.GetComponent<PhotonView>().RPC("yokol", RpcTarget.All, null);
            }
        }
    }


    [PunRPC]
    public void yokol()
    {
        PhotonNetwork.Destroy(gameObject);//biz ate� etti�imizde bu herhangi bir nesneye �arpt��nda onun yok ol fonksiyonuna eri�ecek ve nesnemiz silinecek (sunucuda da yani t�m bilgisayarlarda)
    }

}
