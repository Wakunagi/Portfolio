using UnityEngine;
using UnityEngine.UI;

namespace program {
    namespace monster {
        public class MonsterButtonData : MonoBehaviour {

            //自身のデータ
            public MonsterData data { private set; get; } = new MonsterData();

            //各スクリプトを参照する場所
            ParameterGetter pr_ScriptsGetter;

            //各UIのデータ
            [HeaderAttribute("Button Text Datas")]
            [SerializeField] Text name_text;
            [SerializeField] Text level_text, attribute_text;
            [SerializeField] Image attribute_img;


            //各枚数の表示のデータ
            [HeaderAttribute("Count Image")]
            [SerializeField] GameObject img_parent;
            [SerializeField] Image[] count_imgs = new Image[DeckMakeUI.CARD_COUNT_MAX]; 


            //初期化処理
            public void SetUP(MonsterData _data) {

                pr_ScriptsGetter = ParameterGetter.Instance;

                this.data = _data;

                //ボタンのUIを調整
                name_text.text = _data.name;
                level_text.text = _data.level.ToString();
                attribute_text.text = _data.attribute;

                Color c= pr_ScriptsGetter.pr_CardList.GetAttributeColor(_data.attribute);

                SetCountImage_InDeck();

                attribute_img.color = new Color(c.r,c.g,c.b,0.5f);

            }

            //デッキのUIなら見えなくしておく
            public void SetUP_DeckMode() {
                img_parent.SetActive(false);
            }

            //押された時の処理
            public void OnClick_ViewPanel() {
                //データの設定&表示
                pr_ScriptsGetter.pr_CardText_UI.SetMonsterCardImage(data);
                pr_ScriptsGetter.pr_CardText_UI.DisplayCardImage(data.id, CardClass.monster);
            }

            //カード枚数の表示
            public void SetCountImage_InDeck() {
                int count = pr_ScriptsGetter.pr_DeckMakeUI.GetCardCount(data.id);
                for(int i = 0; i < count_imgs.Length; i++) {
                    if (i < count) count_imgs[i].color = Color.yellow;
                    else count_imgs[i].color = Color.gray;
                }
            }
        }
    }
}