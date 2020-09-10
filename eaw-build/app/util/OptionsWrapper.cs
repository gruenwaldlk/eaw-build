using System;

namespace eaw.build.app.util
{
    internal class OptionsWrapper
    {
        internal object Option { get; }

        internal Type InternalType
        {
            get => Option.GetType();
        }

        internal bool Verbose
        {
            get
            {
                if (InternalType == typeof(Builder.BuildOptions))
                {
                    Builder.BuildOptions o = (Builder.BuildOptions) Option;
                    return o.Verbose;
                }

                if (InternalType == typeof(Builder.CookOptions))
                {
                    Builder.CookOptions o = (Builder.CookOptions) Option;
                    return o.Verbose;
                }

                if (InternalType == typeof(Builder.MigrationOptions))
                {
                    Builder.MigrationOptions o = (Builder.MigrationOptions) Option;
                    return o.Verbose;
                }

                if (InternalType == typeof(Builder.NewOptions))
                {
                    Builder.NewOptions o = (Builder.NewOptions) Option;
                    return o.Verbose;
                }

                if (InternalType == typeof(Builder.ReleaseOptions))
                {
                    Builder.ReleaseOptions o = (Builder.ReleaseOptions) Option;
                    return o.Verbose;
                }

                return false;
            }
        }

        internal OptionsWrapper(object option)
        {
            Option = option ?? throw new ArgumentNullException(nameof(option));
        }
    }
}
