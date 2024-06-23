using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Trait : UI_Popup
{
    enum Text { Trait1InfoTxt, Trait2InfoTxt, Trait3InfoTxt, Trait1LevelTxt, Trait2LevelTxt, Trait3LevelTxt }
    enum GameObjects { Trait1Info , Trait2Info , Trait3Info, Trait1Screen, Trait2Screen, Trait3Screen }
    enum TraitBtn { Trait1, Trait2, Trait3 }

    /*//////////////////////////////////////////////////////////////////
     ///////////////////////Private Field///////////////////////////////
     ///////////////////////////////////////////////////////////////////*/
    private GameObject _player;
    private PlayerStat _playerStat;


    public override void Init()
    {
        base.Init();

        Bind<TextMeshProUGUI>(typeof(Text));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Button>(typeof(TraitBtn));

        _player = GameObject.FindWithTag("Player");
        _playerStat = _player.GetComponent<PlayerStat>();

        InitBtn();
        InitObject();
    }

    private void Start()
    {

    }

    private void InitBtn()
    {
        Get<Button>((int)TraitBtn.Trait1).onClick.AddListener(() => OnClickedTraitBtn((int)GameObjects.Trait1Screen,(int)GameObjects.Trait1Info));
        Get<Button>((int)TraitBtn.Trait2).onClick.AddListener(() => OnClickedTraitBtn((int)GameObjects.Trait2Screen, (int)GameObjects.Trait2Info));
        Get<Button>((int)TraitBtn.Trait3).onClick.AddListener(() => OnClickedTraitBtn((int)GameObjects.Trait3Screen, (int)GameObjects.Trait3Info));
    }

    private void InitObject()
    {
        Get<GameObject>((int)GameObjects.Trait1Info).SetActive(false);
        Get<GameObject>((int)GameObjects.Trait2Info).SetActive(false);
        Get<GameObject>((int)GameObjects.Trait3Info).SetActive(false);

    }

    private void OnClickedTraitBtn(int offIndex, int onIndex)
    {
        Debug.Log("Onclicked");
        int level = int.Parse(Get<TextMeshProUGUI>(offIndex).text);
        if (_playerStat.Level < level)
            return;

        Get<GameObject>(onIndex).SetActive(true);
        Get<GameObject>(offIndex).SetActive(false);

        if (Managers.Character._traitManager == null)
        {
            Debug.LogError("Trait Manager is null!");
            return;
        }

        Debug.Log(Managers.Character.GetNowCharacter().ClassName); 
        Managers.Character._traitManager.AddTrait(Managers.Character.GetNowCharacter().Traits[onIndex], _player);
    }
}
