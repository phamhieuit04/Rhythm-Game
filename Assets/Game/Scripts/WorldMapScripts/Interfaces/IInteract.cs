public interface IInteract
{
    public bool CanInteract { get; set; }

    public void Interact();

    public void ChangeVisual(bool isNear);
}
