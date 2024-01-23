using UnityEngine;
using UnityEngine.UI;

namespace program {
    namespace monster {
        public class MonsterButtonData : MonoBehaviour {

            //���g�̃f�[�^
            public MonsterData data { private set; get; } = new MonsterData();

            //�e�X�N���v�g���Q�Ƃ���ꏊ
            ParameterGetter pr_ScriptsGetter;

            //�eUI�̃f�[�^
            [HeaderAttribute("Button Text Datas")]
            [SerializeField] Text name_text;
            [SerializeField] Text level_text, attribute_text;
            [SerializeField] Image attribute_img;


            //�e�����̕\���̃f�[�^
            [HeaderAttribute("Count Image")]
            [SerializeField] GameObject img_parent;
            [SerializeField] Image[] count_imgs = new Image[DeckMakeUI.CARD_COUNT_MAX]; 


            //����������
            public void SetUP(MonsterData _data) {

                pr_ScriptsGetter = ParameterGetter.Instance;

                this.data = _data;

                //�{�^����UI�𒲐�
                name_text.text = _data.name;
                level_text.text = _data.level.ToString();
                attribute_text.text = _data.attribute;

                Color c= pr_ScriptsGetter.pr_CardList.GetAttributeColor(_data.attribute);

                SetCountImage_InDeck();

                attribute_img.color = new Color(c.r,c.g,c.b,0.5f);

            }

            //�f�b�L��UI�Ȃ猩���Ȃ����Ă���
            public void SetUP_DeckMode() {
                img_parent.SetActive(false);
            }

            //�����ꂽ���̏���
            public void OnClick_ViewPanel() {
                //�f�[�^�̐ݒ�&�\��
                pr_ScriptsGetter.pr_CardText_UI.SetMonsterCardImage(data);
                pr_ScriptsGetter.pr_CardText_UI.DisplayCardImage(data.id, CardClass.monster);
            }

            //�J�[�h�����̕\��
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