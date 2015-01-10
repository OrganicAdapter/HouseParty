using System.Threading.Tasks;
using System.Web.Mvc;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Core.Contents;
using Orchard.Localization;
using Orchard.Logging;
using Orchard.Mvc;
using Orchard.Themes;
using Orchard.UI.Notify;
using System;
using Orchard.Settings;
using System.Collections.Generic;
using Orchard.Security;
using SoftIT.HouseParty.Models;
using Orchard.Data;
using System.Linq;

namespace SoftIT.HouseParty.Controllers
{
    [Themed]
    [Authorize]
    public class HousePartyUserController : Controller
    {
        private readonly IContentManager _contentManager;
        private readonly IOrchardServices _orchardServices;
        private readonly IRepository<FriendsRecord> _friendsRepository;
        private readonly IRepository<RatingsRecord> _ratingsRepository;

        public HousePartyUserController(
            IContentManager contentManager, 
            IOrchardServices orchardServices, 
            IRepository<FriendsRecord> friendsRepository, 
            IRepository<RatingsRecord> ratingsRepository)
        {
            _contentManager = contentManager;
            _orchardServices = orchardServices;
            _friendsRepository = friendsRepository;
            _ratingsRepository = ratingsRepository;
        }

        public ActionResult Index(int userId)
        {
            var user = _contentManager.Get<IUser>(userId);
            var shape = _contentManager.BuildDisplay(user);
            var canRate = _ratingsRepository.Table.FirstOrDefault(rating => rating.FromId.Equals(_orchardServices.WorkContext.CurrentUser.Id) && rating.ToId.Equals(userId)) == null;
            var viewModel = _orchardServices.New.ViewModel(
                UserDisplay: shape, 
                CurrentUser: _orchardServices.WorkContext.CurrentUser.As<HousePartyUserPart>(),
                CanRate: canRate);

            return View(viewModel);
        }

        public ActionResult Like(int userId)
        {
            if (!_orchardServices.WorkContext.CurrentUser.Id.Equals(userId) && 
                _ratingsRepository.Table.FirstOrDefault(rating => rating.FromId.Equals(_orchardServices.WorkContext.CurrentUser.Id) && rating.ToId.Equals(userId)) == null)
            {
                var user = _contentManager.Get<IUser>(userId);
                user.As<HousePartyUserPart>().Likes++;
                _ratingsRepository.Create(new RatingsRecord { FromId = _orchardServices.WorkContext.CurrentUser.Id, ToId = userId, IsLike = true });
            }

            return RedirectToAction("Index", new { userId = userId });
        }

        public ActionResult Dislike(int userId)
        {
            if (!_orchardServices.WorkContext.CurrentUser.Id.Equals(userId) &&
                _ratingsRepository.Table.FirstOrDefault(rating => rating.FromId.Equals(_orchardServices.WorkContext.CurrentUser.Id) && rating.ToId.Equals(userId)) == null)
            {
                var user = _contentManager.Get<IUser>(userId);
                user.As<HousePartyUserPart>().Dislikes++;
                _ratingsRepository.Create(new RatingsRecord { FromId = _orchardServices.WorkContext.CurrentUser.Id, ToId = userId, IsLike = false });
            }

            return RedirectToAction("Index", new { userId = userId });
        }

        public ActionResult SetFriend(int userId)
        {
            if (!_orchardServices.WorkContext.CurrentUser.Id.Equals(userId))
            {
                var friendsRecord = new FriendsRecord 
                { 
                    FriendOne = _orchardServices.WorkContext.CurrentUser.Id,  
                    FriendTwo = userId,
                    IsAccepted = false
                };

                _friendsRepository.Create(friendsRecord);
            }

            return RedirectToAction("Index", new { userId = userId });
        }

        public ActionResult Accept(int userId)
        {
            var request = _friendsRepository.Table
                .ToList()
                .FirstOrDefault(item => item.FriendOne.Equals(userId));

            request.IsAccepted = true;

            return RedirectToAction("Index", new { userId = request.FriendTwo });
        }

        public ActionResult Decline(int userId)
        {
            var request = _friendsRepository.Table
                .ToList()
                .FirstOrDefault(item => item.FriendOne.Equals(userId));

            _friendsRepository.Delete(request);

            return RedirectToAction("Index", new { userId = request.FriendTwo });
        }
    }
}