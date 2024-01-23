using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace program {
    [CreateAssetMenu(menuName = "ScriptableObjects/MonsterList ")]
    public class so_CardList : ScriptableObject {

        //�J�[�h�̃f�[�^
        [field: SerializeField] public List<MonsterData> monsterDatas { private set; get; } = new List<MonsterData>();
        [field: SerializeField] public List<MagicData> magicDatas { private set; get; } = new List<MagicData>();

        //�����X�^�[�̃X�e�[�^�X�̍ő�l
        [field: SerializeField] public int maxLevel { private set; get; } = 0;  /* ���x�� */
        [field: SerializeField] public int maxAtk { private set; get; } = 0;    /* �U���� */
        [field: SerializeField] public int maxDef { private set; get; } = 0;    /* ����� */

        //�J�[�h�̃^�C�v
        [field: SerializeField] public List<string> CardRoleDatas { private set; get; } = new List<string>();           /* ���� */
        [field: SerializeField] public List<string> MonsterAttributeDatas { private set; get; } = new List<string>();   /* �����X�^�[�̑��� */
        [field: SerializeField] public List<string> MonsterTribeDatas { private set; get; } = new List<string>();       /* �����X�^�[�̎푰 */
        [field: SerializeField] public List<string> MagicAttributeDatas { private set; get; } = new List<string>();     /* ���@�̑��� */

        //�����X�^�[�̑����̐F���
        [field: SerializeField] public List<Color> MonsterAttribute_ColorDatas { private set; get; } = new List<Color>();


        //�J�[�h�f�[�^�̃��Z�b�g�֐�
        public void ResetMonsterList() { monsterDatas = new List<MonsterData>(); }
        public void ResetMagicList() { magicDatas = new List<MagicData>(); }

        //�J�[�h�^�C�v�̃��Z�b�g�֐�
        public void ResetAllCardTypeList() {
            CardRoleDatas = new List<string>();
            MonsterAttributeDatas = new List<string>();
            MonsterTribeDatas = new List<string>();
            MagicAttributeDatas = new List<string>();
        }


        //�J�[�h�f�[�^�̒ǉ��֐�
        public void AddCardData(MonsterData monsterData) { monsterDatas.Add(monsterData); }
        public void AddCardData(MagicData magicData) { magicDatas.Add(magicData); }

        //�����X�^�[�̃X�e�[�^�X�̍ő�l�̒ǉ��֐�
        public void SetMaxLevel(int max) { maxLevel = max; }
        public void SetMaxAtk(int max) { maxAtk = max; }
        public void SetMaxDef(int max) { maxDef = max; }

        //�J�[�h�^�C�v�̒ǉ��֐�
        public void AddCardRole(string type) { CardRoleDatas.Add(type); }
        public void AddMonsterAttribute(string type) { MonsterAttributeDatas.Add(type); }
        public void AddMonsterTribe(string type) { MonsterTribeDatas.Add(type); }
        public void AddMagicAttribute(string type) { MagicAttributeDatas.Add(type); }


        //�����X�^�[�̑�������F���擾
        public Color GetAttributeColor(string atb) {
            Color color = Color.white;
            for (int i = 0; i < (int)AttributeData.end; i++) {
                if (atb == MonsterAttributeDatas[i]) {
                    color = MonsterAttribute_ColorDatas[i]; break;
                }
            }
            return color;
        }

        //ID���疼�O���擾
        public string GetCardName(int id) {
            foreach (MonsterData data in monsterDatas) {
                if (id == data.id) return data.name;
            }

            foreach (MagicData data in magicDatas) {
                if (id == data.id) return data.name;
            }

            return null;
        }

        //ID���烂���X�^�[���擾
        public MonsterData MonsterInID(int id) {
            foreach (MonsterData data in monsterDatas) {
                if (id == data.id) return data;
            }
            return null;
        }

        //ID���疂�@���擾
        public MagicData MagicInID(int id) {
            foreach (MagicData data in magicDatas) {
                if (id == data.id) return data;
            }
            return null;
        }


        //�^�O���烂���X�^�[�̃f�[�^��Ԃ�
        public string MonsterDataGetter(MonsterData data, CardTag tag) {
            switch (tag) {
                case CardTag.attribute: return data.attribute;
                case CardTag.tribe: return data.tribe;
                case CardTag.role: return data.role;
                case CardTag.level: return data.level.ToString();
                case CardTag.atk: return data.atk.ToString();
                case CardTag.def: return data.def.ToString();
                case CardTag.name: return data.name;
                case CardTag.effect: return data.effect;
                default: return null;
            }
        }

        //�^�O���疂�@�̃f�[�^��Ԃ�
        public string MagicDataGetter(MagicData data, CardTag tag) {
            switch (tag) {
                case CardTag.attribute: return data.attribute;
                case CardTag.role: return data.role;
                case CardTag.name: return data.name;
                case CardTag.effect: return data.effect;
                default: return null;
            }
        }

        //�^�O����String�̃f�[�^��n��
        public List<string> MonsterCardTagDataGetter(CardTag tag) {
            switch (tag) {
                case CardTag.attribute: return MonsterAttributeDatas;
                case CardTag.role: return CardRoleDatas;
                case CardTag.tribe: return MonsterTribeDatas;
                default: return null;
            }
        }


        //�^�O����Max��n��
        public int MonsterStatusDataGetter(CardTag tag) {
            switch (tag) {
                case CardTag.level: return maxLevel;
                case CardTag.atk: return maxAtk;
                case CardTag.def: return maxDef;
                default: return -1;
            }
        }
    }

    public enum MonsterCardData {
        Name, Furigana, Level, Attribute, Atk, Def, Type, Race, Effect, Count, end
    }

    public enum MagicCardData {
        Name, Furigana, Attribute, Type, Effect, Count, end
    }
    public enum AttributeData {
        Fire, Water, Wind, Ground, Light, Dark, end
    }

    public enum CardTag {
        attribute, tribe, role, level, atk, def, name, effect, end
    }

    public enum CardClass {
        monster, magic, end,
    }

}