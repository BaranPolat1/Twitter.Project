using FinalProject.Associate.VM;
using FinalProject.Business.Services.Abstract;
using FinalProject.Business.UnitOfWork.Abstraction;
using FinalProject.Entities.Entity;


namespace FinalProject.Business.Services.Concrete
{
    public class FollowService : IFollowService
    {
        private IUnitOfWork _uow;
        public FollowService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public JsonFollowVM Follow(string Id, string userName)
        {
            JsonFollowVM js = new JsonFollowVM();
            var user = _uow.User.Find(x=>x.Id == Id);
            var user2 =  _uow.User.Find(x=>x.UserName == userName);
            if (_uow.Follow.Any(x => x.FollowerId == user2.Id && x.FollowedId == user.Id))
            {
                Follow follow = _uow.Follow.Find(x => x.FollowerId == user2.Id && x.FollowedId == user.Id);
                _uow.Follow.Delete(follow);
                _uow.SaveChange();
                js.message = "Follow";
                js.follow = _uow.Follow.FindByList(x => x.FollowedId == Id).Count;
                return js;
            }
            else
            {
                Follow follow = new Follow();
                follow.FollowedId = user.Id;
                follow.FollowerId = user2.Id;
                _uow.Follow.Add(follow);
                _uow.SaveChange();
                js.message = "UnFollow";
                js.follow = _uow.Follow.FindByList(x => x.FollowedId == Id).Count;
                return js;
            }
        }
    }
}
