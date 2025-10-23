using System.Collections.Immutable;
using System.CommandLine;

namespace ActionsImporter.Commands.Harness;

public class DryRun : ContainerCommand
{
    public DryRun(string[] args) : base(args)
    {
    }

    protected override string Name => "harness";
    protected override string Description => "Convert a Harness pipeline to a GitHub Actions workflow and output its yaml file.";

    protected override ImmutableArray<Option> Options => ImmutableArray.Create<Option>(
        Common.Pipeline,
        Common.Organization,
        Common.AccountId,
        Common.Project,
        Common.InstanceUrl,
        Common.AccessToken,
        Common.SourceFilePath,
        Common.ConfigFilePath
    );
}
