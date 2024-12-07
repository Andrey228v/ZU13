using Assets.Scripts.Skills.SkillState;

namespace Assets.Scripts.Skills
{
    public interface ISkillView
    {
        public void SelectState(SkillStateType stateType);
    }
}
