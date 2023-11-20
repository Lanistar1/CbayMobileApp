using CBayMobileApp.Models.Membership;
using CBayMobileApp.Popup;
using CBayMobileApp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CBayMobileApp.ViewModels.Membership
{

    public class GetMemberDownlineViewModel : BaseViewModel
    {
        public GetMemberDownlineViewModel(INavigation navigation)
        {
            Navigation = navigation;

            Task _tsk = FetchUserMembership();


        }


        #region Bindings

        private List<DownlineData> membershipData;
        public List<DownlineData> MembershipData
        {
            get => membershipData;
            set
            {
                membershipData = value;
                OnPropertyChanged(nameof(MembershipData));
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

        private string pimage;
        public string PImage
        {
            get => pimage;
            set
            {
                pimage = value;
                OnPropertyChanged(nameof(PImage));
            }
        }
        #endregion

        #region Commands
        #endregion


        #region functions, methods, events and Navigations
        private async Task FetchUserMembership()
        {
            try
            {
                await LoadingPopup.Instance.Show("Loading Membership downline...");

                var (ResponseData, ErrorData, StatusCode) = await _cbayServices.GetMembershipAsync();
                if (ResponseData != null)
                {
                    if (ResponseData.data != null)
                    {
                        MembershipData = ResponseData.data.downlines;


                    }
                    else
                    {
                        //await MessagePopup.Instance.Show(ErrorData.errors.FirstOrDefault());
                    }
                }
                else if (ErrorData != null)
                {
                    string message = "Error fetching user detail. Do you want to RETRY?";
                    await MessagePopup.Instance.Show(
                        message: message);

                }
                else
                {
                    await MessagePopup.Instance.Show(ErrorData.errors.FirstOrDefault());
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

        #endregion


    }

}
