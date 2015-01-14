using Orchard;
using Orchard.ContentManagement;
using Orchard.Core.Common.Models;
using Orchard.Data;
using Orchard.Localization;
using Orchard.Mvc;
using Orchard.Security;
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
    public class PartyController : Controller, IUpdateModel
    {
        private readonly IOrchardServices _orchardServices;
        private readonly IContentManager _contentManager;
        private readonly ITransactionManager _transactionManager;
        private readonly INotifier _notifier;
        private readonly IRepository<InvitationRecord> _invitationRepository;

        private Localizer T { get; set; }

        public PartyController(IOrchardServices orchardServices, IContentManager contentManager, ITransactionManager transactionManager, INotifier notifier, IRepository<InvitationRecord> invitationRepository)
        {
            _orchardServices = orchardServices;
            _contentManager = contentManager;
            _transactionManager = transactionManager;
            _notifier = notifier;
            _invitationRepository = invitationRepository;

            T = NullLocalizer.Instance;
        }

        public ActionResult PartyDashboard(int id = 0)
        {
            var item = GetItem(id);
            if (item == null) return new HttpNotFoundResult();

            if (!item.As<CommonPart>().Owner.Id.Equals(_orchardServices.WorkContext.CurrentUser.Id))
                return new HttpUnauthorizedResult();

            return PartyDashboardShapeResult(item);
        }

        [HttpPost, ActionName("PartyDashboard")]
        public ActionResult PartyDashboardPost(int id = 0)
        {
            var item = GetItem(id);
            if (item == null) return new HttpNotFoundResult();

            if (id == 0) _contentManager.Create(item, VersionOptions.Draft);

            var editorShape = _contentManager.UpdateEditor(item, this);

            if (!ModelState.IsValid)
            {
                _transactionManager.Cancel();
                return PartyDashboardShapeResult(item);
            }

            _contentManager.Publish(item);
            _notifier.Information(T("The party was successfully saved."));

            return RedirectToAction("PartySummary", new { id = item.Id });
        }

        public ActionResult PartySummary(int id)
        {
            var item = _contentManager.Get(id);
            if (item == null) return new HttpNotFoundResult();

            var itemDisplayShape = _contentManager.BuildDisplay(item);
            var displayShape = _orchardServices.New.SoftIT_HouseParty_PartySummary(DisplayShape: itemDisplayShape, Id: item.Id);

            return new ShapeResult(this, displayShape);
        }

        public ActionResult InviteUser(int partyId)
        {
            var currentUserId = _orchardServices.WorkContext.CurrentUser.Id;
            var friendsIds = _orchardServices.WorkContext.CurrentUser.As<HousePartyUserPart>().FriendsRecords
                .Select(friendRecord => 
                    friendRecord.FriendOneId.Equals(currentUserId) ? friendRecord.FriendTwoId : friendRecord.FriendOneId);
            var invitedUserIds = _invitationRepository.Table
                .Where(
                    invite => invite.
                        PartyId.Equals(partyId))
                .Select(invite => invite.InvitedId)
                 .ToList();
            var friends = friendsIds.Select(friendId => _contentManager.Get<IUser>(friendId)).Where(friend => !invitedUserIds.Contains(friend.Id));
            var invitedFriends = _contentManager.Query("User").ForPart<IUser>().List<IUser>().Where(user => invitedUserIds.Contains(user.Id));

            var inviteFriendListShape = _orchardServices.New.SoftIT_HouseParty_InviteFriend(Friends: friends, InvitedFriends: invitedFriends, PartyId: partyId);

            return new ShapeResult(this, inviteFriendListShape);
        }

        public ActionResult SetUserState(int partyId, int userId, int request)
        {
            switch(request)
            {
                case 0:
                    _invitationRepository.Create(new InvitationRecord
                    {
                        InviterId = _orchardServices.WorkContext.CurrentUser.Id,
                        InvitedId = userId,
                        PartyId = partyId,
                        State = "Pending"
                    });
                    break;

                case 1:
                    var invitationRecord = _invitationRepository.Table.FirstOrDefault(invitation => invitation.PartyId.Equals(partyId) && invitation.InvitedId.Equals(userId));
                    _invitationRepository.Delete(invitationRecord);
                    break;
            }

            return RedirectToAction("InviteUser", new { partyId = partyId });
        }

        private ShapeResult PartyDashboardShapeResult(ContentItem item)
        {
            var itemEditorShape = _contentManager.BuildEditor(item);
            var editorShape = _orchardServices.New.SoftIT_HouseParty_PartyDashboard(EditorShape: itemEditorShape, Id: item.Id);

            return new ShapeResult(this, editorShape);
        }

        private ContentItem GetItem(int id)
        {
            return id.Equals(0) ? _contentManager.New(ContentTypes.Party) : _contentManager.Get(id);
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