using System.CommandLine;

namespace ActionsImporter.Commands.Harness;

public static class Common
{
    public static readonly Option<string> InstanceUrl = new(new[] { "-u", "--harness-instance-url" })
    {
        Description = "The URL of the Harness instance.",
        IsRequired = false,
    };

    public static readonly Option<string> AccessToken = new(new[] { "-t", "--harness-access-token" })
    {
        Description = "Access token for the Harness instance.",
        IsRequired = false,
    };

    public static readonly Option<string> Organization = new(new[] { "-g", "--harness-organization" })
    {
        Description = "The Harness organization name.",
        IsRequired = false,
    };

    public static readonly Option<string> AccountId = new(new[] { "-a", "--harness-account-id" })
    {
        Description = "The Harness account identifier.",
        IsRequired = false,
    };

    public static readonly Option<string> Project = new(new[] { "-p", "--harness-project" })
    {
        Description = "The Harness project name.",
        IsRequired = false,
    };

    public static readonly Option<FileInfo> SourceFilePath = new("--source-file-path")
    {
        Description = "The file path corresponding to the Harness pipeline file.",
        IsRequired = false,
    };

    public static readonly Option<string> Pipeline = new(new[] { "--harness-pipeline", "-r" })
    {
        Description = "The Harness pipeline identifier.",
        IsRequired = true,
    };

    public static readonly Option<FileInfo> ConfigFilePath = new("--config-file-path")
    {
        Description = "The file path to the GitHub Actions Importer configuration file.",
        IsRequired = false,
    };
}
