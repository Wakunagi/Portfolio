using program.monster;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace program {
    namespace magic {
        public class MagicButtonData : MonoBehaviour {

            //���g�̃f�[�^
            public MagicData data { private set; get; } = new MagicData();

            //�e�X�N���v�g���Q�Ƃ���ꏊ
            ParameterGetter pr_ScriptsGetter;

            //�eUI�̃f�[�^
            [HeaderAttribute("Button Text Datas")]
            [SerializeField] Text name_text;

            //�e�����̕\���̃f�[�^
            [HeaderAttribute("Count Image")]
            [SerializeField] GameObject img_parent;
            [SerializeField] Image[] count_imgs = new Image[DeckMakeUI.CARD_COUNT_MAX];


            //����������
            public void SetUP(MagicData _data) {

                pr_ScriptsGetter = ParameterGetter.Instance;

                data = _data;

                //�{�^����UI�𒲐�
                name_text.text = _data.name;

                SetCountImage_InDeck();

            }

            //�f�b�L��UI�Ȃ猩���Ȃ����Ă���
            public void SetUP_DeckMode() {
                img_parent.SetActive(false);
            }

            //�����ꂽ���̏���
            public void OnClick_ViewPanel() {
                //�f�[�^�̐ݒ�&�\��
                pr_ScriptsGetter.pr_CardText_UI.SetMagicCardImage(data);
                pr_ScriptsGetter.pr_CardText_UI.DisplayCardImage(data.id,CardClass.magic);
            }

            //�J�[�h�����̕\��
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