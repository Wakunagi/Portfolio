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

    //�e�X�N���v�g���Q�Ƃ���ꏊ
    ParameterGetter pr_ScriptsGetter;

    //�e�{�^���̐e
    [HeaderAttribute("Buttton Parent Objects")]
    [SerializeField] GameObject mns_btn_parent;
    [SerializeField] GameObject mns_btn_hide;   //��\���p
    [SerializeField] GameObject mgc_btn_parent;

    //�e�����{�^���̐e
    [HeaderAttribute("Buttton Parent Objects (search)")]
    [SerializeField] GameObject mns_level_btn_parent;
    [SerializeField] GameObject mns_atk_btn_parent;
    [SerializeField] GameObject mns_def_btn_parent;
    [SerializeField] GameObject mns_role_btn_parent;
    [SerializeField] GameObject mns_attribute_btn_parent;
    [SerializeField] GameObject mns_tribe_btn_parent;
    [SerializeField] GameObject mgc_role_btn_parent;
    [SerializeField] GameObject mgc_attribute_btn_parent;

    //�e�{�^���̃v���t�@�u
    [field: HeaderAttribute("Buttton Prefab Objects")]
    [field: SerializeField] public GameObject mns_btn_prefab { private set; get; }
    [field: SerializeField] public GameObject mgc_btn_prefab { private set; get; }
    [field: SerializeField] public GameObject tag_btn_prefab { private set; get; }

    //�쐬�����{�^���̃��X�g
    public List<MonsterButtonData> mns_btn_list { private set; get; } = new List<MonsterButtonData>();
    public List<MagicButtonData> mgc_btn_list { private set; get; } = new List<MagicButtonData>();

    //�J�[�h�����p
    [SerializeField] List<string>[] mns_searcher = new List<string>[(int)CardTag.end];
    [SerializeField] List<string>[] mgc_searcher = new List<string>[(int)CardTag.end];

    void Start() {
        //ScriptsGetter�̏�����
        pr_ScriptsGetter = ParameterGetter.Instance;
    }


    //�����X�^�[�̃{�^���̐���
    public void Make_MonsterButtonList() {

        //���X�g�̏�����
        mns_btn_list = new List<MonsterButtonData>();
        for (CardTag i = 0; i < CardTag.end; i++) mns_searcher[(int)i] = new List<string>();

        //�����X�^�[�̃��X�g���擾
        List<MonsterData> datas = pr_ScriptsGetter.pr_CardList.monsterDatas;

        //�e�����X�^�[�̃f�[�^��ݒ�
        foreach (MonsterData data in datas) {

            //�{�^���̐����ƃv���O�����̎擾
            GameObject btn = Instantiate(mns_btn_prefab);
            MonsterButtonData mns_btn_data = null;
            mns_btn_data = btn.GetComponent<MonsterButtonData>();
            if (mns_btn_data == null) { Debug.LogError("Monster Button does not have MonsterButtonData!"); return; }

            //���X�g�ɓo�^
            mns_btn_list.Add(mns_btn_data);

            //�e��ݒ�
            btn.transform.SetParent(mns_btn_parent.transform);

            //MonsterButtonData�̃Z�b�g�A�b�v
            mns_btn_data.SetUP(data);
        }
    }

    //���@�̃{�^���̐���
    public void Make_MagicButtonList() {

        //���X�g�̏�����
        mgc_btn_list = new List<MagicButtonData>();
        for (CardTag i = 0; i < CardTag.end; i++) mgc_searcher[(int)i] = new List<string>();

        //���@�̃��X�g���擾
        List<MagicData> datas = pr_ScriptsGetter.pr_CardList.magicDatas;

        //�e���@�̃f�[�^��ݒ�
        foreach (MagicData data in datas) {

            //�{�^���̐����ƃv���O�����̎擾
            GameObject btn = Instantiate(mgc_btn_prefab);
            MagicButtonData mgc_btn_data = null;
            mgc_btn_data = btn.GetComponent<MagicButtonData>();
            if (mgc_btn_data == null) { Debug.LogError("Monster Button does not have MagicButtonData!"); return; }

            //���X�g�ɓo�^
            mgc_btn_list.Add(mgc_btn_data);

            //�e��ݒ�
            btn.transform.SetParent(mgc_btn_parent.transform);

            //MagicButtonData�̃Z�b�g�A�b�v
            mgc_btn_data.SetUP(data);
        }
    }

    //�J�[�h�̌����p�{�^���̐���
    public void Make_TagButtonList() {

        //�����X�^�[�̃��x���E�U���́E�����
        Make_TagButton(pr_ScriptsGetter.pr_CardList.maxLevel, mns_level_btn_parent, CardTag.level);
        Make_TagButton(pr_ScriptsGetter.pr_CardList.maxAtk, mns_atk_btn_parent, CardTag.atk);
        Make_TagButton(pr_ScriptsGetter.pr_CardList.maxDef, mns_def_btn_parent, CardTag.def);

        //�����X�^�[�̖���
        Make_TagButton(pr_ScriptsGetter.pr_CardList.CardRoleDatas, mns_role_btn_parent, CardTag.role, CardClass.monster);
        //�����X�^�[�̑���
        Make_TagButton(pr_ScriptsGetter.pr_CardList.MonsterAttributeDatas, mns_attribute_btn_parent, CardTag.attribute, CardClass.monster);
        //�����X�^�[�̎푰
        Make_TagButton(pr_ScriptsGetter.pr_CardList.MonsterTribeDatas, mns_tribe_btn_parent, CardTag.tribe, CardClass.monster);
        //���@�̖���
        Make_TagButton(pr_ScriptsGetter.pr_CardList.CardRoleDatas, mgc_role_btn_parent, CardTag.role, CardClass.magic);
        //���@�̑���
        Make_TagButton(pr_ScriptsGetter.pr_CardList.MagicAttributeDatas, mgc_attribute_btn_parent, CardTag.attribute, CardClass.magic);
    }

    public void Make_TagButton(List<string> datas, GameObject parent, CardTag tag, CardClass card) {

        //�e���@�̃f�[�^��ݒ�
        foreach (string data in datas) {

            //�{�^���̐����ƃv���O�����̎擾
            GameObject btn = Instantiate(tag_btn_prefab);
            CardTypeButtonData tag_btn_data = null;
            tag_btn_data = btn.GetComponent<CardTypeButtonData>();
            if (tag_btn_data == null) { Debug.LogError("Tag Button does not have CardTypeButtonData!"); return; }

            //�e��ݒ�
            btn.transform.SetParent(parent.transform);

            //CardTypeButtonData�̃Z�b�g�A�b�v
            tag_btn_data.SetUP(data, tag, card);
        }
    }
    public void Make_TagButton(int max, GameObject parent, CardTag tag) {

        //�e���@�̃f�[�^��ݒ�
        for (int i = 0; i <= max; i++) {

            //�{�^���̐����ƃv���O�����̎擾
            GameObject btn = Instantiate(tag_btn_prefab);
            CardTypeButtonData tag_btn_data = null;
            tag_btn_data = btn.GetComponent<CardTypeButtonData>();
            if (tag_btn_data == null) { Debug.LogError("Tag Button does not have CardTypeButtonData!"); return; }

            //�e��ݒ�
            btn.transform.SetParent(parent.transform);

            //CardTypeButtonData�̃Z�b�g�A�b�v
            tag_btn_data.SetUP(i.ToString(), tag, CardClass.monster);
        }
    }


    //�J�[�h����
    public void CardSearchSetter(string text, bool isSearch, CardTag tag, CardClass cardClass) {

        //�����X�^�[�̌���
        if (cardClass == CardClass.monster) {
            if (isSearch) mns_searcher[(int)tag].Add(text);
            else mns_searcher[(int)tag].Remove(text);
        }

        //���@�̌���
        else if (cardClass == CardClass.magic) {
            if (isSearch) mgc_searcher[(int)tag].Add(text);
            else mgc_searcher[(int)tag].Remove(text);
        }
    }

    //���͌���
    public void CardSearchSetter_InpuField(string text, CardTag tag, CardClass cardClass) {

        List<string> list = new List<string>();

        //�����X�^�[�̌���
        if (cardClass == CardClass.monster) { list = mns_searcher[(int)tag]; }

        //���@�̌���
        else if (cardClass == CardClass.magic) { list = mgc_searcher[(int)tag]; }

        //�������e�̐ݒ�
        if (text == string.Empty) list.Clear();
        else {
            if (list.Count == 0) list.Add(text);
            else list[0] = text;
        }


    }

    //�����X�^�[�̌����{�^���������ꂽ��
    public void MonsterSearcher() {
        //�e�{�^���ɑ΂����s
        foreach (MonsterButtonData btn in mns_btn_list) Searcher(btn, null);
    }

    //���@�̌����{�^���������ꂽ��
    public void MagicSearcher() {
        //�e�{�^���ɑ΂����s
        foreach (MagicButtonData btn in mgc_btn_list) Searcher(null, btn);
    }


    //�����p�֐�
    void Searcher(MonsterButtonData mns_btn, MagicButtonData mgc_btn) {

        bool isActive = true;   //�B�����ǂ���
        bool isSearched = false;//�����������ǂ���

        if (!((mns_btn != null) ^ (mgc_btn != null))) {
            Debug.LogError("!((mns_btn != null) ^ (mgc_btn != null)) == false");
            return;
        }

        //�w�肠�邩�̔���
        for (CardTag i = 0; i < CardTag.end; i++) {
            if (mns_btn != null) if (mns_searcher[(int)i].Count != 0) isActive = false;
            if (mgc_btn != null) if (mgc_searcher[(int)i].Count != 0) isActive = false;
        }

        //�w�肪����ꍇ
        if (!isActive) {

            //�e�^�O�ɂ��Ď��s
            for (CardTag i = 0; i < CardTag.end; i++) {

                if (isSearched && !isActive) break;

                //�J�[�h�̌������e�̓��e���擾
                string data_text = null;
                List<string> list = new List<string>();

                //�����X�^�[�̏ꍇ
                if (mns_btn != null) {
                    data_text = pr_ScriptsGetter.pr_CardList.MonsterDataGetter(mns_btn.data, i);
                    list = mns_searcher[(int)i];
                }
                //���@�̏ꍇ
                else if (mgc_btn != null) {
                    data_text = pr_ScriptsGetter.pr_CardList.MagicDataGetter(mgc_btn.data, i);
                    list = mgc_searcher[(int)i];
                }

                if (data_text == null) { continue; }

                //�^�O�Ɏw�肪�����ꍇ�X�L�b�v
                if (list.Count == 0) continue;

                //�������e�ƈ�v���邩�ǂ���
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


    //�����X�^�[�̃{�^���̃\�[�g
    public void MonsterButtonSort_Normal() {
        foreach (MonsterButtonData btn in mns_btn_list) { btn.transform.SetParent(mns_btn_hide.transform); }
        foreach (MonsterButtonData btn in mns_btn_list) { btn.transform.SetParent(mns_btn_parent.transform); }
    }

    //�����X�^�[�̃{�^���̃\�[�g
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

    //�����X�^�[�̃{�^���̃\�[�g
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