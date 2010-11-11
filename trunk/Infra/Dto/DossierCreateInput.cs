using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MRGSP.ASMS.Infra.Dto
{
    public class DossierCreateInput
    {
        [Req]
        [UIHint("Lookup")]
        [DisplayName("Fermier")]
        public int FarmerVersionId { get; set; }
        
        [Req]
        [DisplayName("Prenume Administrator")]
        [StringLength(20)]
        public string AdminFirstName { get; set; }

        [Req]
        [DisplayName("Nume Administrator")]
        [StringLength(20)]
        public string AdminLastName { get; set; }

        [DisplayName("Prenumele reprezentantului legal")]
        [StringLength(20)]
        public string RepresentativeFirstName { get; set; }

        [DisplayName("Numele reprezentantului legal")]
        [StringLength(20)]
        public string RepresentativeLastName { get; set; }

        [DisplayName("Telefonul unei persoane apropiate")]
        [StringLength(50)]
        public string FriendPhone { get; set; }

        [DisplayName("Pregatire profesionala")]
        public bool ProTraining { get; set; }

        [DisplayName("Denumirea specialitatii")]
        public string Speciality { get; set; }

        [DisplayName("Institutia care a eliberat diploma")]
        public string DiplomaIssuer { get; set; }

        [DisplayName("Contract de consultanta")]
        public bool HasContract { get; set; }

        [DisplayName("Numarul contractului de consultanta")]
        public string ContractNumber { get; set; }

        [DisplayName("Data inregistrarii contractului")]
        public DateTime? ContractDate { get; set; }

        [DisplayName("Prestatorul de servicii de consultanta")]
        public string ServiceProvider { get; set; }

        [DisplayName("Cine a perfectat business planul")]
        [UIHint("Dropdown")]
        public object PerfecterId { get; set; }

        [DisplayName("Masura")]
        [UIHint("Dropdown")]
        public object MeasureId { get; set; }

        [DisplayName("Valoarea Investitiei")]
        public decimal InvestmentValue { get; set; }

        [DisplayName("Suma solicitata")]
        public decimal AmountRequested { get; set; }

        [DisplayName("Raion")]
        [UIHint("Lookup")]
        public int DistrictId { get; set; }
        
        [DisplayName("Localitate")]
        [StringLength(30)]
        [Req]
        public string Locality { get; set; }
    }
}