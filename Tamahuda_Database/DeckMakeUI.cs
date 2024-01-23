using program.magic;
using program.monster;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace program {
    public class DeckMakeUI : MonoBehaviour {

        //�e�X�N���v�g���Q�Ƃ���ꏊ
        ParameterGetter pr_ScriptsGetter;

        void Start() {
            //ScriptsGetter�̏�����
            pr_ScriptsGetter = ParameterGetter.Instance;
        }

        //�f�b�L���̓����J�[�h�̍ő吔
        public const int CARD_COUNT_MAX=3;
        
        //�f�b�L�̍ő吔
        public const int DECK_MAX=30;

        //�f�b�L���X�g
        List<int> deck = new List<int>();

        //�J�[�h�ǉ��ƍ폜
        public void AddCard(int id) { deck.Add(id); }
        public void RemoveCard(int id) { deck.Remove(id); }

        //�f�b�L���X�g�{�^���̐e
        [SerializeField] GameObject deck_btn_parent;

        //�f�b�L���e�\���p��UI
        [SerializeField] GameObject deckList_panel;
        [SerializeField] Text deckList_text;
        [SerializeField] Text analyze_text;

        //�f�b�L���X�g�{�^���̃��X�g
        List<GameObject> deck_btn_list = new List<GameObject>();

        //�f�b�L���X�g����{�^���𐶐�
        public void DisplayDeckList() {

            deck.Sort();

            //�f�b�L���X�g�{�^���̏�����
            for(int i = 0; i < deck_btn_list.Count; i++) { Destroy(deck_btn_list[i]); }
            deck_btn_list.Clear();

            //�f�b�L���e�̐F�̕ύX
            if (deck.Count > DECK_MAX) analyze_text.color = Color.red;
            else if (deck.Count < DECK_MAX) analyze_text.color = Color.blue;
            else analyze_text.color = Color.black;

            int mns_count = 0;
            int mgc_count = 0;

            foreach (int id in deck) {

                //ID�������X�^�[�Ȃ�
                MonsterData monster = pr_ScriptsGetter.pr_CardList.MonsterInID(id);
                if (monster != null) {

                    //�{�^���̐����ƃv���O�����̎擾
                    GameObject btn = Instantiate(pr_ScriptsGetter.pr_UIButtonListMaker.mns_btn_prefab);
                    MonsterButtonData mns_btn_data = null;
                    mns_btn_data = btn.GetComponent<MonsterButtonData>();
                    if (mns_btn_data == null) { Debug.LogError("Monster Button does not have MonsterButtonData!"); return; }

                    //���X�g�ɒǉ�
                    deck_btn_list.Add(btn);

                    //�e��ݒ�
                    btn.transform.SetParent(deck_btn_parent.transform);

                    //MonsterButtonData�̃Z�b�g�A�b�v
                    mns_btn_data.SetUP(monster);
                    mns_btn_data.SetUP_DeckMode();

                    mns_count++;

                    continue;
                }
                
                //ID�����@�Ȃ�
                MagicData magic = pr_ScriptsGetter.pr_CardList.MagicInID(id);
                if (magic != null) {

                    //�{�^���̐����ƃv���O�����̎擾
                    GameObject btn = Instantiate(pr_ScriptsGetter.pr_UIButtonListMaker.mgc_btn_prefab);
                    MagicButtonData mgc_btn_data = null;
                    mgc_btn_data = btn.GetComponent<MagicButtonData>();
                    if (mgc_btn_data == null) { Debug.LogError("Magic Button does not have MagicButtonData!"); return; }

                    //���X�g�ɒǉ�
                    deck_btn_list.Add(btn);

                    //�e��ݒ�
                    btn.transform.SetParent(deck_btn_parent.transform);

                    //MagicButtonData�̃Z�b�g�A�b�v
                    mgc_btn_data.SetUP(magic);
                    mgc_btn_data.SetUP_DeckMode();

                    mgc_count++;

                    continue;
                }
            }

            analyze_text.text = "�f�b�L����: " + deck.Count + " ��\r\n�����X�^�[: " + mns_count + " ��\r\n���@: " + mgc_count + " ��";


            //�J�[�h�����̕\��
            foreach (MonsterButtonData data in pr_ScriptsGetter.pr_UIButtonListMaker.mns_btn_list) {
                data.SetCountImage_InDeck();
            }
            foreach (MagicButtonData data in pr_ScriptsGetter.pr_UIButtonListMaker.mgc_btn_list) {
                data.SetCountImage_InDeck();
            }
        }

        //�f�b�L���e�̕\��
        public void OpenDeckListText() {

            deckList_text.text = string.Empty;
            int count = 0;

            //�f�b�L���e��\��
            foreach (int id in deck) {

                deckList_text.text += "[" + (count+1) + "]";
                if (count < 9) deckList_text.text += " ";
                deckList_text.text += ": " + pr_ScriptsGetter.pr_CardList.GetCardName(id) + "\n";

                count++;
            }

            //�f�b�L���e�̐F�̕ύX
            if (deck.Count > DECK_MAX) deckList_text.color = Color.red;
            else if (deck.Count < DECK_MAX) deckList_text.color = Color.blue;
            else deckList_text.color = Color.black;

            deckList_panel.SetActive(true);
        }

        //�f�b�L���̃J�[�h�̐�
        public int GetCardCount(int id) {
            int count = 0;
            foreach(int _id in deck) {
                if (_id == id) { count++;}
            }
            return count;
        }

    }
}