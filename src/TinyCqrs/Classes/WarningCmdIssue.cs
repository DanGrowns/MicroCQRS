using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyCqrs.Enums;

namespace TinyCqrs.Classes
{
    public class WarningCmdIssue : CmdIssue
    {
        public WarningCmdIssue(string message) : base(message, IssueType.Warning)
        {
        }
    }
}
