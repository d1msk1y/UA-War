using UnityEngine;
using UnityEngine.UI;

public enum HostageRareness { Default, Bronze, Silver, Gold }

public class Hostage : MonoBehaviour
{
    [Header("Bio")]
    [SerializeField] private string _hostageName;
    [SerializeField] private string _rewardSpeech;
    [SerializeField] private HostageRareness hostageRareness;

    [Header("Parameters")]
    private int _chanceToCatch;
    public int reward;

    [Header("Rareness")]
    [SerializeField] private int[] rarenessChance = { 40, 30, 20, 10 };
    [SerializeField] private Color[] _rarenessColor;

    private Hostage TryToCatch()
    {
        Hostage hostage = this;
        if (DetermineChance() == true)
        {
            hostage.hostageRareness = DetermineHostageRareness();
            return hostage;
        }
        else return null;
    }

    private int CalculatedTotalChance()
    {
        int calculatedTotalChance = 0;
        foreach (var item in rarenessChance)
        {
            calculatedTotalChance += item;
        }
        return calculatedTotalChance;
    }

    public bool DetermineChance()//Determine chance to catch the hostage
    {
        int chance = Random.Range(0, _chanceToCatch);
        if (chance == _chanceToCatch)
            return true;
        else return false;
    }

    public void Sell()
    {
        GameManager.Instance.scoreSystem.AddCoins(reward);
    }

    private HostageRareness DetermineHostageRareness()
    {
        int chance = Random.Range(0, CalculatedTotalChance());
        HostageRareness hostageRareness = HostageRareness.Default;

        for (int i = 0; i < rarenessChance.Length; i++)
        {
            if (chance <= rarenessChance[i])
            {
                SetColor(i);
                hostageRareness = (HostageRareness)i;
                return hostageRareness;
            }
            else chance -= rarenessChance[i];
        }
        return hostageRareness;
    }

    private void SetColor(int i)
    {
        GetComponent<Image>().color = _rarenessColor[i];
    }
}