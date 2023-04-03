using ADotNet.Clients;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks.SetupDotNetTaskV1s;

var githubPipeline = new GithubPipeline
{
	Name = "ChatGPT",
	 
	 OnEvents = new Events
	 {
		  PullRequest = new PullRequestEvent
		  {
			   Branches = new  string[] {"Main"}
		  },

		   Push = new PushEvent
		   {
			    Branches= new string[] {"Main"}
		   }
	 },

	  Jobs = new Jobs
	  {
		   Build = new BuildJob
		   {
			  RunsOn = BuildMachines.WindowsLatest,

			   Steps = new List<GithubTask>
			   {
                 new CheckoutTaskV2
				 {
					  Name = "Chesking uot"
				 },

				 new SetupDotNetTaskV1
				 {
					  Name = "Inistaling .Net",

					   TargetDotNetVersion = new TargetDotNetVersion
					   {
						    DotNetVersion = "7.0.200"
					   }
				 },
				 
				 new RestoreTask
				 {
					  Name = "Restoring  pascages"
				 },

				 new DotNetBuildTask
				 {
					  Name = "Building Project"
				 },

				 new TestTask
				 {
					 Name ="Testing Project"
				 }
			   }
			  
		   }
	  }
};

var adotnetClint = new ADotNetClient();
adotnetClint.SerializeAndWriteToFile(
	githubPipeline,
	path: "../../../../.github/workflows/build.yml");
