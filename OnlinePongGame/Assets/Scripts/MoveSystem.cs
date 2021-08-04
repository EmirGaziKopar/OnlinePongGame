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
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * 30.0f; //her saniye 30 birim hareket gücü saðlar
        float y = Input.GetAxis("Vertical") * Time.deltaTime * 30.0f; //her saniye 30 birim hareket gücü saðlar

        transform.Translate(x, 0, y); //Belirlenen pozisyona geçiþi saðlar 
        
    }



    void ball()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if(Physics.Raycast(transform.position , transform.forward , out hit , 100.0f)) //ateþ 
            {
                hit.collider.gameObject.GetComponent<PhotonView>().RPC("yokol", RpcTarget.All, null);
            }
        }
    }


    [PunRPC]
    public void yokol()
    {
        PhotonNetwork.Destroy(gameObject);//biz ateþ ettiðimizde bu herhangi bir nesneye çarptðýnda onun yok ol fonksiyonuna eriþecek ve nesnemiz silinecek (sunucuda da yani tüm bilgisayarlarda)
    }

}
