using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssignmentTwo.Models;


namespace AssignmentTwo.ViewModels
{
    public class ViewModel_Coach_Create
    {
        public Schedule Schedule { get; set; }
        public IEnumerable<Coach> Coach { get; set; }
    }
}
