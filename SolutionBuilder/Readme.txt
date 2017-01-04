Little explanation about Solution Builder.

This project is splitted under four projects.
One dll project (SolutionBuilder)
One Winform Project (SolutionBuilder.WF)
One Window Presentation Foundation Project (SolutionBuilder.WPF.Config)
One Unit Test Project (SolutionBuilder.Tests)


+ SolutionBuilder:
	This project provide classes for other project, loads configuration, execute building, packaging and other stuffs.
	Configuration.cs is the main file, last version is stored under  namespace, the previous one (not in , is used for configuration, not et migrated)
	Differences are:
		1. Better xml mapping
		2. Module and Control now use only one class type (ModuleType)

+ SolutionBuilder.WF:
	This is the main project (After dll)
	This gui provide the visual feedback and events for starting building.
	MainForm is the main form, splitted into 5 big area.
		1. Actions
		2. Installations.
		3. Installation Modules/Controls/Database
		4. Installation status.
		5. Logging acton/status

+ SolutionBuilder.WPF.Settings:
	Project for configurating the build process.
	Use old configuration classes (not in  namespace), but ths result is compatible  with the new one.
	Only main difference is the missing of OutputPath in Modules and Controls xml tree.

+ SolutionBuilder.Tests:
	This project is for unit-testing only.
	It test only Configuration for now.


***** General Notes:

Any changes to Config.xml must be set in Config.Default.xml.
This to avoid every time people to change their Source.
Or, provide an installation for every people (but Installations output path must always be changed)

Note: Config.xml is set to Content and Always Copy to Output Directory in the SolutionBuilder project.
	  Since this project is used as a Project Reference in all of the other projects, you need to change
	  the SolutionBuilder\Config.xml and it is copied everywhere the project is referenced.