namespace ChatGPT.Intergation.Services
{
	public interface IAnswerGeneratorService
	{
		Task<string> GenerateAnswer(string prompt);
	}
}
