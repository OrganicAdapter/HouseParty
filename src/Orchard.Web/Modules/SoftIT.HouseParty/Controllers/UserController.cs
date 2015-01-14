using Orchard;
using Orchard.ContentManagement;
using Orchard.Core.Common.Models;
using Orchard.Data;
using Orchard.Localization;
using Orchard.Mvc;
using Orchard.Themes;
using Orchard.UI.Notify;
using SoftIT.HouseParty.Constants;
using SoftIT.HouseParty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoftIT.HouseParty.Controllers
{
    [Themed]
    [Authorize]
    public class UserController : Controller, IUpdateModel
    {
        private readonly IOrchardServices _orchardServices;
        private readonly IContentManager _contentManager;
        private readonly ITransactionManager _transactionManager;
        private readonly INotifier _notifier;
        private readonly IRepository<FriendRequestRecord> _friendRequestRepository;
        private readonly IRepository<FriendRecord> _friendRepository;

        private Localizer T { get; set; }

        public UserController(
            IOrchardServices orchardServices, 
            IContentManager contentManager, 
            ITransactionManager transactionManager, 
            INotifier notifier,
            IRepository<FriendRequestRecord> friendRequestRepository,
            IRepository<FriendRecord> friendRepository)
        {
            _orchardServices = orchardServices;
            _contentManager = contentManager;
            _transactionManager = transactionManager;
            _notifier = notifier;
            _friendRequestRepository = friendRequestRepository;
            _friendRepository = friendRepository;

            T = NullLocalizer.Instance;
        }

        public ActionResult UserDashboard(int id)
        {
            var item = _contentManager.Get(id);
            if (item == null) return new HttpNotFoundResult();

            return UserDashboardShapeResult(item);
        }

        [HttpPost, ActionName("UserDashboard")]
        public ActionResult UserDashboardPost(int id)
        {
            var item = _contentManager.Get(id);
            if (item == null) return new HttpNotFoundResult();

            var editorShape = _contentManager.UpdateEditor(item, this);

            if (!ModelState.IsValid)
            {
                _transactionManager.Cancel();
                return UserDashboardShapeResult(item);
            }

            _contentManager.Publish(item);
            _notifier.Information(T("Your personal informations were successfully saved."));

            return RedirectToAction("UserSummary", new { id = item.Id });
        }

        public ActionResult UserSummary(int id)
        {
            var item = _contentManager.Get(id);
            if (item == null) return new HttpNotFoundResult();

            var itemDisplayShape = _contentManager.BuildDisplay(item);
            var displayShape = _orchardServices.New.SoftIT_HouseParty_UserSummary(DisplayShape: itemDisplayShape, Id: id);

            return new ShapeResult(this, displayShape);
        }

        public ActionResult RequestFriendship(int id)
        {
            _friendRequestRepository.Create(new FriendRequestRecord
            {
                RequestedId = id,
                RequesterId = _orchardServices.WorkContext.CurrentUser.Id
            });

            _notifier.Information(T("Your friend request has been sent."));
            
            return RedirectToAction("UserSummary", new { id = id });
        }

        public ActionResult SetFriendRequestResult(int requestId, int result)
        {
            var friendRequest = _friendRequestRepository.Table.FirstOrDefault(request => request.Id.Equals(requestId));

            if (result.Equals(0))
            {
                _friendRepository.Create(new FriendRecord
                {
                    FriendOneId = friendRequest.RequestedId,
                    FriendTwoId = friendRequest.RequesterId
                });
            }

            _friendRequestRepository.Delete(friendRequest);

            return Redirect(Request.UrlReferrer.ToString());
        }

        private ShapeResult UserDashboardShapeResult(ContentItem item)
        {
            var itemEditorShape = _contentManager.BuildEditor(item);
            var editorShape = _orchardServices.New.SoftIT_HouseParty_UserDashboard(EditorShape: itemEditorShape, Id: item.Id);

            return new ShapeResult(this, editorShape);
        }

        bool IUpdateModel.TryUpdateModel<TModel>(TModel model, string prefix, string[] includeProperties, string[] excludeProperties)
        {
            return TryUpdateModel(model, prefix, includeProperties, excludeProperties);
        }

        void IUpdateModel.AddModelError(string key, LocalizedString errorMessage)
        {
            ModelState.AddModelError(key, errorMessage.ToString());
        }
    }
}