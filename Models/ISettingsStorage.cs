using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubTagDeployer.Models
{
    interface ISettingsStorage
    {
        string Username { get; set; }
        string DeployUrl { get; set; }
        string RollbackUrl { get; set; }
        string DeployDirectory { get; set; }
        string BackupDirectory { get; set; }
        bool DeleteExisting { get; set; }
        bool UseProxy { get; set; }
    }
}
