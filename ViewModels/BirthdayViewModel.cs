using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using WPFProject.Annotations;
using WPFProject.Tools;
using WPFProject.Tools.Managers;
using WPFProject.Tools.MVVM;

namespace WPFProject.ViewModels
{
    internal partial class BirthdayViewModel : BaseViewModel
    {
        #region Fields

        private DateTime? _birthday = null;
        private String _age;
        private String _zodiacSign;
        private String _chineseZodiacSign;

        private RelayCommand<object> _processBirthdayCommand;

        #endregion

        #region Properties

        public DateTime? Birthday
        {
            set
            {
                _birthday = value;
                OnPropertyChanged();
            }
            get => _birthday;
        }

        public String Age
        {
            get => _age;
            set
            {
                _age = value;
                OnPropertyChanged();
            }
        }

        public String ZodiacSign
        {
            get { return _zodiacSign ?? ""; }
            set
            {
                _zodiacSign = value;
                OnPropertyChanged();
            }
        }

        public String ChineseZodiacSign
        {
            get { return _chineseZodiacSign ?? ""; }
            set
            {
                _chineseZodiacSign = value;
                OnPropertyChanged();
            }
        }

        #endregion

        private String GetZodiac(DateTime date)
        {
            int month = date.Month;
            var day = date.Day;
            if (month == 12)
            {
                return day < 22 ? "Sagittarius" : "Capricorn";
            }

            else if (month == 1)
            {
                return day < 20 ? "Capricorn" : "Aquarius";
            }

            else if (month == 2)
            {
                return day < 19 ? "Aquarius" : "Pisces";
            }

            else if (month == 3)
            {
                return day < 21 ? "Pisces" : "Aries";
            }
            else if (month == 4)
            {
                return day < 20 ? "Aries" : "Taurus";
            }

            else if (month == 5)
            {
                return day < 21 ? "Taurus" : "Gemini";
            }

            else if (month == 6)
            {
                return day < 21 ? "Gemini" : "Cancer";
            }

            else if (month == 7)
            {
                return day < 23 ? "Cancer" : "Leo";
            }

            else if (month == 8)
            {
                return day < 23 ? "Leo" : "Virgo";
            }

            else if (month == 9)
            {
                return day < 23 ? "Virgo" : "Libra";
            }

            else if (month == 10)
            {
                return day < 23 ? "Libra" : "Scorpio";
            }

            else if (month == 11)
            {
                return day < 22 ? "Scorpio" : "Sagittarius";
            }
            else
            {
                MessageBox.Show("Incorrect date of birth!");
                return "";
            }
        }

        public string GetChineseZodiac(DateTime date)
        {
            double remainder = date.Year / 12.0;
            remainder %= (int) remainder;
            int sign = (int) Math.Round(remainder * 12);

            String[] years =
            {
                "Monkey", "Rooster", "Dog", "Pig", "Rat", "Ox", "Tiger", "Rabbit", "Dragon", "Snake", "Horse", "Sheep",
            };

            return years[sign];
        }

        public RelayCommand<object> ProcessBirthdayCommand
        {
            get
            {
                return _processBirthdayCommand ??= new RelayCommand<object>(
                    SignInImplementation, o => CanExecuteCommand());
            }
        }

        private bool CanExecuteCommand()
        {
            return Birthday != null;
        }

        private async void SignInImplementation(object obj)
        {
            LoaderManager.Instance.ShowLoader();
            await Task.Run(() =>
            {
                if (Birthday > DateTime.Today)
                {
                    MessageBox.Show("I don't think, Time machine had been invented...", "Error", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    Age = "";
                    ZodiacSign = "";
                    ChineseZodiacSign = "";
                    return;
                }
                else if (Birthday != null)
                {
                    var today = DateTime.Today;
                    var age = (today.Year - Birthday.Value.Year);
                    // Go back to the year the person was born in case of a leap year
                    if (_birthday?.Date > today.AddYears(-age)) age--;
                    Age = age.ToString();
                    if (Birthday?.Day == DateTime.Today.Day && Birthday?.Month == DateTime.Today.Month)
                        MessageBox.Show("Happy Birthday, my dear friend!🎊🎇🎇🎉🎉🎁🎁");

                    ZodiacSign = GetZodiac(Birthday.Value);
                    ChineseZodiacSign = GetChineseZodiac(Birthday.Value);
                }
                else
                {
                    MessageBox.Show("Invalid birthday!", "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
            LoaderManager.Instance.HideLoader();
            // todo switch to main window
        }
    }
}