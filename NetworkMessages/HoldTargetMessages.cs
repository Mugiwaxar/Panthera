namespace Panthera.NetworkMessages
{
    //public class ServerAttachHoldTargetComp : INetMessage
    //{

    //    GameObject player;
    //    GameObject target;
    //    float relativeDistance;
    //    bool isAlly;

    //    public ServerAttachHoldTargetComp()
    //    {

    //    }

    //    public ServerAttachHoldTargetComp(GameObject player, GameObject target, float relativeDistance, bool isAlly = false)
    //    {
    //        this.player = player;
    //        this.target = target;
    //        this.relativeDistance = relativeDistance;
    //        this.isAlly = isAlly;
    //    }

    //    public void OnReceived()
    //    {
    //        if (this.player == null || this.target == null) return;
    //        PantheraObj ptraObj = this.player.GetComponent<PantheraObj>();
    //        if (ptraObj == null) return;
    //        if (this.target.GetComponent<HoldTarget>() == null)
    //        {
    //            HoldTarget comp = this.target.AddComponent<HoldTarget>();
    //            comp.ptraObj = ptraObj;
    //            comp.relativeDistance = this.relativeDistance;
    //        }
    //        new ClientAttachHoldTargetComp(this.player, this.target, this.relativeDistance, this.isAlly).Send(NetworkDestination.Clients);
    //    }

    //    public void Serialize(NetworkWriter writer)
    //    {
    //        writer.Write(this.player);
    //        writer.Write(this.target);
    //        writer.Write(this.relativeDistance);
    //        writer.Write(this.isAlly);
    //    }

    //    public void Deserialize(NetworkReader reader)
    //    {
    //        this.player = reader.ReadGameObject();
    //        this.target = reader.ReadGameObject();
    //        this.relativeDistance = reader.ReadSingle();
    //        this.isAlly = reader.ReadBoolean();
    //    }
    //}

    //public class ServerDetachHoldTargetComp : INetMessage
    //{

    //    public GameObject player;
    //    public GameObject target;

    //    public ServerDetachHoldTargetComp()
    //    {

    //    }

    //    public ServerDetachHoldTargetComp(GameObject player, GameObject target)
    //    {
    //        this.player = player;
    //        this.target = target;
    //    }

    //    public void OnReceived()
    //    {
    //        if (target == null) return;
    //        if (Utils.Functions.IsServer() == true) GameObject.DestroyImmediate(target.GetComponent<HoldTarget>());
    //        new ClientDetachHoldTargetComp(this.player, this.target).Send(NetworkDestination.Clients);
    //    }

    //    public void Serialize(NetworkWriter writer)
    //    {
    //        writer.Write(this.player);
    //        writer.Write(this.target);
    //    }

    //    public void Deserialize(NetworkReader reader)
    //    {
    //        this.player = reader.ReadGameObject();
    //        this.target = reader.ReadGameObject();
    //    }

    //}

    //public class ServerHoldTargetLastPosition : INetMessage
    //{

    //    public GameObject target;
    //    public Vector3 lastPosition;

    //    public ServerHoldTargetLastPosition()
    //    {

    //    }

    //    public ServerHoldTargetLastPosition(GameObject target, Vector3 lastPosition)
    //    {
    //        this.target = target;
    //        this.lastPosition = lastPosition;
    //    }

    //    public void OnReceived()
    //    {
    //        if (target == null) return;
    //        target.transform.position = this.lastPosition;
    //        KinematicCharacterMotor kinMotor = target.GetComponent<KinematicCharacterMotor>();
    //        if (kinMotor != null)
    //        {
    //            kinMotor.SetPosition(this.lastPosition);
    //        }
    //        else if (target.transform != null)
    //        {
    //            target.transform.position = this.lastPosition;
    //        }
    //        Transform modelTransform = target.GetComponent<ModelLocator>()?.modelTransform;
    //        if (modelTransform != null)
    //        {
    //            modelTransform.position = this.lastPosition;
    //        }
    //    }

    //    public void Serialize(NetworkWriter writer)
    //    {
    //        writer.Write(this.target);
    //        writer.Write(this.lastPosition);
    //    }

    //    public void Deserialize(NetworkReader reader)
    //    {
    //        this.target = reader.ReadGameObject();
    //        this.lastPosition = reader.ReadVector3();
    //    }

    //}

    //public class ClientAttachHoldTargetComp : INetMessage
    //{

    //    GameObject player;
    //    GameObject target;
    //    float relativeDistance;
    //    bool isAlly;

    //    public ClientAttachHoldTargetComp()
    //    {

    //    }

    //    public ClientAttachHoldTargetComp(GameObject player, GameObject target, float relativeDistance, bool isAlly)
    //    {
    //        this.player = player;
    //        this.target = target;
    //        this.relativeDistance = relativeDistance;
    //        this.isAlly = isAlly;
    //    }

    //    public void OnReceived()
    //    {
    //        if (this.player == null || this.target == null || Util.HasEffectiveAuthority(this.player)) return;
    //        PantheraObj ptraObj = this.player.GetComponent<PantheraObj>();
    //        if (ptraObj == null) return;
    //        if (this.target.GetComponent<HoldTarget>() == null)
    //        {
    //            HoldTarget comp = this.target.AddComponent<HoldTarget>();
    //            comp.ptraObj = ptraObj;
    //            comp.relativeDistance = this.relativeDistance;
    //            comp.isAlly = this.isAlly;
    //        }
    //    }

    //    public void Serialize(NetworkWriter writer)
    //    {
    //        writer.Write(this.player);
    //        writer.Write(this.target);
    //        writer.Write(this.relativeDistance);
    //        writer.Write(this.isAlly);
    //    }

    //    public void Deserialize(NetworkReader reader)
    //    {
    //        this.player = reader.ReadGameObject();
    //        this.target = reader.ReadGameObject();
    //        this.relativeDistance = reader.ReadSingle();
    //        this.isAlly = reader.ReadBoolean();
    //    }
    //}

    //public class ClientDetachHoldTargetComp : INetMessage
    //{

    //    public GameObject player;
    //    public GameObject target;

    //    public ClientDetachHoldTargetComp()
    //    {

    //    }

    //    public ClientDetachHoldTargetComp(GameObject player, GameObject target)
    //    {
    //        this.player = player;
    //        this.target = target;
    //    }

    //    public void OnReceived()
    //    {
    //        if (target == null || Util.HasEffectiveAuthority(this.player) == true) return;
    //        GameObject.DestroyImmediate(target.GetComponent<HoldTarget>());
    //    }

    //    public void Serialize(NetworkWriter writer)
    //    {
    //        writer.Write(this.player);
    //        writer.Write(this.target);
    //    }

    //    public void Deserialize(NetworkReader reader)
    //    {
    //        this.player = reader.ReadGameObject();
    //        this.target = reader.ReadGameObject();
    //    }

    //}

}
