using System;
using MvcContrib.UI.InputBuilder.Attributes;

namespace MRGSP.ASMS.Infra.Dto
{
    public class DossierCreateInput
    {
        
        [Req]
        [Label("Genul de activitate")]
        [Example("Primul indicat in statut")]
        public string ActivityType { get; set; }
        
        [Label("Zona geografica")]
        public object AreaId { get; set; }

        [Label("Raion")]
        public object DistrictId { get; set; }

        [Req]
        [Label("Localitatea")]
        public string County { get; set; }

      
        [Req]
        [Label("Cont de decontare")]
        [Example("Trebuie de indicat contul de decontare in MDL")]
        public string SettlementAccount { get; set; }

        [Req]
        [Label("Prenume Administrator")]
        [Example("ex: Vasile")]
        public string AdminFirstName { get; set; }

        [Req]
        [Label("Nume Administrator")]
        [Example("ex: Popescu")]
        public string AdminLastName { get; set; }


        [Label("Prenumele reprezentantului legal")]
        public string RepresentativeFirstName { get; set; }

        [Label("Numele reprezentantului legal")]
        public string RepresentativeLastName { get; set; }

        [Req]
        [Label("Telefon fix")]
        public string Phone { get; set; }

        public string Fax { get; set; }

        [Label("Telefon mobil")]
        public string Mobile { get; set; }

        [Label("Telefonul unei persoane apropiate")]
        public string FriendPhone { get; set; }

        [Label("Adresa e-mail")]
        public string Email { get; set; }

        [Label("Pregatire profesionala")]
        public bool ProTraining { get; set; }

        [Label("Denumirea specialitatii")]
        [Example("Conform dimplomei")]
        public string Speciality { get; set; }

        [Label("Institutia care a eliberat diploma")]
        public string DiplomaIssuer { get; set; }

        [Label("Contract de consultanta")]
        public bool HasContract { get; set; }

        [Label("Numarul contractului de consultanta")]
        public string ContractNumber { get; set; }

        [PartialView("DateTimen")]
        [Label("Data inregistrarii contractului")]
        public DateTime? ContractDate { get; set; }

        [Label("Prestatorul de servicii de consultanta")]
        public string ServiceProvider { get; set; }

        [Label("Cine a perfectat business planul")]
        public object ConsultantId { get; set; }
    }
}