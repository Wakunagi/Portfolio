using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace program {
    public class GSSReader : MonoBehaviour {
        private string SHEET_ID = "1-k7N8CmIPGLa7cANLAr_nYx_E7nijkkEk7I_YCZximY";
        [SerializeField] private string SHEET_NAME = "";

        //�e�X�N���v�g���Q�Ƃ���ꏊ
       protected ParameterGetter pr_ScriptsGetter;

        //https://docs.google.com/spreadsheets/d/1-k7N8CmIPGLa7cANLAr_nYx_E7nijkkEk7I_YCZximY

        void Start() {
            //ScriptsGetter�̏�����
            pr_ScriptsGetter = ParameterGetter.Instance;

            pr_ScriptsGetter.pr_HideLoadingUI.SetCount();

            //��ʃT�C�Y�̕ύX
            Screen.SetResolution(540, 960, false);

            //�X�v���b�g�V�[�g�̓ǂݍ���
            StartCoroutine(Method(SHEET_NAME));
        }

        IEnumerator Method(string _SHEET_NAME) {

            //�X�v���b�g�V�[�g��ǂݍ���
            UnityWebRequest request = UnityWebRequest.Get("https://docs.google.com/spreadsheets/d/" + SHEET_ID + "/gviz/tq?tqx=out:csv&sheet=" + _SHEET_NAME);
            
            //�ǂݍ��߂�܂ő҂�
            yield return request.SendWebRequest();

            //���s
            if (request.isHttpError || request.isNetworkError) {
                Debug.Log(request.error);
            }

            //����
            else {
                //�ǂݍ��񂾃f�[�^�����H
                CardSetting(request.downloadHandler.text + "\n");
                SetUp();

                pr_ScriptsGetter.pr_HideLoadingUI.SetEnd();
            }
        }

        protected virtual void SetUp() {

        }

        protected virtual void CardSetting(string data) {
            ResetData();

            string text = "";   //1�s���̃e�L�X�g�f�[�^

            bool isReadStart = false;   //�P��̓ǂݍ��ݒ����ǂ���
            bool isFirstRead = true;    //�ŏ��̍s���ǂ���

            for (int i = 0; i < data.Length; i++) {
                char c = data[i];

                //���s�ł��P��O�i�s�̏I���j�̏ꍇ
                if (c == '\n' && !isReadStart) {

                    //�P�s�ڂȂ牽�����Ȃ�
                    if (isFirstRead) { isFirstRead = false; }

                    //�Q�s�ڈȍ~�̓f�[�^��}��
                    else {
                        SetData(text);
                    }

                    //�e�L�X�g�����Z�b�g
                    text = ""; continue;
                }

                //������"�̏ꍇ
                if (c == '\"') {
                    //�ǂݍ��ݏ�ԂȂ�I��
                    //�ǂ�ł��Ȃ��Ȃ�ǂݎn��
                    isReadStart = !isReadStart;

                    //"�̑���Ƀ^�u�ɕύX
                    if (!isReadStart) text += '\t';
                    continue;
                }

                //�ǂݍ��ݏ�Ԃ̏ꍇ
                if (isReadStart) text += c;


            }

        }

        protected virtual void ResetData() {

        }

        protected virtual void SetData(string text) {

        }

        protected string StringReader(string data, int num) {
            string myData = "";
            for (int i = 0; i < data.Length; i++) {
                char c = data[i];

                //num�ڂ̃^�u������܂ł̓��Z�b�g����
                if (c == '\t') {
                    num--;
                    if (num == 0) break;

                    myData = "";
                    continue;
                }

                myData += c;
            }
            return myData;
        }
    }
}