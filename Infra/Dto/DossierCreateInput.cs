using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MRGSP.ASMS.Infra.Dto
{
    public class DossierCreateInput
    {
        [Req]
        public int FarmerVersionId { get; set; }

        public string DisplayFarmerVersion { get; set; }
        
        [Req]
        [DisplayName("Prenume Administrator")]
        public string AdminFirstName { get; set; }

        [Req]
        [DisplayName("Nume Administrator")]
        public string AdminLastName { get; set; }

        [DisplayName("Prenumele reprezentantului legal")]
        public string RepresentativeFirstName { get; set; }

        [DisplayName("Numele reprezentantului legal")]
        public string RepresentativeLastName { get; set; }

        [DisplayName("Telefonul unei persoane apropiate")]
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
        [UIHint("Lookup")]
        public object PerfecterId { get; set; }

        [DisplayName("Masura")]
        [UIHint("Lookup")]
        public object MeasureId { get; set; }

        [DisplayName("Suma solicitata")]
        public decimal AmountRequested { get; set; }
    }
}