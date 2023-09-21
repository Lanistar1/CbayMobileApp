using CBayMobileApp.Helpers;
using CBayMobileApp.Models.AuthFlow;
using CBayMobileApp.Models.Membership;
using CBayMobileApp.Popup;
using CBayMobileApp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CBayMobileApp.ViewModels.Membership
{

    public class GetMembershipDetailViewModel : BaseViewModel
    {
        public GetMembershipDetailViewModel(INavigation navigation)
        {
            Navigation = navigation;

            Task _tsk = FetchUserMembership();

            Task _tsks = FetchUserProfile();

            //NextCommand = new Command(async () => await NextCommandExecute());

        }


        #region Bindings
        private List<Downline> membershipDetailData;
        public List<Downline> MembershipDetailData
        {
            get => membershipDetailData;
            set
            {
                membershipDetailData = value;
                OnPropertyChanged(nameof(MembershipDetailData));
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
        
        private string joinedDate;
        public string JoinedDate
        {
            get => joinedDate;
            set
            {
                joinedDate = value;
                OnPropertyChanged(nameof(JoinedDate));
            }
        }


        #endregion

        #region Commands
        #endregion


        #region functions, methods, events and Navigations

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

                        Name = ResponseData.data.name;

                    }
                    else
                    {
                        await MessagePopup.Instance.Show(ErrorData.errors.FirstOrDefault());
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



        private async Task FetchUserMembership()
        {
            try
            {
                await LoadingPopup.Instance.Show("Loading memberships detail...");

                var (ResponseData, ErrorData, StatusCode) = await _cbayServices.GetMembershipDetailAsync();
                if (ResponseData != null)
                {
                    if (ResponseData.downlines != null)
                    {
                        MembershipDetailData = ResponseData.downlines;

                        JoinedDate = ResponseData.joinedDate;

                        MemberID = ResponseData.memberID;
                        Global.memberID = MemberID;
                    }
                    else
                    {
                        await MessagePopup.Instance.Show(ErrorData.errors.FirstOrDefault());
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
