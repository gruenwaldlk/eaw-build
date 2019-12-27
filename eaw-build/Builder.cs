using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using CommandLine;
using eaw.build.app;
using eaw.build.app.util;

// [Kad] Disable ReSharper annotations. Following up on the suggestions would break the commandline parsing.
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable ConvertToAutoProperty

/*
 * I've been going through all my tools, and amongst other things I'm currently working on version 2 of my EaW build tool.
 * Because I've started learning Rust (https://www.rust-lang.org) a few months ago, I've really come to appreciate Cargo, which is Rust's package-manager/build-tool/ci-cd-tool/whatever-else thing, so I thought to myself, why not have something similar for EaW?
 * I've done most of the legwork in the past with `*.meg`, and `*.dat` libraries, so building a command line interface on top of that isn't too much of a hassle.
 *
 * Current feature set:
 * ```
 * new <mod-name> <path> -e|--eaw <eaw-path> -f|--foc <foc-path>         Generates a new mod with a default (non-functional) eaw-build configuration.
 * migrate -m|--mod <old-config>                                         Migrate a yvaw-build project to an eaw-build project.
 * migrate -t|--text <config-file>                                       Migrate a translationmanifest.xml to a localisation project.
 * build <config-file>                                                   Builds (generates the `*.DAT` files and commandbar files) a mod. The build operation is defined by the config file supplied as argument.
 * cook <config-file>                                                    Cooks (packages files into `*.MEG` files and moves files to a target directory) a mod. The cook operation is defined by the config file supplied as argument.
 * release <config-file> <target-directory> (--major|--minor|--patch)    Releases a mod: Builds the mod, cooks the mod, and copies the built mod to a target directory. Optionally it also increments the version number. A config file needs to be supplied as argument.
 * ```
 */
[assembly: InternalsVisibleTo("eaw-build.test")]

namespace eaw.build
{
    internal class Builder
    {
        [Verb("migrate", HelpText = "Migrate a yvaw-build project to an eaw-build project.")]
        public class MigrationOptions
        {
            private readonly bool _modProjectMigration;
            private readonly bool _translationMigration;
            private readonly string _configurationFilePath;

            public MigrationOptions(bool modProjectMigration, bool translationMigration, string configurationFilePath)
            {
                _modProjectMigration = modProjectMigration;
                _translationMigration = translationMigration;
                _configurationFilePath = configurationFilePath;
            }

            [Option('m', "mod", Required = true, Default = false, SetName = nameof(ModProjectMigration),
                HelpText =
                    "Migrate a yvaw-build project to an eaw-build project. --mod and --text are mutually exclusive options.")]
            public bool ModProjectMigration => _modProjectMigration;

            [Option('t', "text", Required = true, Default = false, SetName = nameof(TranslationMigration),
                HelpText =
                    "Migrate a translationmanifest.xml to a localisation project. --mod and --text are mutually exclusive options.")]
            public bool TranslationMigration => _translationMigration;

            [Value(0, Required = true, MetaName = "<CONFIGURATION-FILE>",
                HelpText =
                    "The path to the configuration file to migrate. If the path contains spaces, wrap it in quotation marks (\").")]
            public string ConfigurationFilePath => _configurationFilePath;
        }

        [Verb("new", HelpText = "Initialises a new EaW mod project.", Hidden = true)]
        public class NewOptions
        {
            private readonly string _modName;
            private readonly string _path;
            private readonly string _eawPath;
            private readonly string _focPath;
            private readonly bool _createGitRepository;

            public NewOptions(string modName, string path, string eawPath, string focPath, bool createGitRepository)
            {
                _modName = modName;
                _path = path;
                _eawPath = eawPath;
                _focPath = focPath;
                _createGitRepository = createGitRepository;
            }

            [Value(0, Required = true, MetaName = "<MOD-NAME>", HelpText = "The mod's name.")]
            public string ModName => _modName;

            [Value(1, Required = true, MetaName = "<MOD-PATH>",
                HelpText = "The path to the directory to initialise the mod in.")]
            public string Path => _path;

            [Option('e', "eaw", Required = true, HelpText = "The path to the Empire at War data directory.")]
            public string EawPath => _eawPath;

            [Option('f', "foc", Required = true, HelpText = "The path to the Forces of Corruption data directory.")]
            public string FocPath => _focPath;

            [Option('r', "repository", Required = false, Default = false, HelpText = "Initialises a git repository.",
                Hidden = true)] // [Kad]: Hidden unimplemented feature.
            public bool CreateGitRepository => _createGitRepository;
        }

        [Verb("build", HelpText =
            "Builds (generates the `*.DAT` files and commandbar files) a mod. The build operation is defined by the config file supplied as argument.")]
        public class BuildOptions
        {
            private readonly string _configurationFile;

            public BuildOptions(string configurationFile)
            {
                _configurationFile = configurationFile;
            }

            [Value(0, Required = true, MetaName = "<CONFIGURATION-FILE>",
                HelpText = "The path to the mod's configuration file.")]
            public string ConfigurationFile => _configurationFile;
        }

        [Verb("cook", HelpText =
            "Cooks (packages files into `*.MEG` files and moves files to a target directory) a mod. The cook operation is defined by the config file supplied as argument.")]
        public class CookOptions
        {
            private readonly string _configurationFile;

            public CookOptions(string configurationFile)
            {
                _configurationFile = configurationFile;
            }

            [Value(0, Required = true, MetaName = "<CONFIGURATION-FILE>",
                HelpText = "The path to the mod's configuration file.")]
            public string ConfigurationFile => _configurationFile;
        }

        [Verb("release", HelpText = "Release a mod.")]
        [SuppressMessage("ReSharper", "ConvertToAutoProperty")]
        public class ReleaseOptions
        {
            private readonly string _configurationFile;
            private readonly bool _increaseMajor;
            private readonly bool _increaseMinor;
            private readonly bool _increasePatch;

            public ReleaseOptions(string configurationFile, bool increaseMajor, bool increaseMinor, bool increasePatch)
            {
                _configurationFile = configurationFile;
                _increaseMajor = increaseMajor;
                _increaseMinor = increaseMinor;
                _increasePatch = increasePatch;
            }

            [Value(0, Required = true, MetaName = "<CONFIGURATION-FILE>",
                HelpText = "The path to the mod's configuration file.")]
            public string ConfigurationFile => _configurationFile;

            [Option("major", SetName = "version-major", HelpText = "Create a new major release, e.g. 1.0.0, 3.0.0, ...",
                Default = false, Required = false)]
            public bool IncreaseMajor => _increaseMajor;

            [Option("minor", SetName = "version-minor", HelpText = "Create a new minor release, e.g. 1.1.0, 1.2.0, ...",
                Default = false, Required = false)]
            public bool IncreaseMinor => _increaseMinor;

            [Option("patch", SetName = "version-patch", HelpText = "Create a new patch release, e.g. 1.1.1, 1.1.2, ...",
                Default = false, Required = false)]
            public bool IncreasePatch => _increasePatch;
        }


        internal static int Main(string[] args)
        {
            Options options = Parser.Default
                .ParseArguments<MigrationOptions, NewOptions, BuildOptions, CookOptions, ReleaseOptions>(args)
                .MapResult(
                    (MigrationOptions opts) => new Options(opts),
                    (NewOptions opts) => new Options(opts),
                    (BuildOptions opts) => new Options(opts),
                    (CookOptions opts) => new Options(opts),
                    (ReleaseOptions opts) => new Options(opts),
                    errs => new Options(errs));
            ExitCode exitCode = Executor.Run(options);
            Environment.ExitCode = (int) exitCode;
            return (int) exitCode;
        }
    }
}