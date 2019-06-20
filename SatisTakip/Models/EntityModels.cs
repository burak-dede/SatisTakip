using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SatisTakip.Models
{

    public class ArventoSale
    {
            public ArventoSale()
        {
            CustomerState = true;

        }

        private int _id;

        [Key]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _companyName;


        [DisplayName("Şirket Adı")]
        [Required]
        public string CompanyName
        {
            get { return _companyName; }
            set { _companyName = value; }
        }
        private int _sales;

        [DisplayName("Satış Miktarı")]
        [Required]
        public int Sales
        {
            get { return _sales; }
            set { _sales = value; }
        }
        private DateTime _dateofSale;

        [DisplayName("Başlama Tarihi")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        [Required]
        public DateTime DateofSale
        {
            get { return _dateofSale; }
            set { _dateofSale = value; }
        }

        private DateTime _endOfContractDate;

        [DisplayName("Bitiş Tarihi")]
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime EndOfContractDate
        {
            get { return _endOfContractDate; }
            set
            {
                _endOfContractDate = value;
            }
        }

        private Boolean _isMobileDataOwn=false;

        [DisplayName("Mobil Hat")]
        [Required]
        [DefaultValue(false)]
        public bool IsMobileDataOwn
        {
            get { return _isMobileDataOwn; }
            set { _isMobileDataOwn = value; }
        }

        private DateTime _mobileDate;

        [DisplayName("Mobil Hat Tarihi")]
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime MobileDate
        {
            get { return _mobileDate; }
            set
            {
                _mobileDate = value;
            }
        }
        
        private int _remindingTime;

        [DisplayName("Hatırlatma Zamanı(Gün)")]
        [Required]
        [DefaultValue(7)]
        public int RemindingTime
        {
            get { return _remindingTime; }
            set { _remindingTime = value; }
        }

        [DisplayName("Fatura")]
        [DefaultValue(null)]
        public String InvoiceImagePath
        {
            get { return _InvoiceImagePath; }
            set { _InvoiceImagePath = value; }
        }
        private String _InvoiceImagePath { get; set; }

        private Boolean _CustomerState;

        [DisplayName("Durum")]
        [Required]
        [DefaultValue(true)]
        public bool CustomerState
        {
            get { return _CustomerState; }
            set { _CustomerState = value; }
        }
    }
    public class TurkcellSale
    {
        public TurkcellSale()
        {
            CustomerState = true;

        }

        private int _id;

        [Key]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _name;


        [DisplayName("Ad")]
        [Required]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private string _lastname;

        [DisplayName("Soyad")]
        [Required]
        public string Lastname
        {
            get { return _lastname; }
            set { _lastname = value; }
        }

        private string _phoneNumber;

        [DisplayName("GSM No")]
        [Required]
        [RegularExpression(@"^\(?([5][0-9]{2})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Telefon numarası formatına uygun değildir.")]
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; }
        }

        private Boolean _lineType;

        [DisplayName("Hat Türü")]
        [Required]
        public bool LineType
        {
            get { return _lineType; }
            set { _lineType = value; }
        }

        private Boolean _customerType;

        [DisplayName("Kullanıcı Tipi")]
        [Required]
        [DefaultValue(true)]
        public bool CustomerType
        {
            get { return _customerType; }
            set { _customerType = value; }
        }

        private DateTime _activationDate;

        [DisplayName("Aktivasyon Tarihi")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        [Required]
        public DateTime ActivationDate
        {
            get { return _activationDate; }
            set { _activationDate = value; }
        }

        private string _contactNumber;

        [DisplayName("İrtibat No")]
        [Required]
        [RegularExpression(@"^\(?([5][0-9]{2})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Telefon numarası formatına uygun değildir.")]
        public string ContactNumber
        {
            get { return _contactNumber; }
            set { _contactNumber = value; }
        }

        private string _note;

        [DisplayName("Not")]
        public string Note
        {
            get { return _note; }
            set { _note = value; }
        }

        private bool _state;

        [DisplayName("Durum")]
        [DefaultValue(true)]
        public bool CustomerState
        {
            get { return _state; }
            set { _state = value; }
        }

    }

    public class ListsModel
    {
        //Search results
        public int SearchResults1Page { get; set; }
        public int SearchResults2Page { get; set; }
        public int SearchResults3Page { get; set; }
        public int SearchResults4Page { get; set; }
        public IPagedList<ArventoSale> SearchResults1 { get; set; }
        public IPagedList<ArventoSale> SearchResults2 { get; set; }
        public IPagedList<ArventoSale> SearchResults3 { get; set; }
        public IPagedList<ArventoSale> SearchResults4 { get; set; }
    }
    public class TurkcellListsModel
    {
        //Search results
        public int SearchResults1Page { get; set; }
        public int SearchResults2Page { get; set; }
        public int SearchResults3Page { get; set; }
        public IPagedList<TurkcellSale> SearchResults1 { get; set; }
        public IPagedList<TurkcellSale> SearchResults2 { get; set; }
        public IPagedList<TurkcellSale> SearchResults3 { get; set; }
    }

    public class logMail
    {
        private int _id;

        [Key]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private DateTime _mailDate;

        [DisplayName("Mail Tarihi")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        [Required]
        public DateTime MailDate
        {
            get { return _mailDate; }
            set { _mailDate = value; }
        }

        private string _message;

        [DisplayName("Message")]
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

    }
}