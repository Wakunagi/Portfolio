using program;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchInputFieldData : MonoBehaviour {

    //各スクリプトを参照する場所
    ParameterGetter pr_ScriptsGetter;

    void Start() {
        //ScriptsGetterの初期化
        pr_ScriptsGetter = ParameterGetter.Instance;

        //名前を検索用に設定しておく
        search_name = true;
        search_effect = false;
        name_btn_img.color = push_btn_color;
        effect_btn_img.color = Color.white;
    }

    //オブジェクトと結びつける
    [SerializeField] InputField inputField;

    //ボタンのImage
    [SerializeField] Image name_btn_img, effect_btn_img;

    //モンスターと魔法のどちらを検索するか
    [SerializeField] CardClass myClass;

    //ボタンを押したときの色
    [SerializeField] Color push_btn_color;

    bool search_name, search_effect;

    //片方のみOnになる
    public void OnClickNameTgl() {
        search_name = true;
        search_effect = false;

        name_btn_img.color = push_btn_color;
        effect_btn_img.color = Color.white;

        OnChangedText();
    }
    public void OnClickEffectTgl() {
        search_name = false;
        search_effect = true;

        name_btn_img.color = Color.white;
        effect_btn_img.color = push_btn_color;

        OnChangedText();
    }

    //入力に変化が生じたら実行
    public void OnChangedText() {

        if (myClass == CardClass.end) return;

        if (search_name)
            pr_ScriptsGetter.pr_UIButtonListMaker.
                CardSearchSetter_InpuField(inputField.text, CardTag.name, myClass);
        else
            pr_ScriptsGetter.pr_UIButtonListMaker.
                CardSearchSetter_InpuField(string.Empty, CardTag.name, myClass);

        if (search_effect)
            pr_ScriptsGetter.pr_UIButtonListMaker.
                CardSearchSetter_InpuField(inputField.text, CardTag.effect, myClass);
        else
            pr_ScriptsGetter.pr_UIButtonListMaker.
                CardSearchSetter_InpuField(string.Empty, CardTag.effect, myClass);
    }
}