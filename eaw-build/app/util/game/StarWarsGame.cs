using System.Collections.ObjectModel;

namespace eaw.build.app.util.game
{
    internal static class StarWarsGame
    {
        internal static class EmpireAtWar
        {
            internal static readonly ReadOnlyCollection<string> RESERVED_BUNDLE_NAMES = new ReadOnlyCollection<string>(
                new[]
                {
                    "russianspeech",
                    "models",
                    "italianspeech",
                    "germanspeech",
                    "englishspeech",
                    "japanesespeech",
                    "koreanspeech",
                    "music",
                    "config",
                    "shaders",
                    "chinesespeech",
                    "movies",
                    "frenchspeech",
                    "cinematics",
                    "polishspeech",
                    "thaispeech",
                    "textures",
                    "maps",
                    "spanishspeech",
                    "patch",
                    "patch2"
                });
        }

        internal static class ForcesOfCorruption
        {
            internal static readonly ReadOnlyCollection<string> RESERVED_BUNDLE_NAMES = new ReadOnlyCollection<string>(
                new[]
                {
                    "russianspeech",
                    "frenchspeech",
                    "config",
                    "shaders",
                    "cinematics",
                    "spanishspeech",
                    "maps",
                    "movies",
                    "models",
                    "italianspeech",
                    "polishspeech",
                    "englishspeech",
                    "textures",
                    "music",
                    "germanspeech",
                    "patch",
                    "patch2"
                });
        }
    }
}