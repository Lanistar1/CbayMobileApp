using CBayMobileApp.Helpers;
using CBayMobileApp.Models.AuthFlow;
using CBayMobileApp.Popup;
using CBayMobileApp.Utils;
using CBayMobileApp.Views;
using CBayMobileApp.Views.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CBayMobileApp.ViewModels.AuthFlow
{
    public class EditProfileViewModel : BaseViewModel
    {
        public EditProfileViewModel(INavigation navigation)
        {
            Navigation = navigation;

            Task _tsk = FetchUserProfile();

            UpdateProfileCommand = new Command(async () => await UpdateProfileCommandExecute());

        }


        #region Bindings

        private ProfileData profileData;
        public ProfileData ProfileData
        {
            get => profileData;
            set
            {
                profileData = value;
                OnPropertyChanged(nameof(ProfileData));
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

        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

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

        private double height;
        public double Height
        {
            get => height;
            set
            {
                height = value;
                OnPropertyChanged(nameof(Height));
            }
        }

        private string dOB;
        public string DOB
        {
            get => dOB;
            set
            {
                dOB = value;
                OnPropertyChanged(nameof(DOB));
            }
        }

        private string country;
        public string Country
        {
            get => country;
            set
            {
                country = value;
                OnPropertyChanged(nameof(Country));
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
        #endregion

        #region Commands
        public Command UpdateProfileCommand { get; }
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

        private async Task FetchUserProfile()
        {
            try
            {
                await LoadingPopup.Instance.Show("Loading Profile detail...");

                var (ResponseData, ErrorData, StatusCode) = await _cbayServices.GetUserProfileAsync();
                if (ResponseData != null)
                {
                    if (ResponseData.data != null)
                    {
                        ProfileData = ResponseData.data;

                        Global.UserProfileData = ProfileData;

                        Name = ProfileData.name;
                        LastName = ProfileData.lastName;
                        FirstName = ProfileData.firstName;
                        PhoneNumber = ProfileData.phoneNo;
                        Email = ProfileData.emailAddress;
                        Gender = ProfileData.Gender;
                        DOB = ProfileData.DOB;
                    }
                    else
                    {
                        //await MessagePopup.Instance.Show(ErrorData.errors.FirstOrDefault());
                    }
                }
                else if (ErrorData != null)
                {
                    //string message = "Error fetching user detail. Do you want to RETRY?";
                    //await MessagePopup.Instance.Show(
                    //    message: message);

                }
                else
                {
                    //await MessagePopup.Instance.Show(ErrorData.errors.FirstOrDefault());
                }
            }
            catch (Exception ex)
            {
                string message = "Error fetching user detail. Do you want to RETRY?";
                await MessagePopup.Instance.Show(
                    message: message);
                Console.WriteLine(ex);
            }
            finally
            {
                await LoadingPopup.Instance.Hide();
            }
        }

        // update profile call

        private async Task UpdateProfileAsync()
        {

            try
            {
                DateOfBirth = Global.DateType;
                Gender = Global.GenderType;

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

                    Application.Current.MainPage = new NavigationPage(new Tabbed());
                }
                else if (StatusCode == 200)
                {
                    await MessagePopup.Instance.Show("Profile updated successfully.");

                    Application.Current.MainPage = new NavigationPage(new Tabbed());
                }
                else if (ErrorData != null && StatusCode == 401)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                else if (ErrorData != null && StatusCode == 400)
                {
                    await MessagePopup.Instance.Show(ErrorData.errors.FirstOrDefault());
                }
                else
                {
                    await MessagePopup.Instance.Show("Profile updated successfully.");

                    Application.Current.MainPage = new NavigationPage(new Tabbed());
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
            //if (string.IsNullOrWhiteSpace(Gender))
            //{
            //    await MessagePopup.Instance.Show("select gender to continue.");
            //    return false;
            //}
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
            else
            {
                return true;
            }
        }


        #endregion


    }

}
