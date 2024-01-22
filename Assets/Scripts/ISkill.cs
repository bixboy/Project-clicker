using UnityEngine;

public interface ISkill
{
        public void SetStat(int skillLevel);

        public bool IsActive();

        public SkillName GetSkillName();

}