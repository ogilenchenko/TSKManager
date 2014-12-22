using System.Collections.Generic;
using TM.API.Models;

namespace TM.API.ViewModels
{
    public class BoardsViewModel
    {
        public IEnumerable<BoardModel> Boards { get; set; }
        public string UserId { get; set; }
    }
}