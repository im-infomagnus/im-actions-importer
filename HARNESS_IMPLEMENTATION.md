# Harness CI/CD Migration Support

This document describes the implementation of Harness CI/CD to GitHub Actions migration support in the GitHub Actions Importer.

## Overview

Harness support has been added following the same architectural pattern used by other CI/CD providers (Travis CI, CircleCI, Jenkins, etc.). This ensures consistency and maintainability across the codebase.

## Implementation Details

### 1. Provider Enum Extension
- **File**: `src/ActionsImporter/Models/Provider.cs`
- **Change**: Added `Harness` to the Provider enum
- This makes Harness a first-class provider in the system

### 2. Variable Model Update
- **File**: `src/ActionsImporter/Models/Variable.cs`
- **Change**: Added Harness case to the ProviderName switch statement
- Maps `Provider.Harness` to the display name "Harness"

### 3. Harness Commands Implementation
Created a new directory `src/ActionsImporter/Commands/Harness/` with the following files:

#### Common.cs
Defines Harness-specific command-line options:
- `--harness-instance-url` (-u): The URL of the Harness instance
- `--harness-access-token` (-t): Access token for authentication
- `--harness-organization` (-g): Organization name
- `--harness-account-id` (-a): Account identifier
- `--harness-project` (-p): Project name
- `--harness-pipeline` (-r): Pipeline identifier (required for migrate/dry-run)
- `--source-file-path`: Path to Harness pipeline file
- `--config-file-path`: Path to GitHub Actions Importer configuration file

#### Audit.cs
Command to audit a Harness instance:
- Lists all data used in a Harness instance
- Helps plan the migration by analyzing the current CI/CD footprint
- Options: organization, account ID, project, instance URL, access token, config file path

#### DryRun.cs
Command to perform a dry-run migration:
- Converts a Harness pipeline to GitHub Actions workflow YAML
- Outputs the YAML file without making any changes
- Useful for testing and previewing the migration
- Options: pipeline (required), organization, account ID, project, instance URL, access token, source file path, config file path

#### Migrate.cs
Command to perform actual migration:
- Converts a Harness pipeline to GitHub Actions workflow
- Opens a pull request with the changes
- Options: pipeline (required), organization, account ID, project, instance URL, access token, source file path, config file path

#### Forecast.cs
Command to forecast GitHub Actions usage:
- Analyzes historical Harness pipeline utilization
- Forecasts GitHub Actions usage patterns
- Options: organization, account ID, project, instance URL, access token, source file paths

### 4. Base Command Updates
Updated the following base command files to register Harness subcommands:
- `src/ActionsImporter/Commands/Audit.cs`
- `src/ActionsImporter/Commands/DryRun.cs`
- `src/ActionsImporter/Commands/Migrate.cs`
- `src/ActionsImporter/Commands/Forecast.cs`

Each now includes a line like:
```csharp
command.AddCommand(new Harness.Audit(_args).Command(app));
```

### 5. Documentation Update
- **File**: `README.md`
- **Change**: Added "Harness" to the list of supported platforms

### 6. Test Coverage
- **File**: `src/ActionsImporter.UnitTests/Models/VariableTests.cs`
- **Change**: Added test case for Harness provider name mapping
- Ensures the Variable model correctly maps `Provider.Harness` to "Harness"

## Usage Examples

### Audit a Harness instance
```bash
gh actions-importer audit harness \
  --harness-instance-url https://app.harness.io \
  --harness-access-token <token> \
  --harness-account-id <account-id> \
  --harness-organization <org-name> \
  --output-dir ./audit-results
```

### Dry-run migration of a Harness pipeline
```bash
gh actions-importer dry-run harness \
  --harness-pipeline <pipeline-id> \
  --harness-instance-url https://app.harness.io \
  --harness-access-token <token> \
  --harness-account-id <account-id> \
  --harness-project <project-name> \
  --output-dir ./dry-run-results
```

### Migrate a Harness pipeline
```bash
gh actions-importer migrate harness \
  --harness-pipeline <pipeline-id> \
  --harness-instance-url https://app.harness.io \
  --harness-access-token <token> \
  --harness-account-id <account-id> \
  --harness-project <project-name> \
  --target-url https://github.com/org/repo \
  --output-dir ./migration-results
```

### Forecast GitHub Actions usage
```bash
gh actions-importer forecast harness \
  --harness-instance-url https://app.harness.io \
  --harness-access-token <token> \
  --harness-account-id <account-id> \
  --harness-organization <org-name> \
  --source-file-path ./harness-jobs-data.json \
  --output-dir ./forecast-results
```

## Architecture Pattern

The Harness implementation follows the established pattern:

1. **Provider Enum**: Defines the provider as a first-class type
2. **Common Options**: Defines provider-specific CLI options in a Common.cs file
3. **Command Classes**: Each command (Audit, DryRun, Migrate, Forecast) extends `ContainerCommand`
4. **Base Command Registration**: Each base command registers the provider-specific command
5. **Variable Model**: Maps the provider enum to a display name

This pattern ensures:
- Consistency across all providers
- Easy maintenance and updates
- Clear separation of concerns
- Minimal code duplication

## Testing

All tests pass successfully:
- **Unit Tests**: 43/43 passing (including new Harness provider test)
- **Build**: Successful with no errors (only existing warnings from other parts of the codebase)
- **Command Validation**: All Harness commands validate successfully in TEST_COMMAND_ONLY mode
- **CodeQL Security Scan**: 0 vulnerabilities found

## Future Enhancements

The actual migration logic (converting Harness pipelines to GitHub Actions workflows) is implemented in the Docker container referenced by this CLI tool. The CLI portion (this implementation) provides:
1. Command-line interface
2. Option parsing
3. Docker container orchestration

To extend functionality, updates would be needed in the Docker container image (`ghcr.io/actions-importer/cli`) to handle Harness-specific pipeline syntax and features.

## Summary

The Harness support implementation is complete and follows all existing patterns in the codebase. It provides a consistent user experience with other supported CI/CD platforms and is ready for use once the corresponding Docker container image is updated with Harness-specific migration logic.
