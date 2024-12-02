using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetBuddy.Domain.Models.Dtos
{
    public class LoginDto
    {
        public string EmailOrUserName { get; set; }
        public string Password { get; set; }
    }
}
