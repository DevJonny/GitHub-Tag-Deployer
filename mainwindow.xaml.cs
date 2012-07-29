using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GitHub_Tag_Deployer {

    public partial class MainWindow : Window {

        private FileUtil fileUtil;
        private GitHubAPIUtil gitHubApiUtil;

        public MainWindow() {
            InitializeComponent();

            gitHubApiUtil = new GitHubAPIUtil();
            fileUtil = new FileUtil();
        }

        private void deploy_click(object sender, RoutedEventArgs e) {
            deployTag(tagUrlToDeploy.Text);
        }

        private void rollback_Click(object sender, RoutedEventArgs e) {
            deployTag(rollbackTagUrl.Text);
        }

        private void deployTag(String tagUrl) {
            gitHubApiUtil.fetchTag(username.Text, password.Password, tagUrl, deployDirectory.Text);
            fileUtil.unpackTag(tagUrl, deployDirectory.Text);
        }
    }
}
