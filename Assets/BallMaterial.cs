using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BallMaterial : MonoBehaviourPun
{
    public ClubLogisticsManager CLM;
    private void Start()
    {
        GameObject clubLogistics = GameObject.Find("ClubLogistics");
        CLM = clubLogistics.GetComponent<ClubLogisticsManager>();
    }

    [PunRPC]
    public void UpdateMaterial(int ID, int index)
    {
        CLM.UpdateMaterial(ID, index);
    }
}
