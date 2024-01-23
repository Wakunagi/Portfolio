using program.monster;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace program {
    namespace magic {
        public class MagicButtonData : MonoBehaviour {

            //自身のデータ
            public MagicData data { private set; get; } = new MagicData();

            //各スクリプトを参照する場所
            ParameterGetter pr_ScriptsGetter;

            //各UIのデータ
            [HeaderAttribute("Button Text Datas")]
            [SerializeField] Text name_text;

            //各枚数の表示のデータ
            [HeaderAttribute("Count Image")]
            [SerializeField] GameObject img_parent;
            [SerializeField] Image[] count_imgs = new Image[DeckMakeUI.CARD_COUNT_MAX];


            //初期化処理
            public void SetUP(MagicData _data) {

                pr_ScriptsGetter = ParameterGetter.Instance;

                data = _data;

                //ボタンのUIを調整
                name_text.text = _data.name;

                SetCountImage_InDeck();

            }

            //デッキのUIなら見えなくしておく
            public void SetUP_DeckMode() {
                img_parent.SetActive(false);
            }

            //押された時の処理
            public void OnClick_ViewPanel() {
                //データの設定&表示
                pr_ScriptsGetter.pr_CardText_UI.SetMagicCardImage(data);
                pr_ScriptsGetter.pr_CardText_UI.DisplayCardImage(data.id,CardClass.magic);
            }

            //カード枚数の表示
            public void SetCountImage_InDeck() {
                int count = pr_ScriptsGetter.pr_DeckMakeUI.GetCardCount(data.id);
                for (int i = 0; i < count_imgs.Length; i++) {
                    if (i < count) count_imgs[i].color = Color.yellow;
                    else count_imgs[i].color = Color.gray;
                }
            }
        }
    }
}