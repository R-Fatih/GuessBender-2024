using Octokit;

namespace GuessBender_2024.WebUI.Tools
{
	public static class GitHubFileUploader
	{
		public static async void CreateFile(int id, string content, string type)
		{
			var client = new GitHubClient(new ProductHeaderValue("ACCDB"));
			client.Credentials = new Credentials("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");

			var (owner, repoName, filePath, branch) = ("R-Fatih", "ACCDB",
		"Logos/" + type + "/" + id + ".png", "main");
			string base64String = await GetImageAsBase64Async(content);

			var result = await client.Repository.Content.CreateFile(
		owner, repoName, filePath,
		new CreateFileRequest($"First commit for {filePath}", base64String, branch, false));

		}
		public static async void UpdateFile(string id, string content, string type)
		{
			var client = new GitHubClient(new ProductHeaderValue("ACCDB"));
			client.Credentials = new Credentials("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");

			var (owner, repoName, filePath, branch) = ("R-Fatih", "ACCDB",
		"Logos/" + type + "/" + id + ".png", "main");
			string base64String = await GetImageAsBase64Async(content);

			var result = await client.Repository.Content.UpdateFile(
		owner, repoName, filePath,
		new UpdateFileRequest($"First commit for {filePath}", base64String, branch, false));

		}

		public static async Task<string> GetImageAsBase64Async(string url) // return Task<string>
		{
			using (var client = new HttpClient())
			{
				var bytes = await client.GetByteArrayAsync(url); // there are other methods if you want to get involved with stream processing etc
				var base64String = Convert.ToBase64String(bytes);
				return base64String;
			}
		}
	}
}
