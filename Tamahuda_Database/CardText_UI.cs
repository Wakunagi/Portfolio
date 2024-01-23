using program;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardText_UI : MonoBehaviour {

    //�e�X�N���v�g���Q�Ƃ���ꏊ
    ParameterGetter pr_ScriptsGetter;

    void Start() {
        //ScriptsGetter�̏�����
        pr_ScriptsGetter = ParameterGetter.Instance;
    }



    //�����X�^�[�J�[�h��UI�摜����
    [HeaderAttribute("Card UI Text")]
    [SerializeField] private GameObject card_ui_panel;
    [SerializeField] private Text card_count_text;

    //�f�b�L���̃J�[�h�̐�
    int id_count = 0;

    //�J�[�h��ǉ��A�폜������
    int isChangedDeck = 0;

    //�f�b�L�ɓo�^����p
    public int select_id { private set; get; } = 0;


    //�����X�^�[�J�[�h��UI�摜����
    [SerializeField, HeaderAttribute("Monster UI Image")]
    private GameObject mns_img_panel;   //UI�摜�̃p�l��  
    [SerializeField]                    //�e�e�L�X�g
    private Text
        mns_name_text,
        mns_furigana_text,
        mns_level_text,
        mns_attribute_text,
        mns_atk_text,
        mns_def_text,
        mns_role_text,
        mns_tribe_text,
        mns_effect_text;
    [SerializeField] private Image mns_attribute_img;    //�����̉摜


    //���@�J�[�h��UI�摜����
    [SerializeField, HeaderAttribute("Magic UI Image")]
    private GameObject mgc_img_panel;   //UI�摜�̃p�l��  
    [SerializeField]                    //�e�e�L�X�g
    private Text
        mgc_name_text,
        mgc_furigana_text,
        mgc_attribute_text,
        mgc_role_text,
        mgc_effect_text;



    //�J�[�h��UI�摜�̕\��
    public void DisplayCardImage(int id, CardClass cardClass) {
        isChangedDeck = 0;              //�J�[�h�̐؂�ւ��ɂ�鏉����
        card_ui_panel.SetActive(true);  //CardUI�p�l���̕\��

        id_count = pr_ScriptsGetter.pr_DeckMakeUI.GetCardCount(id); //ID�̃J�[�h�̐��̎擾

        CardCountTextChanger(id_count); //�e�L�X�g�̕ύX

        //�摜�̕\��
        if (cardClass == CardClass.monster) mns_img_panel.SetActive(true);
        if (cardClass == CardClass.magic) mgc_img_panel.SetActive(true);

        select_id = id;                        //ID�̐ݒ�
    }

    //�J�[�h��UI�摜�̔�\��
    public void HideCardImage() {
        if (isChangedDeck != 0)  //�ύX������΃f�b�L���ĕ\��
            pr_ScriptsGetter.pr_DeckMakeUI.DisplayDeckList();

        CardCountTextChanger(0); //�e�L�X�g�̕ύX

        //�摜�̔�\��
        mns_img_panel.SetActive(false);
        mgc_img_panel.SetActive(false);

        card_ui_panel.SetActive(false); //CardUI�p�l���̔�\��
        select_id = 0;                         //ID��������
    }

    //�����X�^�[��UI�摜�̐ݒ�
    public void SetMonsterCardImage(MonsterData data) {
        mns_name_text.text = data.name;
        mns_furigana_text.text = data.furigana;
        mns_level_text.text = data.level.ToString();
        mns_attribute_text.text = data.attribute;
        mns_atk_text.text = data.atk.ToString();
        mns_def_text.text = data.def.ToString();
        mns_role_text.text = data.role;
        mns_tribe_text.text = data.tribe;
        mns_effect_text.text = data.effect;
        mns_attribute_img.color = pr_ScriptsGetter.pr_CardList.GetAttributeColor(data.attribute);
    }

    //���@��UI�摜�̐ݒ�
    public void SetMagicCardImage(MagicData data) {
        mgc_name_text.text = data.name;
        mgc_furigana_text.text = data.furigana;
        mgc_attribute_text.text = data.attribute;
        mgc_role_text.text = data.role;
        mgc_effect_text.text = data.effect;
    }

    //�J�[�h�����e�L�X�g�̕ύX
    void CardCountTextChanger(int count) {
        card_count_text.text = count.ToString() + "/" + DeckMakeUI.CARD_COUNT_MAX;
    }


    //�f�b�L�ɒǉ�
    public void AddCard() {
        if ((id_count + isChangedDeck) >= DeckMakeUI.CARD_COUNT_MAX) return;

        pr_ScriptsGetter.pr_DeckMakeUI.AddCard(select_id);
        isChangedDeck++;   //�ύX���L�^

        CardCountTextChanger(id_count + isChangedDeck); //�e�L�X�g�̕ύX
    }

    //�f�b�L����폜
    public void RemoveCard() {
        if ((id_count + isChangedDeck) <= 0) return;

        pr_ScriptsGetter.pr_DeckMakeUI.RemoveCard(select_id);
        isChangedDeck--;   //�ύX���L�^

        CardCountTextChanger(id_count + isChangedDeck); //�e�L�X�g�̕ύX
    }


}