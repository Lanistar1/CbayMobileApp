using CBayMobileApp.Models.Membership;
using CBayMobileApp.Popup;
using CBayMobileApp.Utils;
using CBayMobileApp.Views.Account;
using CBayMobileApp.Views.Account.UserDownline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CBayMobileApp.ViewModels.Membership
{
    public class GetMembershipTeamDownlineViewModel : BaseViewModel
    {
        public GetMembershipTeamDownlineViewModel(INavigation navigation, List<DownlineData> selectedItems)
        {
            Navigation = navigation;

            SelectedMember = selectedItems;
            MemberID = SelectedMember.FirstOrDefault().memberID;

            Task _tsk = FetchUserMembership(MemberID);

            TappedCommand = new Command<DownlineData>(async (model) => await GetTappedExecute(model));

        }


        #region Bindings

        private List<DownlineData> SelectedItems = new List<DownlineData>();

        private List<DownlineData> selectedMember;
        public List<DownlineData> SelectedMember
        {
            get => selectedMember;
            set
            {
                selectedMember = value;
                OnPropertyChanged(nameof(SelectedMember));
            }
        }

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

        private string memberID;
        public string MemberID
        {
            get => memberID;
            set
            {
                memberID = value;
                OnPropertyChanged(nameof(MemberID));
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

        #endregion

        #region Commands
        public Command TappedCommand { get; }

        #endregion


        #region functions, methods, events and Navigations

        private async Task GetTappedExecute(DownlineData model)
        {
            try
            {
                var mod = model;

                model.isSelected = model.isSelected ? false : true;
                if (SelectedItems.Count > 0)
                {
                    SelectedItems.Clear();
                }
                SelectedItems.Add(model);

                await Navigation.PushAsync(new ThirdDownline(SelectedItems), true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }


        private async Task FetchUserMembership(string MemberID)
        {
            try
            {
                await LoadingPopup.Instance.Show("Loading Membership downline...");

                var (ResponseData, ErrorData, StatusCode) = await _cbayServices.GetMembershipDownlineAsync(MemberID);
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
