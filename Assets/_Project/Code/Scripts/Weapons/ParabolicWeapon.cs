namespace LifeIsTheGame.TechnicalTest
{
    public class ParabolicWeapon : Weapon
    {
        public override void PickUp()
        {
            rb.isKinematic = true;
            rb.detectCollisions = false;
        }

        public override void Drop()
        {
            rb.isKinematic = false;
            rb.detectCollisions = true;
        }
    }
}