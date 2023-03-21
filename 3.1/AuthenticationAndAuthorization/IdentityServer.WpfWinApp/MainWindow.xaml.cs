using IdentityModel.OidcClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
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

namespace IdentityServer.WpfWinApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private OidcClient _oidcClient = null;

        private string _accessToken = string.Empty;
        private string _idToken = string.Empty;
        private ClaimsPrincipal _user;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Auth_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (_user != null && _user.Identity.IsAuthenticated)
            {
                Message.Text = "Already Authenticated";
                await Task.Delay(1000);
                Message.Text = $"Hello {_user.Identity.Name}";
                return;
            }

            Message.Text = "Authenticating...";

            var options = new OidcClientOptions()
            {
                Authority = "https://localhost:44378/",
                ClientId = "client_id_win_wpf",
                Scope = "openid profile ApiOne",
                RedirectUri = "http://localhost/sample-wpf-app",
                Browser = new WpfEmbeddedBrowser(),
                LoadProfile = true
            };

            _oidcClient = new OidcClient(options);

            LoginResult result;
            try
            {
                result = await _oidcClient.LoginAsync();
            }
            catch (Exception ex)
            {
                Message.Text = $"Unexpected Error: {ex.Message}";
                return;
            }

            if (result.IsError)
            {
                Message.Text = result.Error == "UserCancel" ? "The sign-in window was closed before authorization was completed." : result.Error;
            }
            else
            {
                Message.Text = "Authenticated Successfully";
                _accessToken = result.AccessToken;
                _idToken = result.IdentityToken;
                _user = result.User;
                await Task.Delay(1000);
                var name = result.User.Identity.Name;
                Message.Text = $"Hello {name}";
            }
        }

        private async void APICall_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_accessToken))
            {
                Message.Text = "!! Authenticate First !!";
                return;
            }
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            var res = await client.GetAsync("https://localhost:44300/secret");
            Message.Text = await res.Content.ReadAsStringAsync();
        }
    }
}
