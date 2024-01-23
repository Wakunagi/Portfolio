using program;
using program.magic;
using program.monster;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class UIButtonListMaker : MonoBehaviour {

    //各スクリプトを参照する場所
    ParameterGetter pr_ScriptsGetter;

    //各ボタンの親
    [HeaderAttribute("Buttton Parent Objects")]
    [SerializeField] GameObject mns_btn_parent;
    [SerializeField] GameObject mns_btn_hide;   //非表示用
    [SerializeField] GameObject mgc_btn_parent;

    //各検索ボタンの親
    [HeaderAttribute("Buttton Parent Objects (search)")]
    [SerializeField] GameObject mns_level_btn_parent;
    [SerializeField] GameObject mns_atk_btn_parent;
    [SerializeField] GameObject mns_def_btn_parent;
    [SerializeField] GameObject mns_role_btn_parent;
    [SerializeField] GameObject mns_attribute_btn_parent;
    [SerializeField] GameObject mns_tribe_btn_parent;
    [SerializeField] GameObject mgc_role_btn_parent;
    [SerializeField] GameObject mgc_attribute_btn_parent;

    //各ボタンのプレファブ
    [field: HeaderAttribute("Buttton Prefab Objects")]
    [field: SerializeField] public GameObject mns_btn_prefab { private set; get; }
    [field: SerializeField] public GameObject mgc_btn_prefab { private set; get; }
    [field: SerializeField] public GameObject tag_btn_prefab { private set; get; }

    //作成したボタンのリスト
    public List<MonsterButtonData> mns_btn_list { private set; get; } = new List<MonsterButtonData>();
    public List<MagicButtonData> mgc_btn_list { private set; get; } = new List<MagicButtonData>();

    //カード検索用
    [SerializeField] List<string>[] mns_searcher = new List<string>[(int)CardTag.end];
    [SerializeField] List<string>[] mgc_searcher = new List<string>[(int)CardTag.end];

    void Start() {
        //ScriptsGetterの初期化
        pr_ScriptsGetter = ParameterGetter.Instance;
    }


    //モンスターのボタンの生成
    public void Make_MonsterButtonList() {

        //リストの初期化
        mns_btn_list = new List<MonsterButtonData>();
        for (CardTag i = 0; i < CardTag.end; i++) mns_searcher[(int)i] = new List<string>();

        //モンスターのリストを取得
        List<MonsterData> datas = pr_ScriptsGetter.pr_CardList.monsterDatas;

        //各モンスターのデータを設定
        foreach (MonsterData data in datas) {

            //ボタンの生成とプログラムの取得
            GameObject btn = Instantiate(mns_btn_prefab);
            MonsterButtonData mns_btn_data = null;
            mns_btn_data = btn.GetComponent<MonsterButtonData>();
            if (mns_btn_data == null) { Debug.LogError("Monster Button does not have MonsterButtonData!"); return; }

            //リストに登録
            mns_btn_list.Add(mns_btn_data);

            //親を設定
            btn.transform.SetParent(mns_btn_parent.transform);

            //MonsterButtonDataのセットアップ
            mns_btn_data.SetUP(data);
        }
    }

    //魔法のボタンの生成
    public void Make_MagicButtonList() {

        //リストの初期化
        mgc_btn_list = new List<MagicButtonData>();
        for (CardTag i = 0; i < CardTag.end; i++) mgc_searcher[(int)i] = new List<string>();

        //魔法のリストを取得
        List<MagicData> datas = pr_ScriptsGetter.pr_CardList.magicDatas;

        //各魔法のデータを設定
        foreach (MagicData data in datas) {

            //ボタンの生成とプログラムの取得
            GameObject btn = Instantiate(mgc_btn_prefab);
            MagicButtonData mgc_btn_data = null;
            mgc_btn_data = btn.GetComponent<MagicButtonData>();
            if (mgc_btn_data == null) { Debug.LogError("Monster Button does not have MagicButtonData!"); return; }

            //リストに登録
            mgc_btn_list.Add(mgc_btn_data);

            //親を設定
            btn.transform.SetParent(mgc_btn_parent.transform);

            //MagicButtonDataのセットアップ
            mgc_btn_data.SetUP(data);
        }
    }

    //カードの検索用ボタンの生成
    public void Make_TagButtonList() {

        //モンスターのレベル・攻撃力・守備力
        Make_TagButton(pr_ScriptsGetter.pr_CardList.maxLevel, mns_level_btn_parent, CardTag.level);
        Make_TagButton(pr_ScriptsGetter.pr_CardList.maxAtk, mns_atk_btn_parent, CardTag.atk);
        Make_TagButton(pr_ScriptsGetter.pr_CardList.maxDef, mns_def_btn_parent, CardTag.def);

        //モンスターの役割
        Make_TagButton(pr_ScriptsGetter.pr_CardList.CardRoleDatas, mns_role_btn_parent, CardTag.role, CardClass.monster);
        //モンスターの属性
        Make_TagButton(pr_ScriptsGetter.pr_CardList.MonsterAttributeDatas, mns_attribute_btn_parent, CardTag.attribute, CardClass.monster);
        //モンスターの種族
        Make_TagButton(pr_ScriptsGetter.pr_CardList.MonsterTribeDatas, mns_tribe_btn_parent, CardTag.tribe, CardClass.monster);
        //魔法の役割
        Make_TagButton(pr_ScriptsGetter.pr_CardList.CardRoleDatas, mgc_role_btn_parent, CardTag.role, CardClass.magic);
        //魔法の属性
        Make_TagButton(pr_ScriptsGetter.pr_CardList.MagicAttributeDatas, mgc_attribute_btn_parent, CardTag.attribute, CardClass.magic);
    }

    public void Make_TagButton(List<string> datas, GameObject parent, CardTag tag, CardClass card) {

        //各魔法のデータを設定
        foreach (string data in datas) {

            //ボタンの生成とプログラムの取得
            GameObject btn = Instantiate(tag_btn_prefab);
            CardTypeButtonData tag_btn_data = null;
            tag_btn_data = btn.GetComponent<CardTypeButtonData>();
            if (tag_btn_data == null) { Debug.LogError("Tag Button does not have CardTypeButtonData!"); return; }

            //親を設定
            btn.transform.SetParent(parent.transform);

            //CardTypeButtonDataのセットアップ
            tag_btn_data.SetUP(data, tag, card);
        }
    }
    public void Make_TagButton(int max, GameObject parent, CardTag tag) {

        //各魔法のデータを設定
        for (int i = 0; i <= max; i++) {

            //ボタンの生成とプログラムの取得
            GameObject btn = Instantiate(tag_btn_prefab);
            CardTypeButtonData tag_btn_data = null;
            tag_btn_data = btn.GetComponent<CardTypeButtonData>();
            if (tag_btn_data == null) { Debug.LogError("Tag Button does not have CardTypeButtonData!"); return; }

            //親を設定
            btn.transform.SetParent(parent.transform);

            //CardTypeButtonDataのセットアップ
            tag_btn_data.SetUP(i.ToString(), tag, CardClass.monster);
        }
    }


    //カード検索
    public void CardSearchSetter(string text, bool isSearch, CardTag tag, CardClass cardClass) {

        //モンスターの検索
        if (cardClass == CardClass.monster) {
            if (isSearch) mns_searcher[(int)tag].Add(text);
            else mns_searcher[(int)tag].Remove(text);
        }

        //魔法の検索
        else if (cardClass == CardClass.magic) {
            if (isSearch) mgc_searcher[(int)tag].Add(text);
            else mgc_searcher[(int)tag].Remove(text);
        }
    }

    //文章検索
    public void CardSearchSetter_InpuField(string text, CardTag tag, CardClass cardClass) {

        List<string> list = new List<string>();

        //モンスターの検索
        if (cardClass == CardClass.monster) { list = mns_searcher[(int)tag]; }

        //魔法の検索
        else if (cardClass == CardClass.magic) { list = mgc_searcher[(int)tag]; }

        //検索内容の設定
        if (text == string.Empty) list.Clear();
        else {
            if (list.Count == 0) list.Add(text);
            else list[0] = text;
        }


    }

    //モンスターの検索ボタンが押された時
    public void MonsterSearcher() {
        //各ボタンに対し実行
        foreach (MonsterButtonData btn in mns_btn_list) Searcher(btn, null);
    }

    //魔法の検索ボタンが押された時
    public void MagicSearcher() {
        //各ボタンに対し実行
        foreach (MagicButtonData btn in mgc_btn_list) Searcher(null, btn);
    }


    //検索用関数
    void Searcher(MonsterButtonData mns_btn, MagicButtonData mgc_btn) {

        bool isActive = true;   //隠すかどうか
        bool isSearched = false;//検索したかどうか

        if (!((mns_btn != null) ^ (mgc_btn != null))) {
            Debug.LogError("!((mns_btn != null) ^ (mgc_btn != null)) == false");
            return;
        }

        //指定あるかの判定
        for (CardTag i = 0; i < CardTag.end; i++) {
            if (mns_btn != null) if (mns_searcher[(int)i].Count != 0) isActive = false;
            if (mgc_btn != null) if (mgc_searcher[(int)i].Count != 0) isActive = false;
        }

        //指定がある場合
        if (!isActive) {

            //各タグについて実行
            for (CardTag i = 0; i < CardTag.end; i++) {

                if (isSearched && !isActive) break;

                //カードの検索内容の内容を取得
                string data_text = null;
                List<string> list = new List<string>();

                //モンスターの場合
                if (mns_btn != null) {
                    data_text = pr_ScriptsGetter.pr_CardList.MonsterDataGetter(mns_btn.data, i);
                    list = mns_searcher[(int)i];
                }
                //魔法の場合
                else if (mgc_btn != null) {
                    data_text = pr_ScriptsGetter.pr_CardList.MagicDataGetter(mgc_btn.data, i);
                    list = mgc_searcher[(int)i];
                }

                if (data_text == null) { continue; }

                //タグに指定が無い場合スキップ
                if (list.Count == 0) continue;

                //検索内容と一致するかどうか
                foreach (string text in list) {
                    if (data_text.Contains(text)) { isActive = true; break; }
                    else { isActive = false; }
                }

                isSearched = true;
            }
        }

        if (mns_btn != null) mns_btn.gameObject.SetActive(isActive);
        else if (mgc_btn != null) mgc_btn.gameObject.SetActive(isActive);
    }


    //モンスターのボタンのソート
    public void MonsterButtonSort_Normal() {
        foreach (MonsterButtonData btn in mns_btn_list) { btn.transform.SetParent(mns_btn_hide.transform); }
        foreach (MonsterButtonData btn in mns_btn_list) { btn.transform.SetParent(mns_btn_parent.transform); }
    }

    //モンスターのボタンのソート
    public void MonsterButtonSort_String(CardTag tag) {

        foreach (MonsterButtonData btn in mns_btn_list) { btn.transform.SetParent(mns_btn_hide.transform); }

        List<string> list = pr_ScriptsGetter.pr_CardList.MonsterCardTagDataGetter(tag);

        if (list == null)
            foreach (MonsterButtonData btn in mns_btn_list) { btn.transform.SetParent(mns_btn_parent.transform); }
        else {
            foreach (string text in list) {
                foreach (MonsterButtonData btn in mns_btn_list) {
                    if (pr_ScriptsGetter.pr_CardList.MonsterDataGetter(btn.data, tag) == text)
                        btn.transform.SetParent(mns_btn_parent.transform);
                }
            }
        }
    }

    //モンスターのボタンのソート
    public void MonsterButtonSort_Max(CardTag tag,bool isDownToUp) {

        foreach (MonsterButtonData btn in mns_btn_list) { btn.transform.SetParent(mns_btn_hide.transform); }

        int max = pr_ScriptsGetter.pr_CardList.MonsterStatusDataGetter(tag);

        if (max <0)
            foreach (MonsterButtonData btn in mns_btn_list) { btn.transform.SetParent(mns_btn_parent.transform); }
        else {
            if (isDownToUp) {
                for (int i = 0; i <= max; i++) {
                    foreach (MonsterButtonData btn in mns_btn_list) {
                        if (pr_ScriptsGetter.pr_CardList.MonsterDataGetter(btn.data, tag) == i.ToString())
                            btn.transform.SetParent(mns_btn_parent.transform);
                    }
                }
            }
            else {
                for (int i = max; i >= 0; i--) {
                    foreach (MonsterButtonData btn in mns_btn_list) {
                        if (pr_ScriptsGetter.pr_CardList.MonsterDataGetter(btn.data, tag) == i.ToString())
                            btn.transform.SetParent(mns_btn_parent.transform);
                    }
                }
            }
        }
    }

}