//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using ADotNet.Clients;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks.SetupDotNetTaskV1s;

var githubPipeline = new GithubPipeline
{
    Name = "Excel.Importer Build Pipline",

    OnEvents = new Events
    {
        PullRequest = new PullRequestEvent
        {
            Branches = new string[] { "master" }
        },

        Push = new PushEvent
        {
            Branches = new string[] { "master" }
        },
    },

    Jobs = new Dictionary<string, Job>
    {
        {
            "build",

            new Job
            {
                RunsOn = BuildMachines.Windows2022,

                Steps = new List<GithubTask>
                {
                    new CheckoutTaskV2
                    {
                        Name = "Checking out"
                    },

                    new SetupDotNetTaskV1
                    {
                        Name = "Setting up .Net",

                        TargetDotNetVersion = new TargetDotNetVersion
                        {
                            DotNetVersion = "7.x"
                        }
                    },

                    new RestoreTask
                    {
                        Name = "Restoring"
                    },

                    new DotNetBuildTask
                    {
                        Name = "Building"
                    },

                    new TestTask
                    {
                        Name = "Testing"
                    }
                }
            }
        }
    }
};

var aDotNetClient = new ADotNetClient();

string buildScriptPath = "../../../../.github/workflows/dotnet.yml";
string directoryPath = Path.GetDirectoryName(buildScriptPath);

if (!Directory.Exists(directoryPath))
{
    Directory.CreateDirectory(directoryPath);
}

aDotNetClient.SerializeAndWriteToFile(githubPipeline, path: buildScriptPath);
