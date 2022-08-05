using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeHandler : MonoBehaviour
{
    public List<ChoiceSet> choices = new List<ChoiceSet>();

    public PlayerStat playerStat;

    private void Awake()
    {
        GameManager.Instance.upgradeHandler = this;
    }

    private void Start()
    {
        playerStat = GameManager.Instance.playerTrm.GetComponent<PlayerStat>();

        for (int i = 0; i < choices.Count; i++)
        {
            choices[i].merit.uh = this;
            choices[i].demerit.uh = this;
        }
    }

    public List<ChoiceSet> GetRandomChoices(int count)
    {
        List<ChoiceSet> result = new List<ChoiceSet>();

        if (count > 0)
        {
            List<ChoiceSet> list = new List<ChoiceSet>();

            for (int i = 0; i < choices.Count; i++)
            {
                if (choices[i].merit.CanChoice() && choices[i].demerit.CanChoice())
                {
                    list.Add(choices[i]);
                }
            }

            for (int i = 0; i < count; i++)
            {
                int randIdx = Random.Range(0, list.Count);

                ChoiceSet choice = list[randIdx];
                result.Add(choice);

                list.RemoveAt(randIdx);
            }
        }

        return result;
    }
}
