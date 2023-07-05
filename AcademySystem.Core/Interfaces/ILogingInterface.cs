using AcademySystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademySystem.Core.Interfaces
{
    public interface ILogingInterface
    {
        Loging? GetByappUserId(string appUserId);
    }
}
