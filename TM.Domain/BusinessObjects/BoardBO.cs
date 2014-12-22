using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TM.Database;
using TM.Database.Models;
using TM.Domain.Models;

namespace TM.Domain.BusinessObjects
{
    public class BoardBO : BaseBO
    {
        public BoardBO(IUnitOfWork uow, UserProfile userProfile)
            : base(uow, userProfile)
        {

        }

        public BoardModel GetBoard(int id)
        {
            var board = UnitOfWork.BoardRepository.GetById(id);
            return Map(board);
        }

        public BoardModel GetBoard(string userId)
        {
            var board = UnitOfWork.BoardRepository.FirstOrDefault(x => x.UserId.Equals(userId));
            return Map(board);
        }

        public IEnumerable<BoardModel> GetBoards(string userId)
        {
            var boards = UnitOfWork.BoardRepository.Find(x => x.UserId.Equals(userId)).OrderBy(x => x.Position);
            return boards.Select(Map);
        }

        public int Save(BoardModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            var board = Map(model);
            var result = UnitOfWork.Commit();

            return result;
        }

        #region Map

        public BoardModel Map(Board board)
        {
            if (board == null)
                return null;

            var listBO = new ListBO(UnitOfWork, UserProfile);

            return new BoardModel
            {
                Id = board.Id,
                UserId = board.UserId,
                Name = board.Name,
                Position = board.Position,
                IsStarred = board.IsStarred,
                IsDeleted = board.IsDeleted,
                LastModifiedDate = board.LastModifiedDate,
                LastModifiedBy = board.LastModifiedBy,
                CreatedBy = board.CreatedBy,
                CreatedDate = board.CreatedDate,
                Lists = board.Lists.Select(listBO.Map)
            };
        }

        public Board Map(BoardModel model)
        {
            if (model == null)
                return null;

            Board board;
            if (model.Id == 0)
            {
                board = new Board
                {
                    CreatedBy = UserProfile.UserId,
                    CreatedDate = DateTime.UtcNow
                };
                var listBo = new ListBO(UnitOfWork, UserProfile);
                var defaultLists = listBo.DefaultLists(board.Id);
                defaultLists.ForEach(x => listBo.Map(x));

                UnitOfWork.BoardRepository.Add(board);
            }
            else
            {
                board = UnitOfWork.BoardRepository.GetById(model.Id);
                if (board == null)
                    throw new NoNullAllowedException("board");

                board.LastModifiedBy = UserProfile.UserId;
                board.LastModifiedDate = DateTime.UtcNow;
            }
            
            board.UserId = model.UserId;
            board.Name = model.Name;
            board.Position = model.Position;
            board.IsStarred = model.IsStarred;
            board.IsDeleted = false;

            return board;
        }

        #endregion
    }
}