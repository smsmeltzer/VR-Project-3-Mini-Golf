using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ClubGrabSpawnBall : MonoBehaviour
{
    // Start is called before the first frame update
    private ClubLogisticsManager CLM;
    void Start()
    {
        GameObject clubLogistics = GameObject.Find("ClubLogistics");
        CLM = clubLogistics.GetComponent<ClubLogisticsManager>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateBallSpawnBall()
    {
        CLM.SpawnBall();
    }
}
