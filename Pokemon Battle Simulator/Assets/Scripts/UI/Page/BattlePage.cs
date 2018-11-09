using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace RDUI
{
    public class BattlePage : BasePage
    {
        public Slot[] slots;
        public SkillBlock[] skillBlocks;
        public GameObject selectPanel;
        public Button skillBtn;
        public GameObject skillPanel;
        public Button skillReturnBtn;
        public Button pokemonBtn;
        public Button pokemonReturnBtn;
        public GameObject pokemonPanel;
        public Button giveUpBtn;
        public BattleState myState;
        public BattleState opponentState;
        public Text messageText;
        public Animator oppPokmeonAnimator;
        public Animator myPokmeonAnimator;

        public AudioSource clickAudio;
        protected override void Init()
        {
            skillBtn.onClick.AddListener(OnClickSkillBtn);
            skillReturnBtn.onClick.AddListener(OnClickSkillReturnBtn);
            pokemonBtn.onClick.AddListener(OnClickPokemonBtn);
            pokemonReturnBtn.onClick.AddListener(OnClickPokemonReturnBtn);
            giveUpBtn.onClick.AddListener(OnClickGiveUpBtn);
            base.Init();
        }
        public override void Open()
        {
            //初始化Pokemon阵容
            for (int i = 0; i < slots.Length; i++)
            {
                PokemonModel p = RuntimeData.GetMyPokemonByIndex(i).Model;
                Sprite icon = Resources.Load<Sprite>("PokemonSprites/" + StringUtil.FormatId(p.id) + "/Icon/IMG00000");
                slots[i].SetProperty(icon, p.name_ch, p.hp);
                int index = i;
                //更换Pokemon
                slots[i].gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                slots[i].gameObject.GetComponent<Button>().onClick.AddListener(() =>
                {
                    clickAudio.PlayOneShot(clickAudio.clip);
                    //主动更换精灵
                    if (RuntimeData.GetCurrentMyPokemon().CurrentHp > 0)
                    {
                        //不能选择当前出战的Pokemon
                        if(index != RuntimeData.GetCurrentMyIndex())
                        {
                            PNetworkManager.instance.ChangePokemon(index, false);
                            StartCoroutine(ChangePokemon(index, false, true));
                        }
                    }//濒死更换
                    else
                    {
                        PNetworkManager.instance.ChangePokemon(index, true);
                        StartCoroutine(ChangePokemon(index, true,true));
                    }
                });
            }

            UIDelegateManager.AddObserver(UIMessageType.RefreshBattlePokemon, OnRefreshPokemon);
            UIDelegateManager.AddObserver(UIMessageType.RefreshMyHpText, OnRefreshMyHpText);
            UIDelegateManager.AddObserver(UIMessageType.RefreshMyHpBar, OnRefreshMyHpBar);
            UIDelegateManager.AddObserver(UIMessageType.RefreshOpponentHpBar, OnRefreshOpponentHpBar);
            UIDelegateManager.AddObserver(UIMessageType.BeginBattleOrder, OnBeginBattleOrder);
            UIDelegateManager.AddObserver(UIMessageType.UseSkill, ShowUseSkill);
            UIDelegateManager.AddObserver(UIMessageType.ChangePokemon, ShowChangePokemon);
            UIDelegateManager.AddObserver(UIMessageType.PokemonCantFight, OnPokemonCantFight);
            UIDelegateManager.AddObserver(UIMessageType.GiveUp, OnGiveUp);


            
            base.Open();

            StartCoroutine(StartBattle());
        }
        public override void Close()
        {
            UIDelegateManager.RemoveObserver(UIMessageType.RefreshBattlePokemon, OnRefreshPokemon);
            UIDelegateManager.RemoveObserver(UIMessageType.RefreshMyHpText, OnRefreshMyHpText);
            UIDelegateManager.RemoveObserver(UIMessageType.RefreshMyHpBar, OnRefreshMyHpBar);
            UIDelegateManager.RemoveObserver(UIMessageType.RefreshOpponentHpBar, OnRefreshOpponentHpBar);
            UIDelegateManager.RemoveObserver(UIMessageType.UseSkill, ShowUseSkill);
            UIDelegateManager.RemoveObserver(UIMessageType.ChangePokemon, ShowChangePokemon);
            UIDelegateManager.RemoveObserver(UIMessageType.PokemonCantFight, OnPokemonCantFight);
            UIDelegateManager.RemoveObserver(UIMessageType.GiveUp, OnGiveUp);
            base.Close();
        }
        private void OnRefreshMyHpText(object[] _value)
        {
            myState.SetCurrentHP((int)_value[0]);
        }
        private void OnRefreshMyHpBar(object[] _value)
        {
            myState.SetHpBar((float)_value[0]);
        }
        private void OnRefreshOpponentHpBar(object[] _value)
        {
            opponentState.SetHpBar((float)_value[0]);
        }
        private void OnBeginBattleOrder(object[] _value)
        {
            if (_value.Length < 1)
            {
                Debug.LogError("Begin Battle Order Arg Length < 2");
                return;
            }
            ChangeRound((bool)_value[0]);
        }
        private void ShowUseSkill(object[] _value)
        {
            if (_value.Length < 2)
            {
                Debug.LogError("Use Skill Arg Length < 2");
                return;
            }
            StartCoroutine(UseSkill((int)_value[0], (bool)_value[1]));
        }
        private void ShowChangePokemon(object[] _value)
        {
            if (_value.Length < 3)
            {
                Debug.LogError("Change Pokemon Arg Length < 2");
                return;
            }
            StartCoroutine(ChangePokemon((int)_value[0], (bool)_value[1], (bool)_value[2]));
        }
        //更换精灵 object为Pokemon
        private void OnRefreshPokemon(object[] _pokmeon)
        {
            Pokemon p = (_pokmeon[0] as Pokemon);
            if (p != null)
            {
                PokemonModel pModel = p.Model;
                if (pModel != null)
                {
                    Sprite statuImg = null;
                    //if (_pokemon.GetStatu() != null)
                    //{
                    //   statuImg=Resources.Load<Sprite>()
                    //}
                    if (p.isMine)
                    {
                        myPokmeonAnimator.SetInteger("id", pModel.id);
                        myState.SetProperty(pModel.name_ch, pModel.hp, p.CurrentHp, statuImg);
                        Skill[] skills = p.Skills;
                        if (skills.Length > 0)
                        {
                            for (int i = 0; i < skills.Length; i++)
                            {
                                int index = i;
                                skillBlocks[i].SetProperty(skills[i].Model.name_ch);
                                skillBlocks[i].GetComponent<Button>().onClick.RemoveAllListeners();
                                skillBlocks[i].GetComponent<Button>().onClick.AddListener(() =>
                                {
                                    clickAudio.PlayOneShot(clickAudio.clip);
                                    PNetworkManager.instance.UseSkill(index);
                                    StartCoroutine(UseSkill(index, true));
                                });
                            }
                        }
                    }
                    else
                    {
                        oppPokmeonAnimator.SetInteger("id", pModel.id);
                        opponentState.SetProperty(pModel.name_ch, pModel.hp, p.CurrentHp, statuImg);
                    }
                }
            }
            else
            {
                Debug.LogError("ChangePokemon is null");
            }
        }
        //战斗不能 object为null
        private void OnPokemonCantFight(object[] _value)
        {
            if (_value.Length < 1)
            {
                Debug.LogError("Cant Fight length < 1");
                return;
            }
            StartCoroutine(CantFight((bool)_value[0]));
        }
        //对手放弃对战
        private void OnGiveUp(object[] _value)
        {
            StartCoroutine(GiveUp());
        }
        //交换回合 _isMyRound代表下一回合是否为自己回合
        private void ChangeRound(bool _isMyRound)
        {
            if (_isMyRound)
            {
                selectPanel.SetActive(true);
                pokemonPanel.SetActive(false);
                skillPanel.SetActive(false);
                messageText.gameObject.SetActive(false);
            }
            else
            {
                selectPanel.SetActive(false);
                pokemonPanel.SetActive(false);
                skillPanel.SetActive(false);
                messageText.gameObject.SetActive(false);
            }
        }
       
        private void OnClickSkillBtn()
        {
            clickAudio.PlayOneShot(clickAudio.clip);
            selectPanel.SetActive(false);
            skillPanel.SetActive(true);
        }
        private void OnClickSkillReturnBtn()
        {
            clickAudio.PlayOneShot(clickAudio.clip);
            skillPanel.SetActive(false);
            selectPanel.SetActive(true);
        }
        private void OnClickPokemonBtn()
        {
            clickAudio.PlayOneShot(clickAudio.clip);
            selectPanel.SetActive(false);
            for(int i=0;i<RuntimeData.PARTY_NUM;i++)
            {
                if(i == RuntimeData.GetCurrentMyIndex())
                {
                    slots[i].SetSelectedable(false);
                }
                else
                {
                    slots[i].SetSelectedable(true);
                }
            }
            pokemonPanel.SetActive(true);
            pokemonReturnBtn.gameObject.SetActive(true);
        }
        private void OnClickPokemonReturnBtn()
        {
            clickAudio.PlayOneShot(clickAudio.clip);
            pokemonPanel.SetActive(false);
            selectPanel.SetActive(true);
        }
        private void OnClickGiveUpBtn()
        {
            clickAudio.PlayOneShot(clickAudio.clip);
            messageText.text = "放弃对战，5秒后退出房间";
            selectPanel.SetActive(false);
            messageText.gameObject.SetActive(true);
            PNetworkManager.instance.GiveUp();
        }

        //文字-开场
        private IEnumerator StartBattle()
        {
            //默认第一个Pokemon先上场
            selectPanel.SetActive(false);
            messageText.text = string.Empty;
            messageText.gameObject.SetActive(true);
            RuntimeData.SetCurrentMyIndex(0);
            RuntimeData.SetCurrentOppIndex(0);
            messageText.text = "对手派出了" + RuntimeData.GetCurrentOppPokemon().Model.name_ch;
            yield return new WaitForSeconds(2f);
            messageText.text = "就决定是你了," + RuntimeData.GetCurrentMyPokemon().Model.name_ch+"!";
            yield return new WaitForSeconds(2f);
            messageText.gameObject.SetActive(false);
            yield return new WaitForSeconds(1f);
            //根据第一个Pokemon速度决定谁先出手
            JudgeRoundBySpeed(null);
        }

        //文字-使用技能
        private IEnumerator UseSkill(int _skillIndex, bool _isMine)
        {
            skillPanel.SetActive(false);
            messageText.text = string.Empty;
            messageText.gameObject.SetActive(true);
            Pokemon p = null;
            if (_isMine)
            {
                p = RuntimeData.GetCurrentMyPokemon();
            }
            else
            {
                p = RuntimeData.GetCurrentOppPokemon();
            }
            if (p != null)
            {
                Skill skill = p.Skills[_skillIndex];
                messageText.text = p.Model.name_ch + "使用了" + skill.Model.name_ch;
                yield return new WaitForSeconds(1f);
                skill.Do(_isMine);
                yield return new WaitForSeconds(1f);
                messageText.gameObject.SetActive(false);
                //被攻击的Pokemon
                Pokemon beHitP = null;
                if (_isMine)
                {
                    beHitP = RuntimeData.GetCurrentOppPokemon();
                }
                else
                {
                    beHitP = RuntimeData.GetCurrentMyPokemon();
                }
                if (beHitP != null)
                {
                    if (beHitP.CurrentHp > 0)
                    {
                        //更换回合
                        ChangeRound(!_isMine);
                    }
                }

            }
        }
        //文字-更换精灵
        private IEnumerator ChangePokemon(int _index, bool _isCantFight, bool isMy)
        {
            pokemonPanel.SetActive(false);
            messageText.text = string.Empty;
            messageText.gameObject.SetActive(true);

            if (isMy)
            {
                if (!_isCantFight)
                {
                    string currentPokemonName = RuntimeData.GetCurrentMyPokemon().Model.name_ch;
                    messageText.text = "幸苦了," + currentPokemonName + ",回来吧!";
                    yield return new WaitForSeconds(1.5f);
                }
                RuntimeData.SetCurrentMyIndex(_index);
                string nextPokmeonName = RuntimeData.GetCurrentMyPokemon().Model.name_ch;
                messageText.text = "就决定是你了，" + nextPokmeonName + "!";
                yield return new WaitForSeconds(1.5f);
                //濒死跟换根据速度判断出手顺序
                if (_isCantFight)
                {
                    JudgeRoundBySpeed(null);
                }
                else
                {
                    ChangeRound(false);
                }
            }
            else
            {
                if (!_isCantFight)
                {
                    string currentPokemonName = RuntimeData.GetCurrentOppPokemon().Model.name_ch;
                    messageText.text = "对方收回了" + currentPokemonName + "。";
                    yield return new WaitForSeconds(1.5f);
                }
                RuntimeData.SetCurrentOppIndex(_index);
                string nextPokemonName = RuntimeData.GetCurrentOppPokemon().Model.name_ch;
                messageText.text = "对方派出了了" + nextPokemonName + "。";
                yield return new WaitForSeconds(1.5f);
                if (_isCantFight)
                {
                    JudgeRoundBySpeed(null);
                }
                else
                {
                    ChangeRound(true);
                }
            }
        }
        private IEnumerator CantFight(bool _isMine)
        {
            messageText.text = string.Empty;
            messageText.gameObject.SetActive(true);
            if (_isMine)
            {
                if (RuntimeData.IsMyGameOver())
                {
                    Debug.Log("Game Over");
                    messageText.text = "已经没有可以出战的宝可梦了，被击败了！\n5秒后退出房间";
                    yield return new WaitForSeconds(5f);
                    PNetworkManager.instance.Stop();
                }
                else
                {
                    messageText.text = RuntimeData.GetCurrentMyPokemon().Model.name_ch + "倒下了";
                    yield return new WaitForSeconds(1f);
                    int index = RuntimeData.GetCurrentMyIndex();
                    Sprite img = Resources.Load<Sprite>(StringUtil.GetStatuUrl(0));
                    slots[index].SetCurrentHp(0);
                    slots[index].SetStatuImg(img);
                    slots[index].GetComponent<Button>().onClick.RemoveAllListeners();
                    pokemonPanel.SetActive(true);
                    pokemonReturnBtn.gameObject.SetActive(false);
                }
            }
            else
            {
                if(RuntimeData.IsOppGameOver())
                {
                    Debug.Log("Win!!!!!!!");
                    messageText.text = "取得胜利！\n5秒后退出房间";
                    yield return new WaitForSeconds(5f);
                    UIManager.instance.ClosePage(PageCollection.BattlePage);
                    UIManager.instance.OpenPage(PageCollection.StartPage);
                }
                else
                {
                    messageText.text ="对方的"+RuntimeData.GetCurrentOppPokemon().Model.name_ch + "倒下了";
                    yield return new WaitForSeconds(1f);
                }
            }
        }
        private IEnumerator GiveUp()
        {
            messageText.text = "对手放弃对战，5秒后退出房间";
            messageText.gameObject.SetActive(true);
            yield return new WaitForSeconds(5f);
            UIManager.instance.ClosePage(PageCollection.BattlePage);
            UIManager.instance.OpenPage(PageCollection.StartPage);
        }
        //根据速度判断谁先出手
        private void JudgeRoundBySpeed(object _value)
        {
            PokemonModel myP = RuntimeData.GetCurrentMyPokemon().Model;
            PokemonModel oppP = RuntimeData.GetCurrentOppPokemon().Model;
            //速度相同随即
            if (myP.speed == oppP.speed)
            {
                if(PNetworkManager.instance.IsServer())
                {
                    int a = Random.Range(1, 1000);
                    int b = Random.Range(1, 1000);
                    if (a >= b)
                    {
                        PNetworkManager.instance.BeginBattleOrder(false);
                        ChangeRound(true);
                    }
                    else
                    {
                        PNetworkManager.instance.BeginBattleOrder(true);
                        ChangeRound(false);
                    }
                }            
            }
            else if (myP.speed > oppP.speed)
            {
                ChangeRound(true);
            }
            else
            {
                ChangeRound(false);
            }
        }
    }

}
