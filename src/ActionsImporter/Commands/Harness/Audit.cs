using System.Collections.Immutable;
using System.CommandLine;

namespace ActionsImporter.Commands.Harness;

public class Audit : ContainerCommand
{
    public Audit(string[] args) : base(args)
    {
    }

    protected override string Name => "harness";
    protected override string Description => "An audit will output a list of data used in a Harness instance.";

    protected override ImmutableArray<Option> Options => ImmutableArray.Create<Option>(
        Common.Organization,
        Common.AccountId,
        Common.Project,
        Common.InstanceUrl,
        Common.AccessToken,
        Common.ConfigFilePath
    );
}
