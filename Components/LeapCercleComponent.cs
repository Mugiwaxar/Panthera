using Panthera.Base;
using Panthera.Components;
using Panthera.NetworkMessages;
using R2API.Networking;
using R2API.Networking.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Panthera.Components
{
    //class LeapCercleComponent : MonoBehaviour
    //{

    //    public static float destroyDelay = 1;

    //    public PantheraObj ptraObj;
    //    public Vector3 position;
    //    public float buffTimer;
    //    public bool destroying;
    //    public bool client = false;

    //    public void Start()
    //    {
    //        if (this.position == Vector3.zero) this.position = this.transform.position;
    //        if(this.client == false) new ServerSetBuffCount(ptraObj.gameObject, (int)Buff.leapCercle.buffIndex, (int)PantheraConfig.LeapCercle_leapCercleDuration).Send(NetworkDestination.Server);
    //        this.buffTimer = Time.time;
    //    }

    //    public void Update()
    //    {
    //        if (this.destroying == true || this.ptraObj == null) return;
    //        float durationLeft = PantheraConfig.LeapCercle_leapCercleDuration - (Time.time - this.buffTimer);
    //        if (this.client == false) new ServerSetBuffCount(ptraObj.gameObject, (int)Buff.leapCercle.buffIndex, (int)Math.Ceiling(durationLeft)).Send(NetworkDestination.Server);

    //    }

    //    public void OnDestroy()
    //    {
    //        if (this.ptraObj.actualLeapCerle == this.gameObject)
    //        {
    //            this.ptraObj.actualLeapCerle = null;
    //            if (this.client == false) new ServerSetBuffCount(ptraObj.gameObject, (int)Buff.leapCercle.buffIndex, 0).Send(NetworkDestination.Server);
    //        }
    //    }

    //}

}
