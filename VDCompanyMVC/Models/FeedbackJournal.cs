using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

namespace VDCompanyMVC.Models
{
    public sealed class FeedbackJournal
    {
        private readonly string _storePath;

        public FeedbackJournal(IConfiguration configuration)
        {
            _storePath = configuration["Feedback:Directory"];
        }

        public bool IsConfigured => Directory.Exists(_storePath);

        public async Task<string> RecordAsync(FeedbackRequest request, string consentVersion)
        {
            var id = Guid.NewGuid().ToString("N");
            var record = new
            {
                Id = id,
                Name = request.Name.Trim(),
                Email = request.Email.Trim(),
                Subject = request.Subject.Trim(),
                Message = request.Message.Trim(),
                ConsentVersion = consentVersion,
                AcceptedAtUtc = DateTimeOffset.UtcNow
            };

            var path = Path.Combine(_storePath, id + ".json");
            var pendingPath = path + ".tmp";
            await File.WriteAllTextAsync(pendingPath, JsonSerializer.Serialize(record));
            File.Move(pendingPath, path);
            return id;
        }
    }
}
