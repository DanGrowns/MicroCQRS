using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyCqrs.Enums;

namespace TinyCqrs.Classes
{
    public class ErrorCmdIssue : CmdIssue
    {
        public ErrorCmdIssue(string message) : base(message, IssueType.Error)
        {
        }
    }
}
