using GitHubTagDeployer.Properties;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GitHub_Tag_Deployer
{
    public partial class MainWindow : Window
    {
        private FileUtil fileUtil;
        private GitHubAPIUtil gitHubApiUtil;

        public MainWindow()
        {
            InitializeComponent();

            gitHubApiUtil = new GitHubAPIUtil();
            fileUtil = new FileUtil();
        }

        private void deployTag(String tagUrl)
        {
            if (ValidateTextBox(txtDeployDirectory.Text, true))
            {
                if (!string.IsNullOrWhiteSpace(txtBackupDirectory.Text))
                    fileUtil.BackupFolder(txtDeployDirectory.Text, txtBackupDirectory.Text);

                if (cbxDeleteExisting.IsChecked == true)
                    fileUtil.DeleteExistingFiles(txtDeployDirectory.Text);

                gitHubApiUtil.fetchTag(txtUsername.Text, txtPassword.Password, tagUrl, txtDeployDirectory.Text, cbxProxy.IsChecked);               
               
                fileUtil.unpackTag(tagUrl, txtDeployDirectory.Text);
            }
            else
            {
                txtDeployDirectory.Text = "Enter a valid Deploy Directory!";
                txtDeployDirectory.Foreground = Brushes.Red;
            }
        }

        private void btnRollback_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateTextBox(txtRollbackTagUrl.Text))
                deployTag(txtRollbackTagUrl.Text);
            else
            {
                txtRollbackTagUrl.Text = "Enter a valid Rollback URL!!";
                txtRollbackTagUrl.Foreground = Brushes.Red;
            }
        }

        private bool ValidateTextBox(string text, bool deploy = false)
        {
            if (!text.ToLower().StartsWith("https://") && !deploy)
                return false;

            if (string.IsNullOrEmpty(text))
                return false;

           return true;
        }

        private void btnDeploy_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateTextBox(txtTagUrlToDeploy.Text))
                deployTag(txtTagUrlToDeploy.Text);
            else
            {
                txtTagUrlToDeploy.Text = "Enter a valid Rollback URL!!";
                txtTagUrlToDeploy.Foreground = Brushes.Red;
            }
        }

        private void GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox txt = (TextBox)sender;

            txt.Foreground = Brushes.Black;
            txt.Text = "";
        }        
    }
}
