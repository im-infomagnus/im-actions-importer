using System.Collections.Immutable;
using System.CommandLine;

namespace ActionsImporter.Commands.Harness;

public class Migrate : ContainerCommand
{
    public Migrate(string[] args) : base(args)
    {
    }

    protected override string Name => "harness";
    protected override string Description => "Convert a Harness pipeline to a GitHub Actions workflow and open a pull request with the changes.";

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
