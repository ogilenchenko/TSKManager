using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TM.Database;
using TM.Database.Models;
using TM.Domain.Models;

namespace TM.Domain.BusinessObjects
{
    public class ListBO : BaseBO
    {
        public ListBO(IUnitOfWork uow, UserProfile userProfile)
            : base(uow, userProfile)
        {

        }
        
        public ListModel GetList(int id)
        {
            var list = UnitOfWork.ListRepository.GetById(id);
            return Map(list);
        }

        public IEnumerable<ListModel> GetLists(int boardId)
        {
            var lists = UnitOfWork.ListRepository.Find(x => x.BoardId == boardId).OrderBy(x => x.Position);
            return lists.Select(Map);
        }

        public int Save(ListModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            var board = Map(model);
            var result = UnitOfWork.Commit();

            return result;
        }

        public List<ListModel> DefaultLists(int boardId)
        {
            return new List<ListModel>
            {
                new ListModel
                {
                    BoardId = boardId,
                    Name = "To Do",
                    Position = 1
                },
                new ListModel
                {
                    BoardId = boardId,
                    Name = "Doing",
                    Position = 2
                },
                new ListModel
                {
                    BoardId = boardId,
                    Name = "Done",
                    Position = 3
                }
            };
        }

        #region Map

        public ListModel Map(List list)
        {
            if (list == null)
                return null;

            var cardBO = new CardBO(UnitOfWork, UserProfile);

            return new ListModel
            {
                Id = list.Id,
                BoardId = list.BoardId,
                Name = list.Name,
                Position = list.Position,
                IsDeleted = list.IsDeleted,
                LastModifiedDate = list.LastModifiedDate,
                LastModifiedBy = list.LastModifiedBy,
                CreatedBy = list.CreatedBy,
                CreatedDate = list.CreatedDate,
                Cards = list.Cards !=null ?list.Cards.Select(cardBO.Map): new List<CardModel>()
            };
        }

        public List Map(ListModel model)
        {
            if (model == null)
                return null;

            List list;
            if (model.Id == 0)
            {
                list = new List
                {
                    CreatedBy = UserProfile.UserId,
                    CreatedDate = DateTime.UtcNow
                };

                UnitOfWork.ListRepository.Add(list);
            }
            else
            {
                list = UnitOfWork.ListRepository.GetById(model.Id);
                if (list == null)
                    throw new NoNullAllowedException("board");

                list.LastModifiedBy = UserProfile.UserId;
                list.LastModifiedDate = DateTime.UtcNow;
            }

            list.BoardId = model.BoardId;
            list.Name = model.Name;
            list.Position = model.Position;
            list.IsDeleted = false;

            return list;
        }

        #endregion
    }
}