namespace NetOn
{
    /// <summary>
    /// GUI Interface
    /// </summary>
    public interface IGui
    {
        /// <summary>
        /// Updates icon on GUI with given status number
        /// </summary>
        /// <param name="NetOn"></param>
        void UpdateIcon(int NetOn);

        /// <summary>
        /// Disposes GUI
        /// </summary>
        void Dispose();
    }
}