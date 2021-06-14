namespace TcOpen.Inxton.Security.Wpf
{
    using System;
    using System.Linq;

    /// <summary>
    /// Protection mode of the user control.
    /// </summary>
    public enum SecurityModeEnum
    {
        /// <summary>
        /// The user control will not be visibile when current user lacks permissions.
        /// </summary>
        Invisible,
        /// <summary>
        /// The user control will be disabled whe current user lacks permissions.
        /// </summary>
        Disabled,        
    }
}
