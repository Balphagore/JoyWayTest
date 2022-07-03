namespace JoyWayTest.Core
{
    public interface IDamagable
    {
        void TakeNormalDamage(float normaldamage);
        void TakeStatusDamage(float statusDamage, float normalDamage);
    }
}