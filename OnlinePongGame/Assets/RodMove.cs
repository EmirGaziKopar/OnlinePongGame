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
                InvokeRepeating("oyuncukontrol",0,0.5f);

            }
            else if (!PhotonNetwork.IsMasterClient)
            {
                transform.position = new Vector3(-7, 0, -6.5f);
            }
        }
    }


}
