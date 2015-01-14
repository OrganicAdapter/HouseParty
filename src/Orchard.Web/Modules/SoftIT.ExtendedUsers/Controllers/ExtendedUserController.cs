using Orchard;
using Orchard.ContentManagement;
using Orchard.Data;
using Orchard.Localization;
using Orchard.Mvc;
using Orchard.Themes;
using Orchard.UI.Notify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoftIT.ExtendedUsers.Controllers
{
    [Themed]
    [Authorize]
    public class ExtendedUserController : Controller, IUpdateModel
    {
        private readonly IOrchardServices _orchardServices;
        private readonly IContentManager _contentManager;
        private readonly ITransactionManager _transactionManager;
        private readonly INotifier _notifier;

        private Localizer T { get; set; }

        public ExtendedUserController(IOrchardServices orchardServices, IContentManager contentManager, ITransactionManager transactionManager, INotifier notifier)
        {
            _orchardServices = orchardServices;
            _contentManager = contentManager;
            _transactionManager = transactionManager;
            _notifier = notifier;

            T = NullLocalizer.Instance;
        }

        public ActionResult ExtendedUserDashboard(int id = 0)
        {
            var item = _contentManager.Get(id);
            if (item == null) return new HttpNotFoundResult();

            if (!id.Equals(_orchardServices.WorkContext.CurrentUser.Id))
                return new HttpUnauthorizedResult();

            return ExtendedUserDashboardShapeResult(item);
        }

        [HttpPost, ActionName("ExtendedUserDashboard")]
        public ActionResult ExtendedUserDashboardPost(int id = 0)
        {
            var item = _contentManager.Get(id);
            if (item == null) return new HttpNotFoundResult();

            var editorShape = _contentManager.UpdateEditor(item, this);

            if (!ModelState.IsValid)
            {
                _transactionManager.Cancel();
                return ExtendedUserDashboardShapeResult(item);
            }

            _contentManager.Publish(item);
            _notifier.Information(T("Your personal information was successfully saved."));

            return RedirectToAction("ExtendedUserSummary", new { id = item.Id });
        }

        public ActionResult ExtendedUserSummary(int id)
        {
            var item = _contentManager.Get(id);
            if (item == null) return new HttpNotFoundResult();

            var itemDisplayShape = _contentManager.BuildDisplay(item);
            var displayShape = _orchardServices.New.SoftIT_ExtendedUsers_ExtendedUserSummary(DisplayShape: itemDisplayShape);

            return new ShapeResult(this, displayShape);
        }

        private ShapeResult ExtendedUserDashboardShapeResult(ContentItem item)
        {
            var itemEditorShape = _contentManager.BuildEditor(item);
            var editorShape = _orchardServices.New.SoftIT_ExtendedUsers_ExtendedUserDashboard(EditorShape: itemEditorShape, Id: item.Id);

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