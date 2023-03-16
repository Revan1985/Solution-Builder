## Alesys Rebrandizer v1.0


### JSON file structure

1. #### Configuration (root-level)
  - Output path
  - Source path
  - Temporary path
  - Installations (configurations)
2. #### Installations
  - Name
  - Solution filename
  - Source path
  - Output path
  - Enabled (y/n)
  - Clear destination (y/n)
  - Commands
3. #### Commands
  - Name
  - Type
  - Projects 
	(...) project names from **.sln
  - Actions
4. #### Actions	
  - Name
  - Source
  - Source Fallback
  - Value
  - File (default "*.cs")