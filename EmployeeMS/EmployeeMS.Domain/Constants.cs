using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeMS.Domain
{
    public static class Constants
    {
        public static class EmployeeFileTypeName
        {
            public const string profile = "profile_picture";
            public const string id = "id_picture";
            public const string attachment = "attachment";

        }
        public static class ContractStatus
        {
            public const string active = "Active";
            public const string terminated = "Terminated";
            public const string on_leave = "On Leave";

        }
        public static class EmployeeContractType
        {
            public const string full_time = "Full-time";
            public const string part_time = "Part-time";
            public const string temp = "Temporary ";
            public const string contract = "Contract";
            public const string internship = "Internship";
            public const string freelance = "Freelance / Consultant";

        }
    }
}
