using Kledex.Commands;

namespace Scorecard.Applicatioin.Command
{
    public class BaseCommand : ICommand
    {
        public string? UserId { get; set; }
        public Guid? Identity { get; set; }
        public string? Source { get; set; }
        public DateTime TimeStamp { get; set; }
        public bool? ValidateCommand { get; set; }
        public bool? PublishEvents { get; set; }
    }
}
