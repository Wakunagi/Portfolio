using UnityEngine;
using UnityEngine.UI;

namespace program {
    public class CardTypeButtonData : MonoBehaviour {

        //各スクリプトを参照する場所
        ParameterGetter pr_ScriptsGetter;

        //各UIのデータ
        [SerializeField] Image btn_img;
        [SerializeField] Text type_text;

        //ボタンを押したときの色
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