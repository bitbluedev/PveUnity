namespace Pve.Handlers
{
    internal abstract class StateHandlerBase
    {
        public string Description { get; set; }

        public abstract void Execute();
    }
}