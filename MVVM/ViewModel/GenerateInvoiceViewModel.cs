using Ninance_v2.Core;
using System.Collections.ObjectModel;
using System.Windows;
using static Ninance_v2.Core.InvoiceGenerator;

namespace Ninance_v2.MVVM.ViewModel
{

    class GenerateInvoiceViewModel : ObservableObject
    {
        public class InvoiceAddressProfile
        {
            public string DisplayName
            {
                get
                {
                    return address.CompanyName == "" ? address.PersonName + ", " + address.Street + ", " + address.State + ", " + address.Postcode + " " + address.City + ", " + address.CountryName + ", " + address.Email + ", " + address.Phone : address.CompanyName + ", " + address.PersonName + ", " + address.Street + ", " + address.State + ", " + address.Postcode + " " + address.City + ", " + address.CountryName + ", " + address.Email + ", " + address.Phone;
                }
            }

            public Address address;

            public InvoiceAddressProfile(Address address)
            {
                this.address = address;
            }
        }

        private ObservableCollection<InvoiceAddressProfile> _profiles;
        public ObservableCollection<InvoiceAddressProfile> Profiles
        {
            get { return _profiles; }
            set { _profiles = value; OnPropertyChanged(); }
        }

        private Visibility _isPartOneVisible = Visibility.Visible;
        public Visibility IsPartOneVisible
        {
            get { return _isPartOneVisible; }
            set { _isPartOneVisible = value; OnPropertyChanged(); }
        }

        private Visibility _isPartTwoVisible = Visibility.Hidden;
        public Visibility IsPartTwoVisible
        {
            get { return _isPartTwoVisible; }
            set { _isPartTwoVisible = value; OnPropertyChanged(); }
        }

        public GenerateInvoiceViewModel()
        {
            Profiles = new ObservableCollection<InvoiceAddressProfile>();
        }
    }
}