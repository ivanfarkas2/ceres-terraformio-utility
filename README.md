# Terraform.io Workspace Management Utility
Help Terraform.io workspace management utility.

## Prerequisites
### Framework
.NET Core 3.1

### Environment Variables
	TFIO_ORG = "[your organization]";
	TFIO_TEAM_ID = "[your team Id]";
	TFIO_T_TOKEN = "[your team Token]";
	TFIO_O_TOKEN = "[your organization Token]";

## Install
### Linux 64

	wget https://raw.githubusercontent.com/ivanfarkas2/ceres-terraformio-utility/master/install.sh
	chmod 755 install.sh
	./install.sh  

## Usage
TerraformIoUtility help

    wcl         - Clone Workspace
    cpv         - Copy Variables
    crv         - Create Variable
    wcr         - Create Workspace
    t           - Get Team
    ts          - Get Teams
    ws          - Get Workspaces
    l           - List Variables
    w           - Show Workspace
    wi          - Show Workspace Id

    help <name> - For help with one of the above commands    

### Clone Workspace

	'wcl' - Clone Workspace
	Clone Workspace.
	Expected usage: TerraformIoUtility wcl <options>
	<options> available:
      --swn, --sourceworkspacename=VALUE
                             Source Workspace Name.
      --dwn, --destinationworkspacename=VALUE
                             Destination Workspace Name.

### Copy Variables

	'cpv' - Copy Variables
	Copy Variables.
	Expected usage: TerraformIoUtility cpv <options>
	<options> available:
      --swn, --sourceworkspacename=VALUE
                             Source Workspace Name.
      --twn, --targetworkspacename=VALUE
                             Target Workspace Name.
	Optional:	  
      -x, --excludevariables=VALUE
	                         Exclude Variables.
	  -i, --includevariables=VALUE
                             Include Variables.
### Create Variable
	'crv' - Create Variable
	Create Variable.
	Expected usage: TerraformIoUtility crv <options>
	<options> available:
      --twn, --targetworkspacename=VALUE
                             Target Workspace Name.
      --vf, --variablefile=VALUE
                             Variable File.

### Create Workspace
	'wcr' - Create Workspace
	Create Workspace.
	Expected usage: TerraformIoUtility wcr <options>
	<options> available:
      --wf, --workspacefile=VALUE
                             Workspace File.

### Get Team
	't' - Get Team
	Get Team.
	Expected usage: TerraformIoUtility t <options>
	<options> available:
      --ti, --teamid=VALUE   Team Id.

### Get Teams
	'ts' - Get Teams
	Get Teams.
	Expected usage: TerraformIoUtility ts

### Get Workspaces
	'ws' - Get Workspaces
	Get Workspaces.
	Expected usage: TerraformIoUtility ws

### List Variables
	'l' - List Variables
	List Variables.
	Expected usage: TerraformIoUtility l <options>
	<options> available:
      --wn, --workspacename=VALUE
                             Workspace Name.

### Show Workspace
	'w' - Show Workspace
	Show Workspace.
	Expected usage: TerraformIoUtility w <options>
	<options> available:
      --wn, --workspacename=VALUE
                             Workspace Name.

### Show Workspace Id
	'wi' - Show Workspace Id
	Show Workspace Id.
	Expected usage: TerraformIoUtility wi <options>
	<options> available:
      --wn, --workspacename=VALUE
                             Workspace Name.
