using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CardData : MonoBehaviour {

    //CARD TYPE********************************************************************//
    public enum CARD_TYPE
    {
        ITEM,
        SKILLS,
        MAX_CARD_TYPE
    };
    public CARD_TYPE typeCard;
    CARD_TYPE cardType;

    public string CardName;
    public Text CardNameText;

    string Name;
    //CARD TYPE********************************************************************//

    //ITEM TYPE********************************************************************//
    public enum ITEM_TYPE
    {
        NOT_ITEM,
        ARMOUR,
        WEAPON,
        MAX_ITEM_TYPE
    };
    public ITEM_TYPE typeItem;
    ITEM_TYPE itemType;
    //ITEM - WEAPON****************************************************************//
    public int minBasedmgstats, maxBasedmgstats;
    public Text weaponBaseDmgText;

    int basedmgstats;
    //ITEM - WEAPON****************************************************************//

    //ITEM - ARMOUR****************************************************************//
    public int minBasedefstats, maxBasedefstats;
    public Text armourBaseDefText;

    int basedefstats;
    //ITEM - ARMOUR****************************************************************//

    public int minSpecialStats, maxSpecialStats;
    public Text specialStatsText;

    int specialstats;
    //ITEM TYPE********************************************************************//

    //SKILL TYPE*******************************************************************//
    public enum SKILL_TYPE
    {
        NONE,
        FIREBALL,
        FREEZE,
        LIGHTNINGBOLT,
        HEAL,
        TELEPORT,
        SHIELD,
        MAX_SKILL_TYPE
    };
    public SKILL_TYPE typeSkill;
    SKILL_TYPE skillType;
    //SKILLS****************************************************************//
    public int mineffectValue, maxeffectValue;
    public int mineffectTimer, maxeffectTimer;
    public int mineffectTimerDesc, maxeffectTimerDesc;
    public int effectCoolDown;

    public Text EffectValueText;
    public Text EffectCoolDownText;
    public Text EffectTimerText;
    public Text EffectTimerDescText;

    int effectVAL;
    int effectTimer;
    int effectCD;
    //SKILLS****************************************************************//
    //SKILL TYPE*******************************************************************//

	// Use this for initialization
	void Start () {
        SetCardType(typeCard);
        SetCardName(CardName);
        CardNameText.text = Name;
        if(GetCardType() == CARD_TYPE.ITEM)
        {
            SetItemType(typeItem);
            if(GetItemType() == ITEM_TYPE.WEAPON)
            {
                SetWeaponBaseDMG(minBasedmgstats, maxBasedmgstats);
                weaponBaseDmgText.text = basedmgstats.ToString();
            }
            else if(GetItemType() == ITEM_TYPE.ARMOUR)
            {
                SetArmourBaseDEF(minBasedefstats, maxBasedefstats);
                armourBaseDefText.text = basedefstats.ToString();
            }
            SetSpecialStats(minSpecialStats, maxSpecialStats);
            specialStatsText.text = specialstats.ToString();
        }
        else if(GetCardType() == CARD_TYPE.SKILLS)
        {
            SetSkillType(typeSkill);
            SetEffectValue(mineffectValue, maxeffectValue);
            SetEffectTimer(mineffectTimer, maxeffectTimer);
            SetCoolDown(effectCoolDown);

            EffectValueText.text = effectVAL.ToString();
            EffectTimerText.text = effectTimer.ToString();
            EffectTimerDescText.text = effectTimer.ToString();
            EffectCoolDownText.text = effectCD.ToString();
        }
	}

    void SetCardType(CARD_TYPE card)
    {
        cardType = card;
    }

    public CARD_TYPE GetCardType()
    {
        return cardType;
    }

    void SetCardName(string nameofcard)
    {
        Name = nameofcard;
    }

    public string GetCardName()
    {
        return Name;
    }

    //ITEM TYPE********************************************************************//
    //Set Item Type
    void SetItemType(ITEM_TYPE item)
    {
        itemType = item;
    }

    public ITEM_TYPE GetItemType()
    {
        return itemType;
    }

    //Set Stats Of Item
    void SetWeaponBaseDMG(int minDmg, int maxDmg)
    {
        basedmgstats = (Random.Range(minDmg, maxDmg));
    }
    void SetArmourBaseDEF(int minDef, int maxDef)
    {
        basedefstats = (Random.Range(minDef, maxDef));
    }
    void SetSpecialStats(int minSpecial, int maxSpecial)
    {
        specialstats = (Random.Range(minSpecial, maxSpecial));
    }
    //ITEM TYPE********************************************************************//

    //SKILL    ********************************************************************//
    //Set Skill Type
    void SetSkillType(SKILL_TYPE skill)
    {
        skillType = skill;
    }

    public SKILL_TYPE GetSkillType()
    {
        return skillType;
    }

    void SetEffectValue(int minVal, int maxVal)
    {
        effectVAL = (Random.Range(minVal, maxVal));
    }
    void SetEffectTimer(int minTimer, int maxTimer)
    {
        effectTimer = (Random.Range(minTimer, maxTimer));
    }
    void SetCoolDown(int cooldown)
    {
        effectCD = cooldown;
    }
    //SKILL    ********************************************************************//

	// Update is called once per frame
	void Update () {
	    //No Update
	}
}
