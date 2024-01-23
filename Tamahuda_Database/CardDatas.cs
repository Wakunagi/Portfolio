using System;

[Serializable]
public class CardDatas {
    public int id;          //ID

    public string name;     //���O
    public string furigana; //�t���K�i
    public string role;     //�����i�J�[�h�^�C�v�j
    public string effect;   //����
}

[Serializable]
public class MonsterData : CardDatas {
    public int level;       //���x��
    public string attribute;//����
    public string tribe;    //�푰
    public int atk;         //�U����
    public int def;         //�����
}

[Serializable]
public class MagicData : CardDatas {
    public string attribute;//���@�̎��
}

[Serializable]
public class DeckList {
    public Deck[] decks;
}

[Serializable]
public class Deck {
    public int user_id; //���[�Uid
    public string pass; //�p�X���[�h

    public int id;      //����ID
    public bool isOpen = false; //���J���邩

    public string name; //�f�b�L��
    public string deck; //�f�b�L���e

    public string created;  //�쐬��
    public string updated;  //�X�V��
}

[Serializable]
public class UserData {
    public int id;
    public string name;
    public string pass;
}