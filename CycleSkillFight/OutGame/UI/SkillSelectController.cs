using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using Unity.Mathematics;
using Unity.VisualScripting;

using Program.GameSystem;
using Program.GameSystem.Data;


namespace Program.OutGame.UI {

    public class SkillSelectController : InputManager {

        [field: SerializeField] public FighterSkillData fighterData { private set; get; }
        [SerializeField] private SkillDictionary dictionry;
        [SerializeField] private FighterDatas fighterDictionry;
        [SerializeField] private GameObject sbtn_parent, sbtn_prefab;
        [SerializeField] private GameObject player_sbtn_parent, player_sbtn_prefab;

        [SerializeField] private GameObject SelectSkillDataPanel;

        private List<SButtonData> sbtnDatas = new List<SButtonData>();
        private List<Player_SButtonData> player_sbtnDatas = new List<Player_SButtonData>();

        [SerializeField] private Scrollbar sbtn_scrollbar;
        int display_skill_count = 0;

        int target_num;

        int select_sbtn = 0;
        int select_playerSBtn = 0;

        int[] skillCount = new int[(int)SkillPower.End];
        SkillPower isDisplaySelectPanel = SkillPower.End;

        [SerializeField] GameObject playStart_Panel;
        [SerializeField] GameObject playStart_choiceObj;
        [SerializeField] GameObject fighters_choiceObj;
        [SerializeField] Text fighters_text,fighter_status_text;

        List<GameObject> sbtn_choiceObjs = new List<GameObject>();
        List<GameObject> playerSBtn_choiceObjs = new List<GameObject>();

        int fighterData_count = 0;

        bool isOnPlayStartBtn = false;
        bool isOnFighterBtn = false;
        bool isDecision = false;

        [SerializeField]Text skill_name_text, skill_explanation_text;


        void Start() {
            //各パワーのスキルの個数を初期化           
            for (int i = 0; i < skillCount.Length; i++) skillCount[i] = 0;

            playerSBtn_choiceObjs.Add(playStart_choiceObj);
            playerSBtn_choiceObjs.Add(fighters_choiceObj);

            SetSButtons();
            SetPlayerSButtons();
            OnClick_DisplaySelectPanel(0);

            isDisplaySelectPanel = SkillPower.End;
            SelectSkillDataPanel.SetActive(false);
            playStart_Panel.SetActive(false);
            DisplaySelectObj(player_sbtnDatas[0].choice, playerSBtn_choiceObjs);
            FighterSetter(0);
        }

        //左右の入力
        protected override void InputStick(InputPattern pattern) {
            if (isDecision) return;


            int num = 0;

            

            //スキル選択中
            if (isDisplaySelectPanel != SkillPower.End) {

                if (pattern == InputPattern.Right) num = 1;
                else if (pattern == InputPattern.Left) num = -1;
                else return;


                if (select_sbtn + num < 0) { return; }
                if (select_sbtn + num > (skillCount[(int)isDisplaySelectPanel] - 1)) { return; }

                select_sbtn += num;

                Debug.Log("Num : "+num+" Bar : " + (float)((float)num / (float)display_skill_count) + " count : " + display_skill_count);

                sbtn_scrollbar.value += (float)((float)num / (float)(display_skill_count-1));

                //説明の変更
                skill_name_text.text = sbtnDatas[GetSkillNumInPower(isDisplaySelectPanel, select_sbtn)].data.name;
                skill_explanation_text.text = sbtnDatas[GetSkillNumInPower(isDisplaySelectPanel, select_sbtn)].data.explanation;

                DisplaySelectObj(sbtnDatas[GetSkillNumInPower(isDisplaySelectPanel, select_sbtn)].choice, sbtn_choiceObjs);
            }

            //どのスキルを変更するかを選択中
            else {
                if (pattern == InputPattern.Right) num = 1;
                else if (pattern == InputPattern.Left) num = -1;
                else if (pattern == InputPattern.Up) num = -3;
                else if (pattern == InputPattern.Down) num = 3;
                else return;

                if (isOnFighterBtn) {
                    if ((pattern == InputPattern.Right) || (pattern == InputPattern.Left)) {
                        FighterSetter(num);
                        return;
                    }
                    else if ((pattern == InputPattern.Down)) num = 1;
                }

                if (select_playerSBtn + num < 0) {
                    select_playerSBtn = -1;
                    isOnFighterBtn = true;
                    DisplaySelectObj(fighters_choiceObj, playerSBtn_choiceObjs);
                    FighterSetter(0);
                    return;
                }
                if (select_playerSBtn + num > MyConst.SKILL_MAX - 1) {
                    select_playerSBtn = MyConst.SKILL_MAX;
                    isOnPlayStartBtn = true;
                    DisplaySelectObj(playStart_choiceObj, playerSBtn_choiceObjs);
                    return;
                }

                isOnPlayStartBtn = false;
                isOnFighterBtn = false;
                select_playerSBtn += num;

                DisplaySelectObj(player_sbtnDatas[select_playerSBtn].choice, playerSBtn_choiceObjs);
            }

        }

        //fighterの設定
       public void FighterSetter(int delta) {
            fighterData_count+= delta;
            if(fighterData_count < 0)fighterData_count = fighterDictionry.datas.Count-1;
            if (fighterData_count >= fighterDictionry.datas.Count) fighterData_count = 0;

            fighterData.SetFighterStatus( fighterDictionry.datas[fighterData_count]);
            fighters_text.text = fighterDictionry.datas[fighterData_count].name;

            string text =
                "名　前　 : " + fighterDictionry.datas[fighterData_count].name + "\n"+
                "ライフ　 : " + fighterDictionry.datas[fighterData_count].life + "\n" +
                "攻撃力　 : " + fighterDictionry.datas[fighterData_count].atk + "\n" +
                "防御力　 : " + fighterDictionry.datas[fighterData_count].def + "\n" +
                "スピード : " + fighterDictionry.datas[fighterData_count].spd;

            fighter_status_text.text = text;

        }

        //決定の入力
        protected override void InputDecision() {

            if (isOnFighterBtn) return;

            if (isOnPlayStartBtn) {
                OnClick_PlayStart();
                return;
            }

            if (isDisplaySelectPanel != SkillPower.End) {
                int snum = GetSkillNumInPower(isDisplaySelectPanel, select_sbtn);
                if (snum < 0) { Debug.Log("Skill is not found."); return; }
                OnClick_SetTargetSkill(snum);
            }
            else {
                OnClick_DisplaySelectPanel(select_playerSBtn);
            }

        }

        //PlayerButtonの初期設定
        void SetPlayerSButtons() {
            for (int i = 0; i < MyConst.SKILL_MAX; i++) {

                GameObject data_obj = Instantiate(player_sbtn_prefab);
                data_obj.transform.SetParent(player_sbtn_parent.transform);

                //Player_SButtonへの割り当て
                Player_SButtonData btn_data = data_obj.GetComponent<Player_SButtonData>();

                //エラー処理
                if ((btn_data.img == null) || (btn_data.btn == null) || (btn_data.choice == null)) {
                    Destroy(data_obj);
                    Debug.LogError("data_obj does not have Component.");
                    return;
                }

                //画僧変更
                SkillData data = dictionry.GetSkillData(fighterData.skills[i]);
                if (data == null) {
                    foreach (SkillData skillData in dictionry.datas)
                        if (skillData.skillPower == GetSkillPower(i)) {
                            fighterData.SetSkill(i, skillData.name_id);
                            data = skillData;
                        }
                }
                btn_data.img.sprite = data.image;

                //ボタンへの割り当て
                int a = i;
                btn_data.btn.onClick.AddListener(() => OnClick_DisplaySelectPanel(a));

                //ナンバーテキストを変更
                a++;
                if (a > MyConst.LOW_MAX) {
                    a -= MyConst.LOW_MAX;
                    if (a > MyConst.MIDDLE_MAX) a -= MyConst.MIDDLE_MAX;
                }
                btn_data.num_text.text =a.ToString();

                //ボタンの登録
                player_sbtnDatas.Add(btn_data);

                //セレクト中に表示するオブジェクトをリストへ登録
                playerSBtn_choiceObjs.Add(btn_data.choice);
            }
        }

        //SkillButtonの初期設定
        void SetSButtons() {
            for (int i = 0; i < dictionry.datas.Count; i++) {
                SkillData data = dictionry.datas[i];

                GameObject data_obj = Instantiate(sbtn_prefab);
                data_obj.transform.SetParent(sbtn_parent.transform);

                //SButtonへの割り当て
                SButtonData btn_data = new SButtonData();
                btn_data.obj = data_obj;
                btn_data.img = data_obj.transform.GetChild(MyConst.SKILL_IMAGE).GetComponent<Image>();
                btn_data.btn = data_obj.GetComponent<Button>();
                btn_data.choice = data_obj.transform.GetChild(MyConst.CHOICE_IMAGE).gameObject;
                btn_data.data = data;

                //エラー処理
                if ((btn_data.img == null) || (btn_data.btn == null) || (btn_data.choice == null)) {
                    Destroy(data_obj);
                    Debug.LogError("data_obj does not have Component.");
                    return;
                }

                //画像変更
                btn_data.img.sprite = data.image;

                //ボタンの設定
                int a = i;
                btn_data.btn.onClick.AddListener(() => OnClick_SetTargetSkill(a));

                //各パワーのスキルの個数
                skillCount[(int)data.skillPower]++;

                //ボタンの登録
                sbtnDatas.Add(btn_data);

                //セレクト中に表示するオブジェクトをリストへ登録
                sbtn_choiceObjs.Add(btn_data.choice);

            }
        }

        //スキル選択画面を開いたとき
        public void OnClick_DisplaySelectPanel(int cycleNum) {

            SkillPower skillPower = GetSkillPower(cycleNum);
            if (skillPower == SkillPower.End) return;

            display_skill_count = 0;

            //skillPowerが同じなら表示
            foreach (SButtonData btn in sbtnDatas) {
                if (btn.data.skillPower == skillPower) {
                    btn.obj.SetActive(true);
                    display_skill_count++;
                }
                else
                    btn.obj.SetActive(false);
            }

            //選択中のスキルがどれかを表示する
            DisplaySelectObj(sbtnDatas[GetSkillNumInPower(skillPower, select_sbtn)].choice, sbtn_choiceObjs);

            target_num = cycleNum;
            isDisplaySelectPanel = skillPower;

            SelectSkillDataPanel.SetActive(true);

            Debug.Log("sbum in power : " + select_sbtn);

            //スクロールバーの調整
            sbtn_scrollbar.value = (float)((float)select_sbtn / (float)(display_skill_count - 1));

            //説明の変更
            skill_name_text.text = sbtnDatas[GetSkillNumInPower(isDisplaySelectPanel, select_sbtn)].data.name;
            skill_explanation_text.text = sbtnDatas[GetSkillNumInPower(isDisplaySelectPanel, select_sbtn)].data.explanation;
        }

        //スキルを選んだ時
        public void OnClick_SetTargetSkill(int snum) {
            SelectSkillDataPanel.SetActive(false);
            SButtonData sbtn_data = sbtnDatas[snum];
            fighterData.SetSkill(target_num, sbtn_data.data.name_id);
            player_sbtnDatas[target_num].img.sprite = sbtn_data.img.sprite;
            isDisplaySelectPanel = SkillPower.End;
            select_sbtn = 0;
        }

        //スキル選択の終了
        public void OnClick_PlayStart() {
            isDecision = !isDecision;
            int n = 0;
            if (isDecision) { n = 1; playStart_Panel.SetActive(true); }
            else { n = -1; playStart_Panel.SetActive(false); }
            sceneChangeManager.SceneChanger(n);
        }

        //スキルパワーの取得
        public static SkillPower GetSkillPower(int power) {
            if (power < MyConst.LOW_MAX)
                return SkillPower.Low;
            else if (power < MyConst.LOW_MAX + MyConst.MIDDLE_MAX)
                return SkillPower.Middle;
            else if (power < MyConst.LOW_MAX + MyConst.MIDDLE_MAX + MyConst.HIGH_MAX)
                return SkillPower.High;
            else {
                Debug.Log("Power is Big." + power);
                return SkillPower.End;
            }
        }

        //スキルパワーに応じたN番目のスキルを取得
        int GetSkillNumInPower(SkillPower spower, int n) {
            int scount = 0;
            for (int i = 0; i < sbtnDatas.Count; i++) {
                if (sbtnDatas[i].data.skillPower == spower) {
                    if (scount == n) return i;
                    scount++;
                }
            }
            return -1;
        }

        //セレクトを表示
        void DisplaySelectObj(GameObject obj, List<GameObject> hideList) {
            foreach (GameObject hideObj in hideList) hideObj.SetActive(false);
            obj.SetActive(true);
        }

        [System.Serializable]
        public class SButtonData {
            public SkillData data = null;
            public GameObject obj = null;
            public Image img = null;
            public Button btn = null;
            public GameObject choice = null;
        }

    }
}