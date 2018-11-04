using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using RDUI;

public class PNetworkManager : NetworkManager
{
    public static PNetworkManager instance;
    private void Start()
    {
        if (instance == null)
            instance = (singleton as PNetworkManager);
        else if (instance != this)
            Destroy(gameObject);     
    }
    public void LanGame(int _model)
    {
        StartCoroutine(DiscoverNetwork(_model));
    }
    //0:创建房间 1:加入房间
    private IEnumerator DiscoverNetwork(int _model)
    {
        //监听其它的服务器
        PNetworkDiscovery discovery = GetComponent<PNetworkDiscovery>();
        discovery.Initialize();

        if (_model == 0)
        {
            StartServer();
            discovery.StartAsServer();
        }
        else if (_model == 1)
        {
            discovery.StartAsClient();

            //等待60秒，等监听结果
            //yield return new WaitForSeconds(60);

            ////没有找到局域网服务器就停止监听
            //if (discovery.running)
            //{
            //    discovery.StopBroadcast();
            //}

        }
        yield return null;
    }
    public override void OnStartServer()
    {
        Debug.Log("Server Register");
        NetworkServer.RegisterHandler(BattleStartMessage.type, OnBattleStartMessage);
        NetworkServer.RegisterHandler(ChangePokemonMessage.type, OnChangePokemon);
        base.OnStartServer();
    }
    public override void OnStartClient(NetworkClient client)
    {
        Debug.Log("Client Register");
        client.RegisterHandler(BattleStartMessage.type, OnBattleStartMessage);
        client.RegisterHandler(ChangePokemonMessage.type, OnChangePokemon);
        base.OnStartClient(client);
    }
    public override void OnServerReady(NetworkConnection conn)
    {
        Debug.Log("OnServerReady");
        base.OnServerReady(conn);
    }
    public override void OnServerConnect(NetworkConnection conn)
    {
        for (int i = 0; i < 6; i++)
        {
            BattleStartMessage msg = new BattleStartMessage(RuntimeData.GetMyPokemonByIndex(i),i);
            NetworkServer.SendToClient(conn.connectionId, BattleStartMessage.type, msg);
        }
        base.OnServerConnect(conn);
    }
    public override void OnClientConnect(NetworkConnection conn)
    {
        for (int i = 0; i < 6; i++)
        {
            BattleStartMessage msg = new BattleStartMessage(RuntimeData.GetMyPokemonByIndex(i),i);
            client.Send(BattleStartMessage.type, msg);
        }
        base.OnClientConnect(conn);
    }
    //更换精灵
    public  void ChangePokemon(int _index)
    {
        ChangePokemonMessage msg = new ChangePokemonMessage(_index);
        if (NetworkServer.active)
        {          
            NetworkServer.SendToAll(ChangePokemonMessage.type, msg);
        }
        else
        {
            client.Send(ChangePokemonMessage.type, msg);
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
        for(int i=0;i<msg.skillIds.Length;i++)
        {
            sModel[i] = PublicDataManager.instance.GetSkillModel(msg.skillIds[i]);
        }
        Pokemon p = new Pokemon(pModel, cModel, perModel, iModel, sModel);
        RuntimeData.SetOppPokemon(msg.index,p);
        if (RuntimeData.IsOppPokemonsFull())
        {
            UIManager.instance.ClosePage(PageCollection.StartPage);
            UIManager.instance.OpenPage(PageCollection.BattlePage);
        }
    }
    //回调-更换精灵
    private void OnChangePokemon(NetworkMessage netMsg)
    {
        netMsg.reader.SeekZero();
        ChangePokemonMessage msg = netMsg.reader.ReadMessage<ChangePokemonMessage>();
        RuntimeData.SetCurrentOppIndex(msg.index);
    }
}