using System;

namespace eaw.build.app.util
{
    internal class Options
    {
        internal object Option { get; }

        internal Type InternalType
        {
            get => Option.GetType();
        }

        internal Options(object option)
        {
            Option = option ?? throw new ArgumentNullException(nameof(option));
        }
    }
}