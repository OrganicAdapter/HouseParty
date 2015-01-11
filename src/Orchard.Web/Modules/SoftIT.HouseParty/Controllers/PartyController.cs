using Orchard;
using Orchard.ContentManagement;
using Orchard.Data;
using Orchard.Localization;
using Orchard.Mvc;
using Orchard.Themes;
using Orchard.UI.Notify;
using SoftIT.HouseParty.Constants;
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

        private Localizer T { get; set; }

        public PartyController(IOrchardServices orchardServices, IContentManager contentManager, ITransactionManager transactionManager, INotifier notifier)
        {
            _orchardServices = orchardServices;
            _contentManager = contentManager;
            _transactionManager = transactionManager;
            _notifier = notifier;

            T = NullLocalizer.Instance;
        }

        public ActionResult PartyDashboard(int id = 0)
        {
            var item = GetItem(id);
            if (item == null) return new HttpNotFoundResult();

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

            return RedirectToAction("PartyDashboard");
        }

        private ShapeResult PartyDashboardShapeResult(ContentItem item)
        {
            var itemEditorShape = _contentManager.BuildEditor(item);
            var editorShape = _orchardServices.New.SoftIT_HouseParty_PartyDashboard(EditorShape: itemEditorShape);

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