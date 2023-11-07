using EasyCaptcha.Wpf;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Demo_var_6
{
    internal class LoginViewModel : PropertyChangedBase
    {
        private string? _userLogin;
        private string? _password;
        private string? _buttonContent;
        private string? _captchaInput;
        private ObservableCollection<Captcha> captchas;
        public ObservableCollection<Captcha> CaptchaHardCode
        {
            get => captchas;
            set
            {
                captchas = value;
                OnPropertyChanged(nameof(CaptchaHardCode));
            }
        }
        private Visibility _captchaVisibility;
        public Visibility CaptchaVisibility
        {
            get => _captchaVisibility;
            set
            {
                _captchaVisibility = value;
                OnPropertyChanged(nameof(CaptchaVisibility));
            }
        }
        public string CaptchaInput
        {
            get => _captchaInput;
            set
            {
                _captchaInput = value;
                OnPropertyChanged(nameof(CaptchaInput));
            }
        }
        public string? ButtonContent
        {
            get => _buttonContent;
            set
            {
                _buttonContent = value;
                OnPropertyChanged(nameof(ButtonContent));
            }
        }
        public string? UserLogin
        { get => _userLogin;
            set
            {
                if (!object.Equals(value, _userLogin))
                {
                    _userLogin = value;
                    OnPropertyChanged(nameof(UserLogin)); 
                } 
            } 
        }public string? Password
        { get => _password;
            set
            {
                if (!object.Equals(value, _password))
                {
                    _password = value;
                    OnPropertyChanged(nameof(Password)); 
                } 
            } 
        }
        private int loginTryCount;
        private LoginModel loginModel;
        public LoginViewModel()
        {
            _password = "";
            _userLogin = "";
            ButtonContent = "Войти";
            loginTryCount = 0;
            CaptchaVisibility = Visibility.Collapsed;
            CaptchaHardCode = new()
            {
                new Captcha()
            };
            loginModel = new();
        }
        public ICommand TryToLogin {
            get => new CommonCommand<object>(
                (sender) =>
                {
                    {
                        User suspectedUser = loginModel.Login(UserLogin, Password);
                        if (suspectedUser == null)
                        {
                            CaptchaVisibility = Visibility.Visible;
                            CaptchaHardCode[0].CreateCaptcha(Captcha.LetterOption.Alphanumeric, 5);
                            MessageBox.Show("Неверный логин и/или пароль");
                            Task.Run(async () => { await WaitForSeconds(10); });
                        }
                        else
                        {
                            if (CaptchaVisibility == Visibility.Visible)
                            {
                                if (CaptchaInput == CaptchaHardCode[0].CaptchaText)
                                {
                                    Login(sender, suspectedUser);
                                }
                                else
                                {
                                    MessageBox.Show("Вы ввели неверную captcha");
                                    CaptchaHardCode[0].CreateCaptcha(Captcha.LetterOption.Alphanumeric, 5);
                                    CaptchaInput = string.Empty;
                                }
                            }
                            else
                            {
                                Login(sender, suspectedUser);
                            }
                        }
                    }
                }
                );
        }

        private void Login(object sender, User suspectedUser)
        {
            ApplicationContext.SetUser(suspectedUser);
            ProductsPageView pr = new ProductsPageView();
            pr.Show();
            (sender as Window).Close();
        }

        private async Task WaitForSeconds(int seconds)
        {
            for(int i = seconds; i > 0; i--)
            {
                ButtonContent = $"Войти ({i})";
                await Task.Delay(1000);
            }
            ButtonContent = "Войти";
        }
    }
}
