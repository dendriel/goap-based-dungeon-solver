using System.Windows.Forms;

namespace Goap_Based_Dungeon_Solver.Source.Game.Api
{
    /// <summary>
    /// Provides means to the game update the interface.
    /// </summary>
    interface IUserInterface
    {
        /// <summary>
        /// Reset the screen graphics.
        /// </summary>
        void ResetGraphics();

        /// <summary>
        /// Add a new picture box into the form.
        /// </summary>
        /// <param name="picBox">The picture box to be added.</param>
        void SetupPictureBox(PictureBox picBox);

        /// <summary>
        /// Remove a picture box from the form.
        /// </summary>
        /// <param name="picBox">The picture box to be removed.</param>
        void RemovePictureBox(PictureBox picBox);
    }
}
