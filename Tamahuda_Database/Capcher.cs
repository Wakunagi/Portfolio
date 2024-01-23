using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

namespace program {
    public class Capcher : MonoBehaviour {
        [SerializeField] private so_CardList cardList = null;
        [SerializeField] private GameObject canvas, monsterCard, magicCard;
        [SerializeField]
        private Text
            monsterNameText,
            monsterFuriganaText,
            monsterLevelText,
            monsterAttributeText,
            monsterAtkText,
            monsterDefText,
            monsterTypeText,
            monsterRaceText,
            monsterEffectText;
        [SerializeField] private Image monsterAttributeImage;
        [SerializeField]
        private Text
            MagicNameText,
            MagicFuriganaText,
            MagicAttributeText,
            MagicTypeText,
            MagicEffectText;


        public void OnClick_Capcher() {
            StartCoroutine(CapcherCoutin());
        }
        IEnumerator CapcherCoutin() {
            string directoryName = "ExportImages";
            string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "/" + directoryName;
            canvas.gameObject.SetActive(false);
            magicCard.gameObject.SetActive(false);
            monsterCard.gameObject.SetActive(true);
            Screen.SetResolution(930, 1355, false);
            Debug.Log("CapcherStart");
            if (!Directory.Exists(path)) {
                Debug.Log("create directory");
                //ファイル作成
                Directory.CreateDirectory(path);
            }
            yield return null;
            for (int i = 0; i < cardList.monsterDatas.Count; i++) {
                MonsterData monsterData = cardList.monsterDatas[i];
                Debug.Log("i=" + i);
                monsterNameText.text = monsterData.name;
                monsterFuriganaText.text = monsterData.furigana;
                monsterLevelText.text = monsterData.level.ToString();
                monsterAttributeText.text = monsterData.attribute;
                monsterAtkText.text = monsterData.atk.ToString();
                monsterDefText.text = monsterData.def.ToString();
                monsterTypeText.text = monsterData.role;
                monsterRaceText.text = monsterData.tribe;
                monsterEffectText.text = monsterData.effect;
                monsterAttributeImage.color = cardList.GetAttributeColor(monsterData.attribute);
                // スクリーンショットを保存
                CaptureScreenShot(path + "/Monster[" + i + "]_" + monsterData.name + ".png");
                yield return null;

            }
            monsterCard.gameObject.SetActive(false);
            magicCard.gameObject.SetActive(true);
            yield return null;
            for (int i = 0; i < cardList.magicDatas.Count; i++) {
                MagicData magicrData = cardList.magicDatas[i];
                Debug.Log("i=" + i);
                MagicNameText.text = magicrData.name;
                MagicFuriganaText.text = magicrData.furigana;
                MagicAttributeText.text = magicrData.attribute;
                MagicTypeText.text = magicrData.role;
                MagicEffectText.text = magicrData.effect;
                // スクリーンショットを保存
                CaptureScreenShot(path + "/Magic[" + i + "]_" + magicrData.name + ".png");
                yield return null;

            }

            Screen.SetResolution(540, 960, false);
            monsterCard.gameObject.SetActive(false);
            magicCard.gameObject.SetActive(false);
            canvas.gameObject.SetActive(true);
            yield break;

        }

        // 画面全体のスクリーンショットを保存する
        private void CaptureScreenShot(string filePath) {
            ScreenCapture.CaptureScreenshot(filePath);
        }
    }
}