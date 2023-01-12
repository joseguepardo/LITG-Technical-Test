namespace LifeIsTheGame.TechnicalTest
{
    public interface IPlayer
    {
        IPlayerAnimator playerAnimator { get; }
        IPlayerController playerController { get; }
        IPlayerCameraController playerCameraController { get; }
    }
}