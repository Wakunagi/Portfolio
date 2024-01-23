using program;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardText_UI : MonoBehaviour {

    //各スクリプトを参照する場所
    ParameterGetter pr_ScriptsGetter;

    void Start() {
        //ScriptsGetterの初期化
        pr_ScriptsGetter = ParameterGetter.Instance;
    }



    //モンスターカードのUI画像生成
    [HeaderAttribute("Card UI Text")]
    [SerializeField] private GameObject card_ui_panel;
    [SerializeField] private Text card_count_text;

    //デッキ内のカードの数
    int id_count = 0;

    //カードを追加、削除したか
    int isChangedDeck = 0;

    //デッキに登録する用
    public int select_id { private set; get; } = 0;


    //モンスターカードのUI画像生成
    [SerializeField, HeaderAttribute("Monster UI Image")]
    private GameObject mns_img_panel;   //UI画像のパネル  
    [SerializeField]                    //各テキスト
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
    [SerializeField] private Image mns_attribute_img;    //属性の画像


    //魔法カードのUI画像生成
    [SerializeField, HeaderAttribute("Magic UI Image")]
    private GameObject mgc_img_panel;   //UI画像のパネル  
    [SerializeField]                    //各テキスト
    private Text
        mgc_name_text,
        mgc_furigana_text,
        mgc_attribute_text,
        mgc_role_text,
        mgc_effect_text;



    //カードのUI画像の表示
    public void DisplayCardImage(int id, CardClass cardClass) {
        isChangedDeck = 0;              //カードの切り替えによる初期化
        card_ui_panel.SetActive(true);  //CardUIパネルの表示

        id_count = pr_ScriptsGetter.pr_DeckMakeUI.GetCardCount(id); //IDのカードの数の取得

        CardCountTextChanger(id_count); //テキストの変更

        //画像の表示
        if (cardClass == CardClass.monster) mns_img_panel.SetActive(true);
        if (cardClass == CardClass.magic) mgc_img_panel.SetActive(true);

        select_id = id;                        //IDの設定
    }

    //カードのUI画像の非表示
    public void HideCardImage() {
        if (isChangedDeck != 0)  //変更があればデッキを再表示
            pr_ScriptsGetter.pr_DeckMakeUI.DisplayDeckList();

        CardCountTextChanger(0); //テキストの変更

        //画像の非表示
        mns_img_panel.SetActive(false);
        mgc_img_panel.SetActive(false);

        card_ui_panel.SetActive(false); //CardUIパネルの非表示
        select_id = 0;                         //IDを初期化
    }

    //モンスターのUI画像の設定
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

    //魔法のUI画像の設定
    public void SetMagicCardImage(MagicData data) {
        mgc_name_text.text = data.name;
        mgc_furigana_text.text = data.furigana;
        mgc_attribute_text.text = data.attribute;
        mgc_role_text.text = data.role;
        mgc_effect_text.text = data.effect;
    }

    //カード枚数テキストの変更
    void CardCountTextChanger(int count) {
        card_count_text.text = count.ToString() + "/" + DeckMakeUI.CARD_COUNT_MAX;
    }


    //デッキに追加
    public void AddCard() {
        if ((id_count + isChangedDeck) >= DeckMakeUI.CARD_COUNT_MAX) return;

        pr_ScriptsGetter.pr_DeckMakeUI.AddCard(select_id);
        isChangedDeck++;   //変更を記録

        CardCountTextChanger(id_count + isChangedDeck); //テキストの変更
    }

    //デッキから削除
    public void RemoveCard() {
        if ((id_count + isChangedDeck) <= 0) return;

        pr_ScriptsGetter.pr_DeckMakeUI.RemoveCard(select_id);
        isChangedDeck--;   //変更を記録

        CardCountTextChanger(id_count + isChangedDeck); //テキストの変更
    }


}