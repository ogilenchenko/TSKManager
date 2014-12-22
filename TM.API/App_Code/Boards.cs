using System.Collections.Generic;
using System.Data;
using System.Linq;
using TM.Database;
using TM.Domain.BusinessObjects;
using TM.Domain.Models;
using BoardModel = TM.API.Models.BoardModel;

namespace TM.API
{
    public class Boards
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserProfile _userProfile;

        public Boards(IUnitOfWork unitOfWork, UserProfile userProfile)
        {
            _unitOfWork = unitOfWork;
            _userProfile = userProfile;
        }

        public BoardModel GetBoardById(int id)
        {
            var boardBo = new BoardBO(_unitOfWork, _userProfile);
            var result = boardBo.GetBoard(id);

            return BoardModel.Map(result);
        }

        public IEnumerable<BoardModel> GetBoards(string userId)
        {
            var boardBo = new BoardBO(_unitOfWork, _userProfile);
            var result = boardBo.GetBoards(userId);

            return result.Select(BoardModel.Map);
        }

        public int UpdateStatus(int id)
        {
            var boardBo = new BoardBO(_unitOfWork, _userProfile);
            var board = boardBo.GetBoard(id);
            if (board == null)
            {
                throw new NoNullAllowedException("board");
            }

            board.IsStarred = !board.IsStarred;
            int result = boardBo.Save(board);

            return result;
        }

        public int AddBoard(BoardModel model)
        {
            var boardBo = new BoardBO(_unitOfWork, _userProfile);
            var result = boardBo.Save(BoardModel.Map(model));

            return result;
        }
    }
}