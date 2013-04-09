using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GitHubTagDeployer.Helpers;
using GitHubTagDeployer.Models;

namespace GitHubTagDeployer.ViewModels
{
    public class TagDeployViewModel : BaseViewModel
    {
        private string username;
        public string Username
        {
            get { return username; }
            set
            {
                if (username != value)
                {
                    username = value;
                    RaisePropertyChanged(() => Username);                    
                }
            }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                if (password != value)
                {
                    password = value;
                    RaisePropertyChanged(() => Password);
                }
            }
        }

        private string deployUrl;
        public string DeployUrl
        {
            get { return deployUrl; }
            set
            {
                deployUrl = value;
                RaisePropertyChanged(() => DeployUrl);
            }
        }

        private string rollbackUrl;
        public string RollbackUrl
        {
            get { return rollbackUrl; }
            set
            {
                rollbackUrl = value;
                RaisePropertyChanged(() => RollbackUrl);
            }
        }

        private string deployDirectory;
        public string DeployDirectory
        {
            get { return deployDirectory; }
            set
            {
                deployDirectory = value;
                RaisePropertyChanged(() => DeployDirectory);
            }
        }

        private string backupDirectory;
        public string BackupDirectory
        {
            get { return backupDirectory; }
            set
            {
                backupDirectory = value;
                RaisePropertyChanged(() => BackupDirectory);
            }
        }

        private bool deleteExisting;
        public bool DeleteExisting
        {
            get { return deleteExisting; }
            set
            {
                deleteExisting = value;
                RaisePropertyChanged(() => DeleteExisting);
            }
        }

        private bool useProxy;
        public bool UseProxy
        {
            get { return useProxy; }
            set
            {
                useProxy = value;
                RaisePropertyChanged(() => UseProxy);
            }
        }

        public ICommand Deploy { get { return new DelegateCommand(OnDeploy); } }
        public ICommand Rollback { get { return new DelegateCommand(OnRollback); } }


        public TagDeployViewModel()
        {
        }

        private void OnDeploy()
        {               
        }

        private void OnRollback()
        {            
        }

        private void OnDoNothing()
        {
        }

        private bool CanExecuteDoNothing()
        {
            return false;
        }
    }
}