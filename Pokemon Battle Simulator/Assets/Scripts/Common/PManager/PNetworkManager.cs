using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using RDUI;

public class PNetworkManager : NetworkManager
{
    public static PNetworkManager instance;
    PNetworkDiscovery discovery;
    private void Start()
    {
        if (instance == null)
            instance = (singleton as PNetworkManager);
        else if (instance != this)
            Destroy(gameObject);
        discovery = GetComponent<PNetworkDiscovery>();
    }
    public void LanGame(int _model)
    {
        StartCoroutine(DiscoverNetwork(_model));
    }
    //0:创建房间 1:加入房间
    private IEnumerator DiscoverNetwork(int _model)
    {
        if (discovery.running)
        {
            discovery.StopBroadcast();
        }
        discovery.Initialize();
        //监听其它的服务器
        if (_model == 0)
        {
            StartServer();
            discovery.StartAsServer();
        }
        else if (_model == 1)
        {
            discovery.StartAsClient();
            UIDelegateManager.NotifyUI(UIMessageType.SearchRoom, null);
        }
        yield return null;
    }
    public override void OnStartServer()
    {
        Debug.Log("Server Register");
        if (discovery.running)
        {
            discovery.StopBroadcast();
        }
        NetworkServer.RegisterHandler(BattleStartMessage.type, OnBattleStartMessage);
        NetworkServer.RegisterHandler(ChangePokemonMessage.type, OnChangePokemon);
        NetworkServer.RegisterHandler(UseSkillMessage.type, OnUseSkill);
        NetworkServer.RegisterHandler(GiveUpMessage.type, OnGiveUp);
        base.OnStartServer();
        UIDelegateManager.NotifyUI(UIMessageType.CreateRoomSucceed, null);
    }
    public override void OnStartClient(NetworkClient client)
    {
        Debug.Log("Client Register");
        if (discovery.running)
        {
            discovery.StopBroadcast();
        }
        client.RegisterHandler(BattleStartMessage.type, OnBattleStartMessage);
        client.RegisterHandler(BeginBattleOrderMessage.type, OnBeginBattleOrder);
        client.RegisterHandler(ChangePokemonMessage.type, OnChangePokemon);
        client.RegisterHandler(UseSkillMessage.type, OnUseSkill);
        client.RegisterHandler(GiveUpMessage.type, OnGiveUp);
        base.OnStartClient(client);
    }
    public override void OnServerReady(NetworkConnection conn)
    {
        Debug.Log("OnServerReady");
        base.OnServerReady(conn);
    }
    public override void OnServerDisconnect(NetworkConnection conn)
    {
        Debug.Log("Server  a cient Disconnect");
        NetworkServer.ClearHandlers();
        StopClient();
        StopServer();
        base.OnServerDisconnect(conn);
    }
    public override void OnStopServer()
    {
        UIManager.instance.ClosePage(PageCollection.BattlePage);
        UIManager.instance.OpenPage(PageCollection.StartPage);
        Debug.Log("Stop Server");
        base.OnStopServer();
    }
    public override void OnClientDisconnect(NetworkConnection conn)
    {
        Debug.Log("Client Disconnect");
        client.UnregisterHandler(BattleStartMessage.type);
        client.UnregisterHandler(BeginBattleOrderMessage.type);
        client.UnregisterHandler(ChangePokemonMessage.type);
        client.UnregisterHandler(UseSkillMessage.type);
        client.UnregisterHandler(GiveUpMessage.type);
        StopClient();
        base.OnClientDisconnect(conn);
    }
    public override void OnStopClient()
    {
        Debug.Log("Stop Client ");
        UIManager.instance.ClosePage(PageCollection.BattlePage);
        UIManager.instance.OpenPage(PageCollection.StartPage);
        base.OnStopClient();
    }
    public override void OnServerConnect(NetworkConnection conn)
    {
        for (int i = 0; i < RuntimeData.PARTY_NUM; i++)
        {
            BattleStartMessage msg = new BattleStartMessage(RuntimeData.GetMyPokemonByIndex(i), i);
            NetworkServer.SendToClient(conn.connectionId, BattleStartMessage.type, msg);
        }
        base.OnServerConnect(conn);
    }
    public override void OnClientConnect(NetworkConnection conn)
    {
        for (int i = 0; i < RuntimeData.PARTY_NUM; i++)
        {
            BattleStartMessage msg = new BattleStartMessage(RuntimeData.GetMyPokemonByIndex(i), i);
            client.Send(BattleStartMessage.type, msg);
        }
        base.OnClientConnect(conn);
    }
    public bool IsServer()
    {
        return NetworkServer.active;
    }
    //开始对战-出手顺序-只在服务器发送
    public void BeginBattleOrder(bool _isMyOrder)
    {
        BeginBattleOrderMessage msg = new BeginBattleOrderMessage(_isMyOrder);
        if (NetworkServer.active)
        {
            NetworkServer.SendToAll(BeginBattleOrderMessage.type, msg);
        }
    }
    //更换精灵
    public void ChangePokemon(int _index, bool _isDeathChange)
    {
        ChangePokemonMessage msg = new ChangePokemonMessage(_index, _isDeathChange);
        if (NetworkServer.active)
        {
            NetworkServer.SendToAll(ChangePokemonMessage.type, msg);
        }
        else
        {
            client.Send(ChangePokemonMessage.type, msg);
        }
    }
    //使用技能
    public void UseSkill(int _index)
    {
        UseSkillMessage msg = new UseSkillMessage(_index);
        if (NetworkServer.active)
        {
            NetworkServer.SendToAll(UseSkillMessage.type, msg);
        }
        else
        {
            client.Send(UseSkillMessage.type, msg);
        }
    }
    //放弃对战
    public void GiveUp()
    {
        GiveUpMessage msg = new GiveUpMessage(client.connection.connectionId);
        if (NetworkServer.active)
        {
            NetworkServer.SendToAll(GiveUpMessage.type, msg);
        }
        else
        {
            client.Send(GiveUpMessage.type, msg);
        }
    }
    //停止寻找房间/等待对手加入
    public void Stop()
    {
        if (discovery.running)
        {
            discovery.StopBroadcast();
        }
        if (NetworkServer.active)
        {
            StopServer();
        }
        if (instance.client != null)
        {
            client.Disconnect();
            StopClient();
        }
    }
    //回调-初始化对方阵容
    private void OnBattleStartMessage(NetworkMessage netMsg)
    {
        netMsg.reader.SeekZero();
        BattleStartMessage msg = netMsg.ReadMessage<BattleStartMessage>();
        PokemonModel pModel = PublicDataManager.instance.GetPokemonModel(msg.pokemonId);
        Debug.Log("Opp Pokemon" + pModel.name_ch);
        pModel.attack = msg.attackAv;
        pModel.defense = msg.defenceAv;
        pModel.hp = msg.hpAv;
        pModel.sp_attack = msg.sp_attackAv;
        pModel.sp_defense = msg.sp_defenceAv;
        pModel.speed = msg.speedAv;
        CharacterModel cModel = PublicDataManager.instance.GetCharacterModel(msg.charavterId);
        PersonalityModel perModel = PublicDataManager.instance.GetPersonalityModel(msg.personalityId);
        ItemModel iModel = PublicDataManager.instance.GetItemModel(msg.itemId);
        SkillModel[] sModel = new SkillModel[4];
        for (int i = 0; i < msg.skillIds.Length; i++)
        {
            sModel[i] = PublicDataManager.instance.GetSkillModel(msg.skillIds[i]);
        }
        Pokemon p = new Pokemon(pModel, cModel, perModel, iModel, sModel);
        RuntimeData.SetOppPokemon(msg.index, p);
        if (RuntimeData.IsOppPokemonsFull())
        {
            UIManager.instance.ClosePage(PageCollection.StartPage);
            UIManager.instance.OpenPage(PageCollection.BattlePage);
        }
    }
    private void OnBeginBattleOrder(NetworkMessage netMsg)
    {
        BeginBattleOrderMessage msg = netMsg.reader.ReadMessage<BeginBattleOrderMessage>();
        UIDelegateManager.NotifyUI(UIMessageType.BeginBattleOrder, new object[] { msg.isMyRound });
    }
    //回调-更换精灵
    private void OnChangePokemon(NetworkMessage netMsg)
    {
        ChangePokemonMessage msg = netMsg.reader.ReadMessage<ChangePokemonMessage>();
        UIDelegateManager.NotifyUI(UIMessageType.ChangePokemon, new object[] { msg.index, msg.isCantFightChange, false });
    }
    //回调-使用技能
    private void OnUseSkill(NetworkMessage netMsg)
    {
        UseSkillMessage msg = netMsg.reader.ReadMessage<UseSkillMessage>();
        UIDelegateManager.NotifyUI(UIMessageType.UseSkill, new object[] { msg.skillIndex, false });
    }
    //回调-放弃对战
    private void OnGiveUp(NetworkMessage netMsg)
    {
        GiveUpMessage msg = netMsg.reader.ReadMessage<GiveUpMessage>();
        UIDelegateManager.NotifyUI(UIMessageType.GiveUp, null);
        StartCoroutine(DelayDisConnect(5f));
    }
    private IEnumerator DelayDisConnect(float _seconds)
    {
        yield return new WaitForSeconds(_seconds);
        if (discovery.running)
        {
            discovery.StopBroadcast();
        }
        if (NetworkServer.active)
        {
            NetworkServer.DisconnectAll();
        }
        if (client != null)
        {
            client.Disconnect();
        }
    }
}