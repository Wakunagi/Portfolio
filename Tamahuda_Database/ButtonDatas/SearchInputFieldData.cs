using program;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchInputFieldData : MonoBehaviour {

    //�e�X�N���v�g���Q�Ƃ���ꏊ
    ParameterGetter pr_ScriptsGetter;

    void Start() {
        //ScriptsGetter�̏�����
        pr_ScriptsGetter = ParameterGetter.Instance;

        //���O�������p�ɐݒ肵�Ă���
        search_name = true;
        search_effect = false;
        name_btn_img.color = push_btn_color;
        effect_btn_img.color = Color.white;
    }

    //�I�u�W�F�N�g�ƌ��т���
    [SerializeField] InputField inputField;

    //�{�^����Image
    [SerializeField] Image name_btn_img, effect_btn_img;

    //�����X�^�[�Ɩ��@�̂ǂ�����������邩
    [SerializeField] CardClass myClass;

    //�{�^�����������Ƃ��̐F
    [SerializeField] Color push_btn_color;

    bool search_name, search_effect;

    //�Е��̂�On�ɂȂ�
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

    //���͂ɕω�������������s
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