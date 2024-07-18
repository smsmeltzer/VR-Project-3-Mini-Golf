using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ClubMaterial : MonoBehaviourPun
{
    public ClubLogisticsManager CLM;
    PhotonView view;
    int index = -1;
    private void Start()
    {
        GameObject clubLogistics = GameObject.Find("ClubLogistics");
        CLM = clubLogistics.GetComponent<ClubLogisticsManager>();

        view = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (view.IsMine)
        {
            if (index != CLM.playerCount - 1)
            {
                view.RPC("UpdateMaterial", RpcTarget.Others, view.ViewID, index);
                index = CLM.playerCount - 1;
            }
        }
    }

    [PunRPC]
    public void UpdateMaterial(int ID, int index)
    {
        CLM.UpdateMaterial(ID, index);
    }
}
