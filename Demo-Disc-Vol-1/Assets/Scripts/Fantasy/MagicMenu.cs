using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MagicMenu : MonoBehaviour
{
    [SerializeField] GameObject actionCover;
    [SerializeField] AnimationCurve lvlCurve;

    [Header("Magic Levels")]
    [SerializeField] int fireLevel = 1;
    [SerializeField] int iceLevel = 1;
    [SerializeField] int lightningLevel = 1;

    [Header("Magic Calc")]
    [SerializeField] int baseDamage;
    [SerializeField] float damageMultiplier;
    [SerializeField] int baseCost;
    [SerializeField] float costMultiplier;

    [Header("Magic Text")]
    [SerializeField] TextMeshProUGUI fireText;
    [SerializeField] TextMeshProUGUI iceText;
    [SerializeField] TextMeshProUGUI lightningText;

    int fireUses;
    int iceUses;
    int lightningUses;

    string magicType;

    FantasyEnemy enemy;
    StatManager statManager;

    private void Start()
    {
        statManager = FindObjectOfType<StatManager>();
    }

    private void OnEnable()
    {
        actionCover.SetActive(true);
        enemy = FindObjectOfType<FantasyEnemy>();
        showMagicLevels();
    }

    private void OnDisable()
    {
        actionCover.SetActive(false);
    }

    public void CastFire()
    {
        magicType = "fire";

        if (statManager.CastMagic(magicCost(magicType)))
        {
            enemy.TakeDamage(magicDamage(magicType), magicType);
            fireUses++;
            gameObject.SetActive(false);
        }
    }

    public void CastIce()
    {
        magicType = "ice";
        if (statManager.CastMagic(magicCost(magicType)))
        {
            enemy.TakeDamage(magicDamage(magicType), magicType);
            iceUses++;
            gameObject.SetActive(false);
        }

    }

    public void CastLightning()
    {
        magicType = "lightning";

        if (statManager.CastMagic(magicCost(magicType)))
        {
            enemy.TakeDamage(magicDamage(magicType), magicType);
            lightningUses++;
            gameObject.SetActive(false);
        }
        checkMagicLvl(magicType);
    }

    int magicDamage (string type)
    {
        int damage = 0;

        switch (type)
        {
            case "fire":
                damage = (int)(baseDamage * (fireLevel / damageMultiplier));
                    break;

            case "ice":
                damage = (int)(baseDamage * (iceLevel / damageMultiplier));
                    break;

            case "lightning":
                damage = (int)(baseDamage * (lightningLevel / damageMultiplier));
                    break;
        }

        return damage;
    }

    int magicCost(string type)
    {
        int cost = 0;

        switch (type)
        {

            case "fire":
            {
                cost = (int)(baseCost * (fireLevel / costMultiplier));
                    break;
            }
            case "ice":
            {
                cost = (int)(baseCost * (iceLevel / costMultiplier));
                    break;
            }
            case "lightning":
            {
                cost = (int)(baseCost * (lightningLevel / costMultiplier));
                    break;
            }
        }

        return cost;
    }

    void checkMagicLvl(string type)
    {
        int nextLevelRequirement;

        switch (type)
        {
            case "fire":
                nextLevelRequirement = (int)lvlCurve.Evaluate(fireLevel + 1);
                if(fireUses >= nextLevelRequirement)
                {
                    LvlUpMagic(type); break;
                }
                break;
            case "ice":
                nextLevelRequirement = (int)lvlCurve.Evaluate(iceLevel + 1);
                if (iceUses >= nextLevelRequirement)
                {
                    LvlUpMagic(type); break;
                }
                break;
            case "lightning":
                nextLevelRequirement = (int)lvlCurve.Evaluate(lightningLevel + 1);
                if (lightningUses >= nextLevelRequirement)
                {
                    LvlUpMagic(type); break;
                }
                break;

        }
    }

    void LvlUpMagic(string type)
    {
        switch (type)
        {
            case "fire":
                fireLevel++;
                break;

            case "ice":
                iceLevel++;
                break;

            case "lightning":
                lightningLevel++;
                break;
        }
    }

    void showMagicLevels()
    {
        fireText.text = $"Fire Lvl. {fireLevel}";
        iceText.text = $"Ice Lvl. {iceLevel}";
        lightningText.text = $"Lightning Lvl. {lightningLevel}";
    }
    public void Back()
    {
        gameObject.SetActive(false);
    }
}
