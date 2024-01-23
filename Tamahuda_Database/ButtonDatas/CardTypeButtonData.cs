using UnityEngine;
using UnityEngine.UI;

namespace program {
    public class CardTypeButtonData : MonoBehaviour {

        //�e�X�N���v�g���Q�Ƃ���ꏊ
        ParameterGetter pr_ScriptsGetter;

        //�eUI�̃f�[�^
        [SerializeField] Image btn_img;
        [SerializeField] Text type_text;

        //�{�^�����������Ƃ��̐F
        [SerializeField] Color push_btn_color;

        bool isSearch = false;

        string text;
        CardTag tag;
        CardClass cardClass;

        public void SetUP(string _text, CardTag _tag, CardClass _cardClass) {

            pr_ScriptsGetter = ParameterGetter.Instance;

            text = _text;
            tag = _tag;
            cardClass = _cardClass;

            type_text.text = _text;
        }


        public void OnClick_Search() {

            isSearch = !isSearch;

            if (isSearch) {
                btn_img.color = push_btn_color;
            }
            else {
                btn_img.color = Color.white;
            }

            pr_ScriptsGetter.pr_UIButtonListMaker.CardSearchSetter(text, isSearch, tag, cardClass);
        }

    }
}