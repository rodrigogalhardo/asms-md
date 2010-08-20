using System;
using MvcContrib.UI.InputBuilder.Attributes;

namespace MRGSP.ASMS.Infra.Dto
{
    public class DossierCreateInput
    {
        
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

        [Label("Telefonul unei persoane apropiate")]
        public string FriendPhone { get; set; }

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
        public object PerfecterId { get; set; }

        public object MeasureId { get; set; }

        [Label("Suma solicitata")]
        public decimal AmountRequested { get; set; }
    }
}