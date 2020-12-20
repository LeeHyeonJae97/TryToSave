using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat
{
    [System.Serializable]
    public struct Modifier
    {
        public enum Type { FLAT = 0, PERCENT = 100 };

        // 스킬의 Buf에 있는 Modifier는 해당 버프가 특정 능력치를 어떻게 조절하냐에 따라 type이 정해지고
        // 아이템의 ItemStat에 있는 Modifier는 증가시키는 능력치의 타입에 따라 달라진다.
        public Type type;
        public int value;

        public Modifier(Type type, int value)
        {
            this.type = type;
            this.value = value;
        }
    }

    public string key;           // 이름
    private int baseValue;       // 기본 값 (스탯 포인트를 투자하는 경우에만 값이 변경된다.)
    private int finalValue;      // 아이템/버프효과 등이 적용된 값
    private bool isDirty = true; // finalValue를 계산해야하는지 그 여부

    public List<Modifier> modifiers = new List<Modifier>();

    public Stat(string key, int baseValue)
    {
        this.key = key;
        this.baseValue = baseValue;
    }

    public void IncreaseBaseValue(int amount)
    {
        baseValue += amount;
        isDirty = true;
    }

    public int GetValue()
    {
        if (isDirty) CalculateFinalValue();

        return finalValue;
    }

    // 아이템을 장착하거나 버프/디버프를 받는 경우 호출된다.
    public void AddModifier(Modifier modi)
    {
        // FLAT인 경우 가장 앞에 추가하고 PERCENT인 경우 마지막에 추가한다
        if (modi.type == Modifier.Type.FLAT)
            modifiers.Insert(0, modi);
        else if (modi.type == Modifier.Type.PERCENT)
            modifiers.Add(modi);

        isDirty = true;
    }

    // 아이템을 해제하거나 버프/디버프가 해제되는 경우 호출된다.
    public void RemoveModifier(Modifier modi)
    {
        modifiers.Remove(modi);

        isDirty = true;
    }

    // baseValue에 modifier를 적용시킨 finalValue를 계산한다.
    private void CalculateFinalValue()
    {
        // finalValue를 baseValue로 초기화
        finalValue = baseValue;

        // FLAT인 경우만 먼저 계산
        int index;
        for (index = 0; index < modifiers.Count; index++)
        {
            if (modifiers[index].type == Modifier.Type.PERCENT)
                break;

            finalValue += modifiers[index].value;
        }

        // PERCENT인 경우를 계산
        int percent = 0;
        for (; index < modifiers.Count; index++)
            percent += modifiers[index].value;
        finalValue = Mathf.RoundToInt(finalValue * (1 + percent * 0.01f));

        isDirty = false;
    }
}
