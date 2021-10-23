﻿using Ninance_v2.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using System.Xml.Serialization;
using static Ninance_v2.Core.InvoiceGenerator;

namespace Ninance_v2.MVVM.ViewModel
{

    public class Country
    {
        public string DisplayName => Name;

        public string Name;
        public string Code;

        public Country(string Name, string Code)
        {
            this.Name = Name;
            this.Code = Code;
        }
    }

    class GenerateInvoiceViewModel : ObservableObject
    {

        private List<InvoiceProfile> _profiles;
        public List<InvoiceProfile> Profiles
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

        private string _commentText;
        public string CommentText
        {
            get { return _commentText; }
            set { _commentText = value; OnPropertyChanged(); }
        }

        private string _invoiceNumberText;
        public string InvoiceNumberText
        {
            get { return _invoiceNumberText; }
            set { _invoiceNumberText = value; OnPropertyChanged(); }
        }

        private string _orderNumberText;
        public string OrderNumberText
        {
            get { return _orderNumberText; }
            set { _orderNumberText = value; OnPropertyChanged(); }
        }

        private string _issueDateText;
        public string IssueDateText
        {
            get { return _issueDateText; }
            set { _issueDateText = value; OnPropertyChanged(); }
        }

        private string _dueDateText;
        public string DueDateText
        {
            get { return _dueDateText; }
            set { _dueDateText = value; OnPropertyChanged(); }
        }

        private string _sellerFullNameText;
        public string SellerFullNameText
        {
            get { return _sellerFullNameText; }
            set { _sellerFullNameText = value; OnPropertyChanged(); }
        }

        private string _sellerCompanyNameText;
        public string SellerCompanyNameText
        {
            get { return _sellerCompanyNameText; }
            set { _sellerCompanyNameText = value; OnPropertyChanged(); }
        }

        private string _sellerStreetText;
        public string SellerStreetText
        {
            get { return _sellerStreetText; }
            set { _sellerStreetText = value; OnPropertyChanged(); }
        }

        private string _sellerPostcodeText;
        public string SellerPostcodeText
        {
            get { return _sellerPostcodeText; }
            set { _sellerPostcodeText = value; OnPropertyChanged(); }
        }

        private string _sellerStateText;
        public string SellerStateText
        {
            get { return _sellerStateText; }
            set { _sellerStateText = value; OnPropertyChanged(); }
        }

        private string _sellerCityText;
        public string SellerCityText
        {
            get { return _sellerCityText; }
            set { _sellerCityText = value; OnPropertyChanged(); }
        }

        private string _sellerEmailText;
        public string SellerEmailText
        {
            get { return _sellerEmailText; }
            set { _sellerEmailText = value; OnPropertyChanged(); }
        }

        private string _sellerPhoneNumberText;
        public string SellerPhoneNumberText
        {
            get { return _sellerPhoneNumberText; }
            set { _sellerPhoneNumberText = value; OnPropertyChanged(); }
        }

        private string _sellerVatNumberText;
        public string SellerVatNumberText
        {
            get { return _sellerVatNumberText; }
            set { _sellerVatNumberText = value; OnPropertyChanged(); }
        }

        private string _buyerFullNameText;
        public string BuyerFullNameText
        {
            get { return _buyerFullNameText; }
            set { _buyerFullNameText = value; OnPropertyChanged(); }
        }

        private string _buyerCompanyNameText;
        public string BuyerCompanyNameText
        {
            get { return _buyerCompanyNameText; }
            set { _buyerCompanyNameText = value; OnPropertyChanged(); }
        }

        private string _buyerStreetText;
        public string BuyerStreetText
        {
            get { return _buyerStreetText; }
            set { _buyerStreetText = value; OnPropertyChanged(); }
        }

        private string _buyerPostcodeText;
        public string BuyerPostcodeText
        {
            get { return _buyerPostcodeText; }
            set { _buyerPostcodeText = value; OnPropertyChanged(); }
        }

        private string _buyerStateText;
        public string BuyerStateText
        {
            get { return _buyerStateText; }
            set { _buyerStateText = value; OnPropertyChanged(); }
        }

        private string _buyerCityText;
        public string BuyerCityText
        {
            get { return _buyerCityText; }
            set { _buyerCityText = value; OnPropertyChanged(); }
        }

        private string _buyerEmailText;
        public string BuyerEmailText
        {
            get { return _buyerEmailText; }
            set { _buyerEmailText = value; OnPropertyChanged(); }
        }

        private string _buyerPhoneNumberText;
        public string BuyerPhoneNumberText
        {
            get { return _buyerPhoneNumberText; }
            set { _buyerPhoneNumberText = value; OnPropertyChanged(); }
        }

        private string _buyerVatNumberText;
        public string BuyerVatNumberText
        {
            get { return _buyerVatNumberText; }
            set { _buyerVatNumberText = value; OnPropertyChanged(); }
        }

        private int _sellerCountryIndex;
        public int SellerCountryIndex
        {
            get { return _sellerCountryIndex; }
            set { _sellerCountryIndex = value; OnPropertyChanged(); }
        }

        private int _buyerCountryIndex;
        public int BuyerCountryIndex
        {
            get { return _buyerCountryIndex; }
            set { _buyerCountryIndex = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Country> _countries;
        public ObservableCollection<Country> Countries
        {
            get { return _countries; }
            set { _countries = value; OnPropertyChanged(); }
        }

        private RelayCommand _saveBuyerProfileCommand;
        public RelayCommand SaveBuyerProfileCommand
        {
            get { return _saveBuyerProfileCommand; }
            set { _saveBuyerProfileCommand = value; OnPropertyChanged(); }
        }

        private RelayCommand _saveSellerProfileCommand;
        public RelayCommand SaveSellerProfileCommand
        {
            get { return _saveSellerProfileCommand; }
            set { _saveSellerProfileCommand = value; OnPropertyChanged(); }
        }

        private RelayCommand _continueToPartTwoCommand;
        public RelayCommand ContinueToPartTwoCommand
        {
            get { return _continueToPartTwoCommand; }
            set { _continueToPartTwoCommand = value; OnPropertyChanged(); }
        }

        private RelayCommand _backToPreviousPartCommand;
        public RelayCommand BackToPreviousPartCommand
        {
            get { return _backToPreviousPartCommand; }
            set { _backToPreviousPartCommand = value; OnPropertyChanged(); }
        }

        public GenerateInvoiceViewModel()
        {
            Profiles = App.InvoiceProfileHandler.ListProfiles();
            Countries = new ObservableCollection<Country>();

            SaveSellerProfileCommand = new RelayCommand(o => { Address address = GetAddress(0); if (!address.IsValid) return; SaveProfile(address); });
            SaveBuyerProfileCommand = new RelayCommand(o => { Address address = GetAddress(1); if (!address.IsValid) return; SaveProfile(address); });
            ContinueToPartTwoCommand = new RelayCommand(o => { IsPartOneVisible = Visibility.Hidden; IsPartTwoVisible = Visibility.Visible; });

            var xmlDocument = XDocument.Load("Data/countries.xml");
            var xmlCountries = xmlDocument.Element("countries").Elements();

            foreach(var child in xmlCountries)
                Countries.Add(new Country(child.Value, child.Attributes().ElementAt(0).Value));

            App.InvoiceProfileHandler.SaveProfile(InvoiceDocumentDataSource.GenerateRandomAddress(), "Test");
        }

        private Address GetAddress(int type)
        {
            return type == 0 ? new Address() { CountryName = Countries[SellerCountryIndex].Name, CountryCode = Countries[SellerCountryIndex].Code, CompanyName = SellerCompanyNameText, City = SellerCityText, Email = SellerEmailText, PersonName = SellerFullNameText, Phone = SellerPhoneNumberText, Postcode = SellerPostcodeText, State = SellerStateText, Street = SellerStreetText }
            : new Address() { CountryName = Countries[BuyerCountryIndex].Name, CountryCode = Countries[BuyerCountryIndex].Code, CompanyName = BuyerCompanyNameText, City = BuyerCityText, Email = BuyerEmailText, PersonName = BuyerFullNameText, Phone = BuyerPhoneNumberText, Postcode = BuyerPostcodeText, State = BuyerStateText, Street = BuyerStreetText };
        }

        private void SaveProfile(Address address)
        {
            
        }
    }
}