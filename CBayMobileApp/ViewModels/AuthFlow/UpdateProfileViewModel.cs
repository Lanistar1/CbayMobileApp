using CBayMobileApp.Models.AuthFlow;
using CBayMobileApp.Models.Common;
using CBayMobileApp.Popup;
using CBayMobileApp.Utils;
using CBayMobileApp.Views.Identity;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CBayMobileApp.ViewModels.AuthFlow
{
    public class UpdateProfileViewModel : BaseViewModel
    {
        public UpdateProfileViewModel(INavigation navigation)
        {
            Navigation = navigation;

            UpdateProfileCommand = new Command(async () => await UpdateProfileCommandExecute());
            SelectGenderCommand = new Command(async () => await SelectGenderCommandExecute());

        }



        #region Bindings
        private string firstName;
        public string FirstName
        {
            get => firstName;
            set
            {
                firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        private string email;
        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        private string lastName;
        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }


        private string gender;
        public string Gender
        {
            get => gender;
            set
            {
                gender = value;
                OnPropertyChanged(nameof(Gender));
            }
        }

        private DateTime? dateOfBirth;
        public DateTime? DateOfBirth
        {
            get => dateOfBirth;
            set
            {
                dateOfBirth = value;
                OnPropertyChanged(nameof(DateOfBirth));
            }
        }

        private DateTime maximumDate = DateTime.Now.AddYears(-16);
        public DateTime MaximumDate
        {
            get => maximumDate;
            set
            {
                maximumDate = value;
                OnPropertyChanged(nameof(MaximumDate));
            }
        }

        private string phoneNumber;
        public string PhoneNumber
        {
            get => phoneNumber;
            set
            {
                phoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }

        Regex EmailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        public bool ValidateEmail(string Username)
        {
            if (string.IsNullOrWhiteSpace(Username))
                return false;

            return EmailRegex.IsMatch(Username);
        }

        #endregion

        #region Commands
        public Command UpdateProfileCommand { get; }
        public Command SelectGenderCommand { get; }

        #endregion


        #region functions, methods, events and Navigations

        private async Task UpdateProfileCommandExecute()
        {
            bool checker = await VerifyBioDataEntry();
            if (checker)
            {
                await UpdateProfileAsync();
            }
        }

        private async Task UpdateProfileAsync()
        {

            try
            {

                await LoadingPopup.Instance.Show("Updating User profile. Please wait...");
                UpdateProfileRequestModel request = new UpdateProfileRequestModel()
                {
                    DOB = DateOfBirth?.ToString("yyyy-MM-dd"),
                    EmailAddress = Email,
                    FirstName = FirstName,
                    Gender = Gender,
                    LastName = LastName,
                    PhoneNo = PhoneNumber
                };

                var (ResponseData, ErrorData, StatusCode) = await _cbayServices.UpdateUserProfileAsync(request);


                if (ResponseData != null)
                {
                    await MessagePopup.Instance.Show("Profile updated successfully.");

                    await Navigation.PushAsync(new LoginPage());
                }
                else if (StatusCode == 200)
                {
                    await MessagePopup.Instance.Show("Profile updated successfully.");

                    await Navigation.PushAsync(new LoginPage());
                }
                else if (ErrorData != null && StatusCode == 401)
                {
                    await MessagePopup.Instance.Show(ErrorData.errors.FirstOrDefault());
                }
                else if (ErrorData != null && StatusCode == 400)
                {
                    await MessagePopup.Instance.Show(ErrorData.errors.FirstOrDefault());
                }
                else
                {
                    await MessagePopup.Instance.Show("Profile updated successfully.");

                    await Navigation.PushAsync(new LoginPage());
                }


            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex);
                //await MessagePopup.Instance.Show("Something went wrong. Please try again later.");
            }
            finally
            {
                await LoadingPopup.Instance.Hide();
            }
        }



        private async Task<bool> VerifyBioDataEntry()
        {
            if (string.IsNullOrWhiteSpace(FirstName))
            {
                await MessagePopup.Instance.Show("Enter firstname to continue.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(Gender))
            {
                await MessagePopup.Instance.Show("select gender to continue.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(LastName))
            {
                await MessagePopup.Instance.Show("Enter lastName to continue.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(Email))
            {
                await MessagePopup.Instance.Show("Enter email to continue.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(PhoneNumber))
            {
                await MessagePopup.Instance.Show("Enter phone number to continue.");
                return false;
            }
            //if (!DateOfBirth.HasValue)
            //{
            //    await MessagePopup.Instance.Show("select customer date of birth to continue.");
            //    return false;
            //}
            else
            {
                return true;
            }
        }

        private async Task SelectGenderCommandExecute()
        {

            List<SelectItemModel> genderTypes = new List<SelectItemModel>()
            {
                new SelectItemModel("1","FEMALE"),
                new SelectItemModel("2","MALE"),
                //new SelectItemModel(3,"Others"),
            };
            var popup = new SelectItemPickerPopup(genderTypes);

            await PopupNavigation.Instance.PushAsync(popup);

            var result = await popup.PopupClosedTask;
            Gender = result.Item1;
        }

        #endregion


    }

}
