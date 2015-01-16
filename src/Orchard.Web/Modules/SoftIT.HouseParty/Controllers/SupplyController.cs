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
    public class SupplyController : Controller, IUpdateModel
    {
        private readonly IOrchardServices _orchardServices;
        private readonly IContentManager _contentManager;
        private readonly ITransactionManager _transactionManager;
        private readonly INotifier _notifier;
        private readonly IRepository<InvitationRecord> _invitationRepository;

        private Localizer T { get; set; }


        public SupplyController(
            IOrchardServices orchardServices, 
            IContentManager contentManager, 
            ITransactionManager transactionManager,
            INotifier notifier, 
            IRepository<InvitationRecord> invitationRepository)
        {
            _orchardServices = orchardServices;
            _contentManager = contentManager;
            _transactionManager = transactionManager;
            _notifier = notifier;
            _invitationRepository = invitationRepository;

            T = NullLocalizer.Instance;
        }


        public ActionResult SupplyDashboard(int partyId, int id = 0, int type = 0)
        {
            var item = GetItem(id);
            if (item == null) return new HttpNotFoundResult();

            var supplyPart = item.As<SupplyPart>();
            supplyPart.PartyId = partyId;

            if (type.Equals(0))
            {
                if (!supplyPart.Party.FoodType.Equals("Self-service"))
                    supplyPart.AssignedToId = supplyPart.Party.As<CommonPart>().Owner.Id;
            }
            else
            {
                if (!supplyPart.Party.DrinkType.Equals("Self-service"))
                    supplyPart.AssignedToId = supplyPart.Party.As<CommonPart>().Owner.Id;
            }

            return SupplyDashboardShapeResult(item);
        }

        [HttpPost, ActionName("SupplyDashboard")]
        public ActionResult SupplyDashboardPost(int id = 0)
        {
            var item = GetItem(id);
            if (item == null) return new HttpNotFoundResult();

            if (id == 0) _contentManager.Create(item, VersionOptions.Draft);

            var editorShape = _contentManager.UpdateEditor(item, this);

            if (!ModelState.IsValid)
            {
                _transactionManager.Cancel();
                return SupplyDashboardShapeResult(item);
            }

            _contentManager.Publish(item);
            _notifier.Information(T("The supply was successfully saved."));

            return RedirectToAction("PartySummary", "Party", new { id = item.As<SupplyPart>().PartyId });
        }

        public ActionResult SupplySummary(int id)
        {
            var item = _contentManager.Get(id);
            if (item == null) return new HttpNotFoundResult();

            var itemDisplayShape = _contentManager.BuildDisplay(item);
            var displayShape = _orchardServices.New.SoftIT_HouseParty_PartySummary(DisplayShape: itemDisplayShape, Id: item.Id);

            return new ShapeResult(this, displayShape);
        }


        private ShapeResult SupplyDashboardShapeResult(ContentItem item)
        {
            var itemEditorShape = _contentManager.BuildEditor(item);
            var editorShape = _orchardServices.New.SoftIT_HouseParty_SupplyDashboard(EditorShape: itemEditorShape);

            return new ShapeResult(this, editorShape);
        }

        private ContentItem GetItem(int id)
        {
            return id.Equals(0) ? _contentManager.New(ContentTypes.Supply) : _contentManager.Get(id);
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