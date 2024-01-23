using program;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterButtonSort : MonoBehaviour
{

    //各スクリプトを参照する場所
    ParameterGetter pr_ScriptsGetter;

    //各UIのデータ
    [HeaderAttribute("Button Text Datas")]
    [SerializeField] Image normal_btn;
    [SerializeField] Image attribute_btn, tribe_btn, level_dtu_btn, level_utd_btn, atk_dtu_btn, atk_utd_btn, def_dtu_btn, def_utd_btn;

    //dtu -> Down to Up
    //utd -> Up to Down

    //ボタンを押したときの色
    [SerializeField] Color push_btn_color;

    void Start() {
        //ScriptsGetterの初期化
        pr_ScriptsGetter = ParameterGetter.Instance;

        AllImgWhite();
        normal_btn.color = push_btn_color;
    }

    public void Sort_Noamal() {
        AllImgWhite();
        normal_btn.color = push_btn_color;
        pr_ScriptsGetter.pr_UIButtonListMaker.MonsterButtonSort_Normal();
    }


    public void Sort_Attribute() {
        AllImgWhite();
        attribute_btn.color = push_btn_color;
        pr_ScriptsGetter.pr_UIButtonListMaker.MonsterButtonSort_String(CardTag.attribute);
    }
    public void Sort_Tribe() {
        AllImgWhite();
        tribe_btn.color = push_btn_color;
        pr_ScriptsGetter.pr_UIButtonListMaker.MonsterButtonSort_String(CardTag.tribe);
    }


    public void Sort_Level_DownToUp() {
        AllImgWhite();
        level_dtu_btn.color = push_btn_color;
        pr_ScriptsGetter.pr_UIButtonListMaker.MonsterButtonSort_Max(CardTag.level,true);
    }
    public void Sort_Level_UpToDown() {
        AllImgWhite();
        level_utd_btn.color = push_btn_color;
        pr_ScriptsGetter.pr_UIButtonListMaker.MonsterButtonSort_Max(CardTag.level, false);
    }
    public void Sort_Atk_DownToUp() {
        AllImgWhite();
        atk_dtu_btn.color = push_btn_color;
        pr_ScriptsGetter.pr_UIButtonListMaker.MonsterButtonSort_Max(CardTag.atk, true);
    }
    public void Sort_Atk_UpToDown() {
        AllImgWhite();
        atk_utd_btn.color = push_btn_color;
        pr_ScriptsGetter.pr_UIButtonListMaker.MonsterButtonSort_Max(CardTag.atk, false);
    }
    public void Sort_Def_DownToUp() {
        AllImgWhite();
        def_dtu_btn.color = push_btn_color;
        pr_ScriptsGetter.pr_UIButtonListMaker.MonsterButtonSort_Max(CardTag.def, true);
    }
    public void Sort_Def_UpToDown() {
        AllImgWhite();
        def_utd_btn.color = push_btn_color;
        pr_ScriptsGetter.pr_UIButtonListMaker.MonsterButtonSort_Max(CardTag.def, false);
    }



    //全てのボタンのイメージを白色にする
    void AllImgWhite() {
        normal_btn.color = Color.white;
        attribute_btn.color = Color.white;
        tribe_btn.color = Color.white;
        level_dtu_btn.color = Color.white;
        level_utd_btn.color = Color.white;
        atk_dtu_btn.color= Color.white;
        atk_utd_btn.color=Color.white;
        def_dtu_btn.color = Color.white;
        def_utd_btn.color = Color.white;
    }

}
