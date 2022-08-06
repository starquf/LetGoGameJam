using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeHandler : MonoBehaviour
{
    //public List<ChoiceSet> choices = new List<ChoiceSet>();

    public Transform meritParent;
    public Transform demeritParent;

    private List<ChoiceInfo> merits = new List<ChoiceInfo>();
    private List<ChoiceInfo> demerits = new List<ChoiceInfo>();

    public PlayerStat playerStat;

    private void Awake()
    {
        GameManager.Instance.upgradeHandler = this;

        meritParent.GetComponentsInChildren(merits);
        demeritParent.GetComponentsInChildren(demerits);

        for (int i = 0; i < merits.Count; i++)
        {
            merits[i].uh = this;
        }

        for (int i = 0; i < demerits.Count; i++)
        {
            demerits[i].uh = this;
        }
    }

    private void Start()
    {
        playerStat = GameManager.Instance.playerTrm.GetComponent<PlayerStat>();
    }

    public List<ChoiceSet> GetRandomChoices(int count)
    {
        List<ChoiceSet> result = new List<ChoiceSet>();

        if (count > 0)
        {
            List<ChoiceInfo> meritlist = new List<ChoiceInfo>();

            for (int i = 0; i < merits.Count; i++)
            {
                if (merits[i].CanChoice())
                {
                    meritlist.Add(merits[i]);
                }
            }

            List<ChoiceInfo> demeritlist = new List<ChoiceInfo>();

            for (int i = 0; i < demerits.Count; i++)
            {
                if (demerits[i].CanChoice())
                {
                    demeritlist.Add(demerits[i]);
                }
            }

            for (int i = 0; i < count; i++)
            {
                int randIdx_merit = Random.Range(0, meritlist.Count);
                int randIdx_demerit = Random.Range(0, demeritlist.Count);

                ChoiceSet choice = new ChoiceSet(meritlist[randIdx_merit], demeritlist[randIdx_demerit]);
                result.Add(choice);

                meritlist.RemoveAt(randIdx_merit);
                demeritlist.RemoveAt(randIdx_demerit);
            }
        }

        return result;
    }
}
