# Empire at War Build Tool

The EaW build tool allows for automated building of mod binary files and cooking of mods into `*.meg` files. Packaging mods allows to get rid of overhead and fragmentation issues when loading game data into memory, and allows for AI files to be read properly by the game.

## Features

Currently (05/08/2018) the build tool supports the following features:

* Creating `*.dat` files from conform `TranslationManifest.xml` file.
* Cooking a mod into `*.meg` archives.
* Versioning of mod releases conform to the [Semantic Versioning Scheme](https://semver.org/ "Go to https://semver.org/")
* Creating a patch build from a file list.

## Usage

The build tool is an console application that can be run in modes, depending on the arguments passed to it.
It always assumes that you have a file named `build.cfg` placed inside of your mod's base folder, which is used by the build tool to figure out how to build and package your mod. See **Build Configuration** for details.

### Commands

```
build <mod-path> [d]
```
The `build` command can be used to generate binary files prior to cooking the mod.

The command takes the required argument `<mod-path>` which points to the base directory of your mod, e.g. `C:\Some\Place\MyMod\`. The `MyMod` directory has to include the mod's `Data` folder and any other assets.
At the moment it only creates `*.dat` files from a valid `TranslationManifest.xml` file that has to be placed inside of your `\MyMod\Data\Text\` directory.
Such a translation manifest is the default output format of my TextEditor, but can also be edited by hand. See **Translation Manifest** for details.

The optional `d` flag is a so called "destructive flag" when it is passed as an argument, the tool will delete previous version of the `*.dat` files it creates, if the flag is not passed, it will back them up instead.
For keeping a version history, it is advised to use the `TranslationManifest.xml` as version control systems like git can handle and merge the file without problems.

```
cook  <mod-base-path> <output-path> [iver-maj|iver-min|iver-patch]
```

The `cook` command will package an existing mod into one or multiple `*.meg` files, depending on how the `build.cfg` is configured.

The command takes the required argument `<mod-path>` which points to the base directory of your mod, e.g. `C:\Some\Place\MyMod\`. The `MyMod` directory has to include the mod's `Data` folder and any other assets.

It also takes the required argument `<output-path>` which points to the directory you want the `*.meg` files to be written to, e.g. `C:\Some\Place\MyMod\cook\`.

The optional `iver` flag automatically increases your mod version depending on the flag set according to the [Semantic Versioning Scheme](https://semver.org/ "Go to https://semver.org/"). It can be omitted, if you do not care for versioning. If the flag is being used, it has to be one of the three options:

* `iver-maj` Increases the major version of the mod, e.g. from 1.2.2 to 2.0.0 in accordance to the Semantic Versioning.
* `iver-min` Increases the minor version of the mod, e.g. from 1.2.2 to 1.3.0 in accordance to the Semantic Versioning.
* `iver-patch` Increases the patch version of the mod, e.g. from 1.2.2 to 1.2.3 in accordance to the Semantic Versioning.

```
patch  <mod-base-path> <output-path> <patch-file>
```
The `patch` command will generate a patch bundle with the files listed in `<patch-file>`

The command takes the required argument `<mod-path>` which points to the base directory of your mod, e.g. `C:\Some\Place\MyMod\`. The `MyMod` directory has to include the mod's `Data` folder and any other assets.

It also takes the required argument `<output-path>` which points to the directory you want the `*.meg` files to be written to, e.g. `C:\Some\Place\MyMod\cook\`.

### Patch file list
A valid patch file can be generated via the console `git` command:
```
git diff --name-only commit-hash-a commit-hash-b > patch.content
``` 

It will generate a file containing a list of all changed files between the two commits:
Running the command for YVaW generated a file with the following content:
```
data/text/TranslationManifest.xml
data/xml/GameObjectFiles.xml
data/xml/HardPointDataFiles.xml
data/xml/_res/hardpoints/ships/hp_freighter_ct200.xml
data/xml/_res/hardpoints/turrets/hp_turret_ftaab.xml
data/xml/_res/hardpoints/turrets/hp_turret_hbt.xml
data/xml/_res/projectiles/proj_laser.xml
data/xml/_res/units/space/master/m_freighter_ct200.xml
data/xml/_res/units/space/variants/var_freighter_ct200.xml
```

Any text file with a similar syntax will do though, just make sure the path starts at the `data` folder of your mod.

### Build Configuration
The build tool requires a `build.cfg` file placed inside of the top level folder of your mod. (The one that holds the data folder and the one you point the build to to with the `<mod-path>` argument.)
A minimal build file looks like this:

```xml
<ModBuildConfig">
  <ModSettings Name="MyMod" Version="0.0.1" ReleaseType="Full" />
  <BuildSettings>
    <Localisation LocalisationFile="TranslationManifest.xml" />
    <IncludeBaseGameFiles>True</IncludeBaseGameFiles>
  </BuildSettings>
</ModBuildConfig>
```

Running a build and cook sequence with this file will create all `*.dat` translation files that are configured inside of your `TranslationManifest.xml` but not create any `*.meg` packages.

To cook files into packages, you will have to configure how the build tool should package your files. to do so, you have to create bundle settings:

```xml
<Bundle Name="MyBundle">
  <Directory FilePattern="*" Recurse="true">data\xml</Directory>
  <Directory FilePattern="*.dds" >data\art</Directory>
  <File>Data\XML\CampaignFiles.xml</File>
</Bundle>
```

A bundle may include any number of `Directory` and `File` definitions, within a bundle the tool will prevent duplicates of objects. Across different bundles the tool will **not** prevent duplicates, as the game only loads the last instance of files with identical names. Each bundle will be cooked into a `*.meg` file of the same name.

#### Configuring a Bundle
A bundle definition consists of several elements.

```xml
<Bundle Name="MyBundle">
```
The bundle definition itself. The name you set here will be the name of your cooked `*.meg` file. In this case the bundle would create a file called `MyBundle.meg`

```xml
<File>Data\XML\CampaignFiles.xml</File>
```
A file definition points to one specific file within your mod. Can be used if you want to include *that* one file in a given directory, but nothing else.
Also, take note that the path is relative to the mod's base path without the leading backslash.

```xml
<Directory FilePattern="*" Recurse="true">data\xml</Directory>
```
The directory definition is what you will probably use most of the time, it points to a given directory relative to the mod's base path.

The `FilePattern` setting is required and specifies what file types you want to include.
The pattern can use the following wildcards:

| Wildcard specifier | Matches |
|---|---|
|* (asterisk)| Zero or more characters in that position. |
|? (question mark)|Zero or one character in that position.|

When you use the asterisk wildcard character in a `FilePattern` such as `"*.txt"`, the number of characters in the specified extension affects the search as follows:

* If the specified extension is *exactly* three characters long, the method returns files with extensions that begin with the specified extension. For example, `"*.xls"` returns both `"book.xls"` and `"book.xlsx"`.

* In all other cases, the method returns files that exactly match the specified extension. For example, `"*.ai"` returns `"file.ai"` but not `"file.aif"`.

When you use the question mark wildcard character, this method returns only files that match the specified file extension. For example, given two files, `"file1.txt"` and `"file1.txtother"`, in a directory, a search pattern of `"file?.txt"` returns just the first file, whereas a search pattern of `"file*.txt"` returns both files.

The `Recurse` setting is optional, it can be set to either `true` or `false`, if not set it will default to `false`.
If set to `true`, it will recursively search through all directories contained within the given Directory and pack files that match the given pattern.
If set to `false`, it will only pack files that match the pattern within the given directory, but ignore child directories.

#### Example: `build.cfg`

```xml
<ModBuildConfig>
  <ModSettings Name="MyMod" Version="1.0.2" ReleaseType="Full" />
  <BuildSettings>
    <Localisation LocalisationFile="TranslationManifest.xml" />
    <Bundle Name="TestBunde_00">
      <File>Data\XML\CampaignFiles.xml</File>
      <File>Data\XML\CommandBarComponentFiles.xml</File>
      <File>Data\XML\FactionFiles.xml</File>
      <Directory FilePattern="*">Data\XML\Enum</Directory>
      <Directory FilePattern="*.lua" Recurse="true">Data\Scripts</Directory>
    </Bundle>
    <Bundle Name="TestBunde_01">
      <Directory FilePattern="*.xml" Recurse="true">Data\XML\AI</Directory>
    </Bundle>
    <IncludeBaseGameFiles>True</IncludeBaseGameFiles>
  </BuildSettings>
</ModBuildConfig>
```

### Creating a valid `TranslationManifest.xml`

The easiest way to create a `TranslationManifest.xml` would be by using my TextEditor which is available as an alpha version via the ModHub discord.

If you want to manually create one, you will have to make sure it consists of a least the following nodes:

```
<?xml version="1.0" encoding="utf-8"?>
<LocalisationData xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Localisation Key="KEY_NAME">
    <TranslationData>
      <Translation Language="ENGLISH"><![CDATA[The text that shows up ingame.]]></Translation>
    </TranslationData>
  </Localisation>
</LocalisationData>
```

A localisation entry is made up of a unique `Key`, the translation data can technically hold any number of translations wrapped in `CDATA` tags, identified by its `Language`, but it's currently limited to the languages the game supports: Engish, German, French, Italian, Spanish. (all in capital letters).
